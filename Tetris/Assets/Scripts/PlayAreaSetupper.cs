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

        float xScale = transform.localScale.x / NumberXCells;
        float yScale = transform.localScale.y / NumberYCells;
        Vector3 gameCellScale = new Vector3(xScale, yScale);

        float originX = -transform.localScale.x / 2 + xScale / 2;
        float originY = transform.localScale.y / 2 - yScale / 2;

        for (int y = 0; y < NumberYCells; y++)
        {
            for (int x = 0; x < NumberXCells; x++)
            {
                GameCell gameCell = GameObject.Instantiate<GameObject>(GameCellPrefab).GetComponent<GameCell>();
                gameCell.transform.localScale = gameCellScale;
                gameCell.InitializeCoordinate(x, y, originX, originY);
                cellsByCoordinate.Add(gameCell.Coordinate, gameCell);
            }
        }

        return cellsByCoordinate;
    }
}
