using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockPreviewController : MonoBehaviour
{

    private Dictionary<Vector2Int, GameCell> _cellsByCoordinate;
    private BlockSpawnerBuffer _blockSpawnerBuffer;
    private DimensionsHandler _dimensions;

    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        _blockSpawnerBuffer = GetComponent<BlockSpawnerBuffer>();
        _blockSpawnerBuffer.BufferUpdatedEvent += OnBufferUpdate;
        _dimensions = GetComponent<DimensionsHandler>();
        GameState _gameState = GoUtil.FindGameState();
        _gameState.GameStartedEvent += () => EnableGameCells();
        _gameState.GameOverEvent += () => DisableGameCells();
    }

    void Start()
    {
        Initialize();
    }

    private void OnBufferUpdate()
    {
        PlaceBlocksInPreviewArea();
    }

    private void PlaceBlocksInPreviewArea()
    {
        WipeExistingBlockPieces();
        List<Block> blocksToRender = _blockSpawnerBuffer.GetBlocksInBuffer();
        for (int i = 0; i < blocksToRender.Count; i++)
        {
            int rowIndexToPlace = 3 * i + 1;
            PlaceBlockOnRow(blocksToRender[i], rowIndexToPlace);
        }
    }

    private void WipeExistingBlockPieces()
    {
        if (!Initialized()) Initialize();
        new List<Vector2Int>(_cellsByCoordinate.Keys)
            .ForEach(coordinate => _cellsByCoordinate[coordinate].BlockPiece = null);
    }

    private void PlaceBlockOnRow(Block block, int rowIndexToPlaceAt)
    {
        int centerOffsetX = _dimensions.CalculateCenterOffsetX(block.BlockType);
        BlockTransformation placementTransformation = block.CalculateTransformationForMovingToCoordinate(centerOffsetX, rowIndexToPlaceAt);
        block.PerformTransformation(placementTransformation);
        new List<Coordinate>(placementTransformation.OldToNewCoordinates.Keys).ForEach(oldCoordinate =>
        {
            Coordinate newCoordinate = placementTransformation.OldToNewCoordinates[oldCoordinate];
            _cellsByCoordinate[newCoordinate.AsVector2Int()].BlockPiece = block.PiecesByCoordinate[newCoordinate];
        });
    }

    private void Initialize()
    {
        if (Initialized()) return;
        _cellsByCoordinate = GetComponent<AreaSetupper>().InitializeGameCells();
    }

    private void EnableGameCells()
    {
        ToggleGameCells(true);
    }

    private void DisableGameCells()
    {
        ToggleGameCells(false);
    }

    private void ToggleGameCells(bool enable)
    {
        Initialize();
        new List<GameCell>(_cellsByCoordinate.Values).ForEach(gameCell => gameCell.gameObject.SetActive(enable));
    }

    private bool Initialized()
    {
        return _cellsByCoordinate != null;
    }
}
