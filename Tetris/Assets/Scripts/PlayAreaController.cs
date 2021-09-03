using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaController : MonoBehaviour
{
    private Dictionary<Coordinate, GameCell> _cellsByCoordinate;

    // Start is called before the first frame update
    void Start()
    {
        _cellsByCoordinate = GetComponent<PlayAreaSetupper>().InitializeGameCells();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
