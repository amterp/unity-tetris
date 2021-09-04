using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaController : MonoBehaviour
{
    private Dictionary<Coordinate, GameCell> _cellsByCoordinate;

    void Start()
    {
        _cellsByCoordinate = GetComponent<PlayAreaSetupper>().InitializeGameCells();
    }

    void Update()
    {

    }
}
