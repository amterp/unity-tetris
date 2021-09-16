using System;
using System.Collections.Generic;
using UnityEngine;

public class AreaSetupper : MonoBehaviour
{
    public const int SPAWN_PILLOW_ROOM = -2;

    public GameObject GameCellPrefab;
    public int NumberPillowCells;

    private DimensionsHandler _dimensions;

    public Dictionary<Vector2Int, GameCell> InitializeGameCells()
    {
        if (_dimensions != null) throw new InvalidOperationException("Can only init game cells once!");
        _dimensions = GetComponent<DimensionsHandler>();

        Dictionary<Vector2Int, GameCell> cellsByCoordinate = new Dictionary<Vector2Int, GameCell>();
        for (int y = -NumberPillowCells; y < _dimensions.NumberYCells; y++)
        {
            for (int x = 0; x < _dimensions.NumberXCells; x++)
            {
                GameCell gameCell = GameObject.Instantiate<GameObject>(GameCellPrefab).GetComponent<GameCell>();
                gameCell.transform.localScale = _dimensions.GetCellScale();
                gameCell.InitCoordinate(_dimensions.CreateCoordinate(x, y, gameCell.transform));
                cellsByCoordinate.Add(gameCell.Coordinate.AsVector2Int(), gameCell);
            }
        }

        return cellsByCoordinate;
    }
}
