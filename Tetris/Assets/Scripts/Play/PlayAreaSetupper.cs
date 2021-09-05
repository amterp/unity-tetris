using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaSetupper : MonoBehaviour
{

    public GameObject GameCellPrefab;

    private DimensionsHandler _dimensions;

    public Dictionary<Coordinate, GameCell> InitializeGameCells()
    {
        if (_dimensions != null) throw new InvalidOperationException("Can only init game cells once!");
        _dimensions = GetComponent<DimensionsHandler>();

        Dictionary<Coordinate, GameCell> cellsByCoordinate = new Dictionary<Coordinate, GameCell>();
        for (int y = 0; y < _dimensions.NumberYCells; y++)
        {
            for (int x = 0; x < _dimensions.NumberXCells; x++)
            {
                GameCell gameCell = GameObject.Instantiate<GameObject>(GameCellPrefab).GetComponent<GameCell>();
                gameCell.transform.localScale = _dimensions.GetCellScale();
                Debug.Log(gameCell.transform.localScale + " = " + _dimensions.GetCellScale());
                gameCell.InitCoordinate(_dimensions.CreateCoordinate(x, y, gameCell.transform));
                cellsByCoordinate.Add(gameCell.Coordinate, gameCell);
            }
        }

        return cellsByCoordinate;
    }
}
