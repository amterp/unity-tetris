using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UiGameOverShifter : MonoBehaviour
{

    [SerializeField] private float _transitionTimeSeconds;
    [SerializeField] private Vector2 _targetPos;
    private RectTransform _rectTransform;
    private Vector2 _originalPos;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _originalPos = _rectTransform.anchoredPosition;
        GameState _gameState = GoUtil.FindGameState();
        _gameState.GameStartedEvent += () => ResetPosition();
        _gameState.GameOverEvent += () => BeginShiftingPosition();
    }

    private void ResetPosition()
    {
        _rectTransform.anchoredPosition = _originalPos;
    }

    private void BeginShiftingPosition()
    {
        StartCoroutine(ShiftPosition());
    }

    private IEnumerator ShiftPosition()
    {
        float startTimeSeconds = Time.time;
        float endTimeSeconds = startTimeSeconds + _transitionTimeSeconds;

        while (Time.time <= endTimeSeconds)
        {
            float elapsedTimeSeconds = (Time.time - startTimeSeconds);
            float lerpFraction = elapsedTimeSeconds / _transitionTimeSeconds;
            Vector2 lerpedPosition = Vector2.Lerp(_originalPos, _targetPos, lerpFraction);
            _rectTransform.anchoredPosition = lerpedPosition;
            yield return new WaitForEndOfFrame();
        }
    }
}
