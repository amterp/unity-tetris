using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    private const int NUM_CELLS_PER_ROW = 10;

    [Range(0f, 5f)]
    public float InitialCompletionWaitSeconds = 0f;
    [Range(0f, 1f)]
    public float CellCompletionSpacingSeconds = 0.02f;

    [HideInInspector]
    public float LineCompletionPauseSeconds { get { return InitialCompletionWaitSeconds + (NUM_CELLS_PER_ROW - 1) * CellCompletionSpacingSeconds; } private set { } }
}
