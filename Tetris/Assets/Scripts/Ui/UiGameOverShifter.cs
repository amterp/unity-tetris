using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UiGameOverShifter : MonoBehaviour
{

    [SerializeField] private float _transitionTimeSeconds = 1f;
    [SerializeField] private EasingType _easingType = EasingType.InOutSine;
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
            float smoothedLerpFraction = _easingType.Apply(lerpFraction);

            Vector2 lerpedPosition = Vector2.Lerp(_originalPos, _targetPos, smoothedLerpFraction);

            _rectTransform.anchoredPosition = lerpedPosition;
            yield return new WaitForEndOfFrame();
        }

        _rectTransform.anchoredPosition = _targetPos;
    }
}
