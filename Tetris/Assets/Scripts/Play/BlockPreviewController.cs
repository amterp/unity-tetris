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
        _blockSpawnerBuffer = GetComponent<BlockSpawnerBuffer>();
        _blockSpawnerBuffer.BufferUpdatedEvent += OnBufferUpdate;
        _dimensions = GetComponent<DimensionsHandler>();
    }

    void Start()
    {
        _cellsByCoordinate = GetComponent<AreaSetupper>().InitializeGameCells();
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
}
