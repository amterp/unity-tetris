using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DimensionsHandler))]
public class OriginHandler : MonoBehaviour, IOriginProvider
{
    public Action OriginChangeEvent { get; set; }

    [SerializeField] private float _gameOverShiftX = -4;
    [SerializeField] private float _gameOverShiftTimeSeconds = 1f;
    private float _originX;
    private float _originY;
    private DimensionsHandler _dimensionsHandler;
    private GameState _gameState;
    private bool _initialized;

    private float _secondGameEnded;

    void Awake()
    {
        _dimensionsHandler = GetComponent<DimensionsHandler>();
        _gameState = GoUtil.FindGameState();
        _gameState.GameOverEvent += OnGameOver;
        _gameState.GameStartedEvent += () => EventUtil.SafeInvoke(OriginChangeEvent);
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (IsOriginChanging())
        {
            EventUtil.SafeInvoke(OriginChangeEvent);
        }
    }


    public float GetX()
    {
        Initialize();
        return !IsOriginChanging()
            ? _originX
            : CalculateGameOverOrigin().x;
    }

    public float GetY()
    {
        Initialize();
        return !IsOriginChanging()
            ? _originY
            : CalculateGameOverOrigin().y;
    }

    private void Initialize()
    {
        if (_initialized) return;
        _originX = -transform.localScale.x / 2 + _dimensionsHandler.XScale / 2 + transform.position.x;
        _originY = transform.localScale.y / 2 - _dimensionsHandler.YScale / 2 + transform.position.y;
        _initialized = true;
    }

    private void OnGameOver()
    {
        _secondGameEnded = Time.time;
    }

    private Vector2 CalculateGameOverOrigin()
    {
        float elapsedTimeSeconds = Time.time - _secondGameEnded;
        return new Vector2(Mathf.Lerp(_originX, _originX + _gameOverShiftX, elapsedTimeSeconds / _gameOverShiftTimeSeconds), _originY);
    }

    public bool IsOriginChanging()
    {
        return !_gameState.IsGameInProgress() && Time.time < _secondGameEnded + _gameOverShiftTimeSeconds * 3;
    }
}