using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCell : MonoBehaviour
{

    public Color SpawnPillowRoomCellColor;
    public float GhostColorStrength = 0.3f;

    public Coordinate Coordinate { get; private set; }
    public BlockPiece? BlockPiece { get { return _realBlockPiece; } set { _realBlockPiece = value; UpdateRender(); } }
    public BlockPiece? GhostBlockPiece { get { return _ghostBlockPiece; } set { _ghostBlockPiece = value; UpdateRender(); } }

    private BlockPiece? _realBlockPiece;
    private BlockPiece? _ghostBlockPiece;
    private SpriteRenderer _spriteRenderer;
    private GameConstants _gameConstants;
    private Color _defaultEmptyColor;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameConstants = GoUtil.FindGameConstants();
        _defaultEmptyColor = _spriteRenderer.color;
    }

    public void InitCoordinate(Coordinate coordinate)
    {
        if (Coordinate != null) throw new InvalidOperationException("Can only add a coordinate once!");
        Coordinate = coordinate;
        UpdateRender();
    }

    public void Reset()
    {
        BlockPiece = null;
        GhostBlockPiece = null;
    }

    public bool IsEmpty()
    {
        return BlockPiece == null;
    }

    public IEnumerator AnimateToWhite(float cellSpecificDurationSeconds)
    {
        float zeroGuardedDurationSeconds = cellSpecificDurationSeconds;

        Color startingColor = _spriteRenderer.color;
        float startingTimeSeconds = Time.time;
        float stopTimeSeconds = startingTimeSeconds + zeroGuardedDurationSeconds;
        while (Time.time <= stopTimeSeconds && BlockPiece != null)
        {
            float elapsedTimeSeconds = (Time.time - startingTimeSeconds);
            float rawLerpFraction = elapsedTimeSeconds / zeroGuardedDurationSeconds;
            float lerpFraction = Easing.EaseInQuad(rawLerpFraction);
            _spriteRenderer.color = Color.Lerp(startingColor, Color.white, lerpFraction);
            yield return new WaitForEndOfFrame();
        }
        _spriteRenderer.color = Color.white;

        float overallRowCompletionSecondsRemaining = _gameConstants.LineCompletionPauseSeconds - zeroGuardedDurationSeconds;
        yield return new WaitForSeconds(overallRowCompletionSecondsRemaining);

        UpdateRender();
    }

    private void UpdateRender()
    {
        UpdateBaseColor();
        AddGhostColorIfApplicable();
    }

    private void UpdateBaseColor()
    {
        if (BlockPiece != null)
        {
            _spriteRenderer.color = BlockPiece.GetColor();
        }
        else if (IsSpawnPillowRoomCell())
        {
            _spriteRenderer.color = SpawnPillowRoomCellColor;
        }
        else if (BlockPiece == null)
        {
            _spriteRenderer.color = _defaultEmptyColor;
        }
    }

    private void AddGhostColorIfApplicable()
    {
        if (_ghostBlockPiece != null)
        {
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _ghostBlockPiece.GetColor(), GhostColorStrength);
        }
    }

    private bool IsSpawnPillowRoomCell()
    {
        return Coordinate.Y < 0;
    }
}
