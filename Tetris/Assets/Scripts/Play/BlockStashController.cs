using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AreaSetupper), typeof(DimensionsHandler))]
public class BlockStashController : MonoBehaviour
{

    private AreaSetupper _areaSetupper;
    private DimensionsHandler _dimensions;
    private Dictionary<Vector2Int, GameCell> _cellsByCoordinate;
    private Block? _stashedBlock;
    private bool _initialized;

    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        _areaSetupper = GetComponent<AreaSetupper>();
        _dimensions = GetComponent<DimensionsHandler>();
        GameState _gameState = GoUtil.FindGameState();
        _gameState.GameStartedEvent += OnGameStarted;
        _gameState.GameOverEvent += () => DisableGameCells();
    }

    void Start()
    {
        Initialize();
    }

    public Block? SwapBlock(Block currentBlock)
    {
        Block? blockToGiveBack = _stashedBlock;
        _stashedBlock = currentBlock;

        UpdateStashArea();

        return blockToGiveBack;
    }

    private void OnGameStarted()
    {
        Initialize();
        ClearArea();
        EnableGameCells();
    }

    private void UpdateStashArea()
    {
        ClearArea();
        AddCurrentlyStashedBlock();
    }

    private void ClearArea()
    {
        new List<Vector2Int>(_cellsByCoordinate.Keys)
            .ForEach(coordinate => _cellsByCoordinate[coordinate].BlockPiece = null);
    }

    private void AddCurrentlyStashedBlock()
    {
        if (_stashedBlock == null) return;
        _stashedBlock.PerformRotationReset();
        CenterBlockAndUpdateCells();
    }

    private void CenterBlockAndUpdateCells()
    {
        int centerX = _dimensions.CalculateCenterOffsetX(_stashedBlock.BlockType);
        BlockTransformation placementTransformation = _stashedBlock.CalculateTransformationForMovingToCoordinate(centerX, 1);
        _stashedBlock.PerformTransformation(placementTransformation);

        new List<Coordinate>(placementTransformation.OldToNewCoordinates.Values)
            .ForEach(coordinate => _cellsByCoordinate[coordinate.AsVector2Int()].BlockPiece = placementTransformation.Block.PiecesByCoordinate[coordinate]);
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

    private void Initialize()
    {
        if (_initialized) return;
        _cellsByCoordinate = _areaSetupper.InitializeGameCells();
        _initialized = true;
    }
}
