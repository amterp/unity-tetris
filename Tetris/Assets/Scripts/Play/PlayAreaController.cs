using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayAreaController : MonoBehaviour
{
    private const int FIRST_ROW_INDEX_OUTSIDE_PLAY_AREA = -1;
    private const float NOT_USED = -1f;
    private const bool SUCCEEDED = true;
    private const bool FAILED = false;
    private const bool CLEAR_EXISTING_CELLS = true;
    private const bool DO_NOT_CLEAR_EXISTING_CELLS = false;
    private readonly IOriginProvider NOT_USED_ORIGIN_PROVIDER = new ZeroOriginProvider();

    public event Action<BlockTransformation> BlockTransformationEvent;
    public event Action BlockPlacedEvent;
    public event Action<int> RowsCompletedEvent;

    private Dictionary<Vector2Int, GameCell> _cellsByCoordinate;
    private DimensionsHandler _dimensions;
    private List<Vector2Int> _ghostBlockCoordinates;
    private GameState _gameState;
    private AudioManager _audioManager;

    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        _dimensions = GetComponent<DimensionsHandler>();
        _ghostBlockCoordinates = new List<Vector2Int>();

        _gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
        _gameState.GameStartedEvent += OnGameStarted;

        _audioManager = GoUtil.FindAudioManager();
    }

    void Start()
    {
        _cellsByCoordinate = GetComponent<AreaSetupper>().InitializeGameCells();
        _gameState.SetGameStarted();
    }

    public void AddBlock(Block currentBlock)
    {
        ShiftIntoInitialYPosition(currentBlock);
        ShiftIntoInitialXPosition(currentBlock);

        AddBlockToPlayArea(currentBlock);
        UpdateBlockGhost(currentBlock);
    }

    public void RemoveBlock(Block currentBlock)
    {
        new List<Coordinate>(currentBlock.GetCoordinatesCopy())
            .ForEach(coordinate => _cellsByCoordinate[coordinate.AsVector2Int()].BlockPiece = null);
    }

    public void TryMove(Block currentBlock, int xShift, int yShift)
    {
        if (xShift != 0 && yShift != 0)
        {
            throw new InvalidOperationException("Cannot move diagonally!");
        }
        BlockTransformation blockTransformation = currentBlock.CalculateLinearTransformation(xShift, yShift);
        PerformShiftTransformation(currentBlock, blockTransformation);
    }

    public void TryRotate(Block currentBlock, RotationDirection rotationDirection)
    {
        BlockTransformation blockTransformation =
            currentBlock.CalculateRotatedCoordinates(rotationDirection, coordinates => IsValidPlacement(currentBlock, coordinates));
        if (!blockTransformation.IsValid()) return;
        currentBlock.PerformTransformation(blockTransformation);
        UpdateCellTable(blockTransformation, true);
        UpdateBlockGhost(currentBlock);
        EventUtil.SafeInvoke(BlockTransformationEvent, blockTransformation);
    }

    public void InstantPlace(Block currentBlock)
    {
        BlockTransformation instantPlaceTransformation = CalculateInstantPlaceTransformation(currentBlock);
        PerformShiftTransformation(currentBlock, instantPlaceTransformation);
        PlaceBlock(currentBlock);
    }

    private BlockTransformation CalculateInstantPlaceTransformation(Block currentBlock)
    {
        BlockTransformation previousTestedTransformation = currentBlock.CalculateLinearTransformation(0, 0);
        BlockTransformation currentTransformationToTest;
        for (int cellDistance = 1; cellDistance < _dimensions.NumberYCells; cellDistance++)
        {
            currentTransformationToTest = currentBlock.CalculateLinearTransformation(0, cellDistance);
            if (!currentTransformationToTest.IsValid()
                || !IsValidPlacement(currentTransformationToTest.Block, new List<Coordinate>(currentTransformationToTest.OldToNewCoordinates.Values)))
            {
                return previousTestedTransformation;
            }
            previousTestedTransformation = currentTransformationToTest;
        }
        return previousTestedTransformation;
    }

    private void PerformShiftTransformation(Block currentBlock, BlockTransformation transformation)
    {
        if (!IsValidPlacement(transformation.Block, new List<Coordinate>(transformation.OldToNewCoordinates.Values)))
        {
            if (transformation.ShiftDirection == ShiftDirection.Down)
            {
                PlaceBlock(currentBlock);
            }
        }
        else if (transformation.IsValid())
        {
            ShiftBlockWithEventTrigger(currentBlock, transformation);
        }
    }

    private void AddBlockToPlayArea(Block currentBlock)
    {
        new List<Coordinate>(currentBlock.PiecesByCoordinate.Keys)
            .ForEach(coordinate => _cellsByCoordinate[coordinate.AsVector2Int()].BlockPiece = currentBlock.PiecesByCoordinate[coordinate]);
    }

    private void MoveYSpawnBy(int yShift, Block currentBlock)
    {
        BlockTransformation shiftUpTransformation = currentBlock.CalculateLinearTransformation(0, yShift);
        currentBlock.PerformTransformation(shiftUpTransformation);
    }

    private void ShiftBlockWithEventTrigger(Block currentBlock, BlockTransformation blockTransformation)
    {
        currentBlock.PerformTransformation(blockTransformation);
        UpdateCellTable(blockTransformation, CLEAR_EXISTING_CELLS);
        UpdateBlockGhost(currentBlock);
        EventUtil.SafeInvoke(BlockTransformationEvent, blockTransformation);
    }

    private void ShiftBlockWithoutEventTriggerAndNoExistingCellClear(Block currentBlock, BlockTransformation blockTransformation)
    {
        currentBlock.PerformTransformation(blockTransformation);
        UpdateCellTable(blockTransformation, DO_NOT_CLEAR_EXISTING_CELLS);
        UpdateBlockGhost(currentBlock);
    }

    private void ShiftBlockWithoutEventTriggerAndClearExistingCells(Block currentBlock, BlockTransformation blockTransformation)
    {
        currentBlock.PerformTransformation(blockTransformation);
        UpdateCellTable(blockTransformation, CLEAR_EXISTING_CELLS);
        UpdateBlockGhost(currentBlock);
    }

    private void UpdateCellTable(BlockTransformation blockTransformation, bool clearOldCells)
    {
        Dictionary<Coordinate, Coordinate> oldToNewCoordinateDict = blockTransformation.OldToNewCoordinates;
        Dictionary<Coordinate, BlockPiece> piecesByNewCoordinate = new Dictionary<Coordinate, BlockPiece>();

        foreach (Coordinate oldCoordinate in oldToNewCoordinateDict.Keys)
        {
            Coordinate newCoordinate = oldToNewCoordinateDict[oldCoordinate];
            GameCell gameCellAtOldCoordinate = _cellsByCoordinate[oldCoordinate.AsVector2Int()];
            BlockPiece? blockPieceToMove = gameCellAtOldCoordinate.BlockPiece;

            if (blockPieceToMove != null && clearOldCells)
            {
                gameCellAtOldCoordinate.BlockPiece = null;
            }
            else
            {
                blockPieceToMove = blockTransformation.Block.PiecesByCoordinate[newCoordinate];
            }

            piecesByNewCoordinate.Add(newCoordinate, blockPieceToMove);
        }

        foreach (Coordinate newCoordinate in piecesByNewCoordinate.Keys)
        {
            _cellsByCoordinate[newCoordinate.AsVector2Int()].BlockPiece = piecesByNewCoordinate[newCoordinate];
        }
    }

    private void PlaceBlock(Block currentBlock)
    {
        currentBlock.IsPlaced = true;
        CheckRowCompletion();
        CheckGameOver();
        EventUtil.SafeInvoke(BlockPlacedEvent);
        _audioManager.Play(SoundEnum.BlockPlacementBeep);
    }

    private void CheckGameOver()
    {
        if (RowIndexIsEmpty(FIRST_ROW_INDEX_OUTSIDE_PLAY_AREA)) return;
        _gameState.SetGameOver();
    }

    private void CheckRowCompletion()
    {
        int numRowsComplete = 0;
        for (int rowIndex = _dimensions.NumberYCells - 1; rowIndex >= 0; rowIndex--)
        {
            if (RowComplete(rowIndex))
            {
                DeleteRow(rowIndex);
                numRowsComplete++;
            }
            else
            {
                ShiftDownRow(rowIndex, numRowsComplete);
            }
        }
        if (numRowsComplete > 0) EventUtil.SafeInvoke(RowsCompletedEvent, numRowsComplete);
    }

    private bool RowComplete(int rowIndex)
    {
        for (int columnIndex = 0; columnIndex < _dimensions.NumberXCells; columnIndex++)
        {
            if (_cellsByCoordinate[new Vector2Int(columnIndex, rowIndex)].IsEmpty())
            {
                return false;
            }
        }
        return true;
    }

    private void DeleteRow(int rowIndex)
    {
        for (int columnIndex = 0; columnIndex < _dimensions.NumberXCells; columnIndex++)
        {
            _cellsByCoordinate[new Vector2Int(columnIndex, rowIndex)].BlockPiece = null;
        }
    }

    private void ShiftDownRow(int rowIndex, int yShiftAmount)
    {
        if (yShiftAmount == 0) return;
        for (int columnIndex = 0; columnIndex < _dimensions.NumberXCells; columnIndex++)
        {
            Coordinate coordinateToMove = new Coordinate(columnIndex, rowIndex, NOT_USED_ORIGIN_PROVIDER);
            Coordinate coordinateToMoveTo = new Coordinate(columnIndex, rowIndex + yShiftAmount, NOT_USED_ORIGIN_PROVIDER);
            _cellsByCoordinate[coordinateToMoveTo.AsVector2Int()].BlockPiece = _cellsByCoordinate[coordinateToMove.AsVector2Int()].BlockPiece;
            _cellsByCoordinate[coordinateToMove.AsVector2Int()].BlockPiece = null;
        }
    }

    private bool IsValidPlacement(Block currentBlock, List<Coordinate> potentialCoordinates)
    {
        return AreWithinBounds(potentialCoordinates) && CoordinatesAreOpen(currentBlock, potentialCoordinates);
    }

    private bool AreWithinBounds(List<Coordinate> shiftedCoordinates)
    {
        return shiftedCoordinates.TrueForAll(coordinate => coordinate.X < _dimensions.NumberXCells
            && coordinate.X >= 0
            && coordinate.Y < _dimensions.NumberYCells
            && coordinate.Y >= AreaSetupper.SPAWN_PILLOW_ROOM);
    }

    private bool CoordinatesAreOpen(Block currentBlock, List<Coordinate> shiftedCoordinates)
    {
        return shiftedCoordinates.TrueForAll(coordinate =>
            _cellsByCoordinate[coordinate.AsVector2Int()].IsEmpty() || currentBlock.PiecesByCoordinate.ContainsKey(coordinate));
    }

    public bool RowIndexIsEmpty(int yRowIndex)
    {
        for (int x = 0; x < _dimensions.NumberXCells; x++)
        {
            Vector2Int vector2IntCoordinate = new Vector2Int(x, yRowIndex);
            if (_cellsByCoordinate[vector2IntCoordinate].BlockPiece != null) return false;
        }
        return true;
    }

    private void UpdateBlockGhost(Block currentBlock)
    {
        ClearExistingGhostPieces();
        BlockTransformation transformation = CalculateInstantPlaceTransformation(currentBlock);
        AddNewGhostPieces(transformation);
    }

    private void ClearExistingGhostPieces()
    {
        _ghostBlockCoordinates.ForEach(coordinate => _cellsByCoordinate[coordinate].GhostBlockPiece = null);
    }

    private void AddNewGhostPieces(BlockTransformation transformation)
    {
        foreach (Coordinate existingCoordinate in transformation.OldToNewCoordinates.Keys)
        {
            Vector2Int ghostCoordinate = transformation.OldToNewCoordinates[existingCoordinate].AsVector2Int();
            _cellsByCoordinate[ghostCoordinate].GhostBlockPiece =
                transformation.Block.PiecesByCoordinate[existingCoordinate];
            _ghostBlockCoordinates.Add(ghostCoordinate);
        }
    }

    private void ShiftIntoInitialYPosition(Block currentBlock)
    {
        BlockTransformation initialYTransformation;
        if (RowIndexIsEmpty(1))
        {
            initialYTransformation = currentBlock.CalculateTransformationForMovingToCoordinateY(0);
        }
        else if (RowIndexIsEmpty(0))
        {
            initialYTransformation = currentBlock.CalculateTransformationForMovingToCoordinateY(-1);
        }
        else
        {
            initialYTransformation = currentBlock.CalculateTransformationForMovingToCoordinateY(-2);
        }
        ShiftBlockWithoutEventTriggerAndNoExistingCellClear(currentBlock, initialYTransformation);
    }

    private void ShiftIntoInitialXPosition(Block currentBlock)
    {
        int centerOffsetX = _dimensions.CalculateCenterOffsetX(currentBlock.BlockType);
        BlockTransformation initialCenteringTransformation = currentBlock.CalculateTransformationForMovingToCoordinateX(centerOffsetX);
        ShiftBlockWithoutEventTriggerAndClearExistingCells(currentBlock, initialCenteringTransformation);
    }

    private void OnGameStarted()
    {
        new List<GameCell>(_cellsByCoordinate.Values).ForEach(gameCell => gameCell.Reset());
        _ghostBlockCoordinates.Clear();
    }

    private static List<Coordinate> CalculateShiftedCoordinates(List<Coordinate> coordinatesToShift, int xShift, int yShift)
    {
        return coordinatesToShift.Select(coordinate => coordinate.Shifted(xShift, yShift)).ToList();
    }
}
