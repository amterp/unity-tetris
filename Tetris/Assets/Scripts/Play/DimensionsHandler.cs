using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionsHandler : MonoBehaviour
{
    public int NumberXCells;
    public int NumberYCells;

    public float XScale { get; private set; }
    public float YScale { get; private set; }
    public Vector3 CellScale { get; private set; }

    private float _originX;
    private float _originY;

    void Awake()
    {

        XScale = transform.localScale.x / NumberXCells;
        YScale = transform.localScale.y / NumberYCells;
        CellScale = new Vector3(XScale, YScale);

        _originX = -transform.localScale.x / 2 + XScale / 2;
        _originY = transform.localScale.y / 2 - YScale / 2;
    }

    public Vector3 GetCellScale()
    {
        return CellScale;
    }

    public Coordinate CreateCoordinate(int x, int y)
    {
        return CreateCoordinate(x, y, null);
    }

    public Coordinate CreateCoordinate(int x, int y, Transform underlyingTransform)
    {
        Coordinate coordinate = new Coordinate(x, y, _originX, _originY, underlyingTransform);
        coordinate.UpdateTransform();
        return coordinate;
    }
}
