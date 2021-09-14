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

    public event Action<BlockTransformation> BlockTransformationEvent;
    public event Action BlockPlacedEvent;

    private Dictionary<Vector2Int, GameCell> _cellsByCoordinate;
    private DimensionsHandler _dimensions;
    private List<Vector2Int> _ghostBlockCoordinates;
    private GameState _gameState;

    void Awake()
    {
        _dimensions = GetComponent<DimensionsHandler>();
        _ghostBlockCoordinates = new List<Vector2Int>();

        _gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
        _gameState.GameStartedEvent += OnGameStarted;
    }

    void Start()
    {
        _cellsByCoordinate = GetComponent<PlayAreaSetupper>().InitializeGameCells();
        _gameState.SetGameStarted();
    }

    public void AddBlock(Block currentBlock)
    {
        MoveBlockUpIfInitialSpawnHasNoRoom(currentBlock);
        AddBlockToPlayArea(currentBlock);
        UpdateBlockGhost(currentBlock);
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
            currentBlock.CalculateRotatedCoordinates(currentBlock, rotationDirection, coordinates => IsValidPlacement(currentBlock, coordinates));
        if (!blockTransformation.IsValid()) return;
        currentBlock.PerformTransformation(blockTransformation);
        UpdateCellTable(blockTransformation.OldToNewCoordinates);
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

    private void MoveBlockUpIfInitialSpawnHasNoRoom(Block currentBlock)
    {
        if (RowIndexIsEmpty(1))
        {
            return;
        }
        else if (RowIndexIsEmpty(0))
        {
            MoveYSpawnBy(-1, currentBlock);
            return;
        }
        else
        {
            MoveYSpawnBy(-2, currentBlock);
            return;
        }
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
            ShiftBlock(currentBlock, transformation);
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

    private void ShiftBlock(Block currentBlock, BlockTransformation blockTransformation)
    {
        currentBlock.PerformTransformation(blockTransformation);
        UpdateCellTable(blockTransformation.OldToNewCoordinates);
        UpdateBlockGhost(currentBlock);
        EventUtil.SafeInvoke(BlockTransformationEvent, blockTransformation);
    }

    private void UpdateCellTable(Dictionary<Coordinate, Coordinate> oldToNewCoordinateDict)
    {
        Dictionary<Coordinate, BlockPiece> piecesByNewCoordinate = new Dictionary<Coordinate, BlockPiece>();

        foreach (Coordinate oldCoordinate in oldToNewCoordinateDict.Keys)
        {
            GameCell gameCellAtOldCoordinate = _cellsByCoordinate[oldCoordinate.AsVector2Int()];
            BlockPiece? blockPieceToMove = gameCellAtOldCoordinate.BlockPiece;

            if (blockPieceToMove == null) continue;

            gameCellAtOldCoordinate.BlockPiece = null;
            piecesByNewCoordinate.Add(oldToNewCoordinateDict[oldCoordinate], blockPieceToMove);
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
            Coordinate coordinateToMove = new Coordinate(columnIndex, rowIndex, NOT_USED, NOT_USED);
            Coordinate coordinateToMoveTo = new Coordinate(columnIndex, rowIndex + yShiftAmount, NOT_USED, NOT_USED);
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
            && coordinate.Y >= PlayAreaSetupper.SPAWN_PILLOW_ROOM);
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
