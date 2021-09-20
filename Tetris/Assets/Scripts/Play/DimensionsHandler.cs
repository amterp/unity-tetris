using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionsHandler : MonoBehaviour, IOriginProvider
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

        _originX = -transform.localScale.x / 2 + XScale / 2 + transform.position.x;
        _originY = transform.localScale.y / 2 - YScale / 2 + transform.position.y;
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
        Coordinate coordinate = new Coordinate(x, y, this, underlyingTransform);
        coordinate.UpdateTransform();
        return coordinate;
    }

    public int CalculateCenterOffsetX(BlockType blockType)
    {
        int halfwayXCoordinate = NumberXCells / 2;
        return halfwayXCoordinate - Mathf.CeilToInt(blockType.BoundingBoxDimensions().x / 2f);
    }

    public float GetX()
    {
        return _originX;
    }

    public float GetY()
    {
        return _originY;
    }
}
