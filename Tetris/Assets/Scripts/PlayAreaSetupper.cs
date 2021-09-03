using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaSetupper : MonoBehaviour
{
    public int NumberXCells;
    public int NumberYCells;
    public GameObject GameCellPrefab;

    public Dictionary<Coordinate, GameCell> InitializeGameCells()
    {
        Dictionary<Coordinate, GameCell> cellsByCoordinate = new Dictionary<Coordinate, GameCell>();

        float scale = transform.localScale.x / NumberXCells;
        float originX = -transform.localScale.x / 2 + scale / 2;
        float originY = transform.localScale.y / 2 - scale / 2;

        for (int y = 0; y < NumberYCells; y++)
        {
            for (int x = 0; x < NumberXCells; x++)
            {
                GameCell gameCell = GameObject.Instantiate<GameObject>(GameCellPrefab).GetComponent<GameCell>();
                gameCell.transform.localScale = new Vector3(scale, scale);
                gameCell.InitializeCoordinate(x, y, originX, originY, scale);
                cellsByCoordinate.Add(gameCell.Coordinate, gameCell);
            }
        }

        return cellsByCoordinate;
    }
}
