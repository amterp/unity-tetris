using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OriginHandler))]
public class DimensionsHandler : MonoBehaviour, IOriginProvider
{
    public int NumberXCells;
    public int NumberYCells;
    public Action OriginChangeEvent { get => _originHandler.OriginChangeEvent; set => _originHandler.OriginChangeEvent = value; }

    public float XScale { get; private set; }
    public float YScale { get; private set; }
    public Vector3 CellScale { get; private set; }

    private OriginHandler _originHandler;

    void Awake()
    {
        XScale = transform.localScale.x / NumberXCells;
        YScale = transform.localScale.y / NumberYCells;
        CellScale = new Vector3(XScale, YScale);

        _originHandler = GetComponent<OriginHandler>();
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
        return _originHandler.GetX();
    }

    public float GetY()
    {
        return _originHandler.GetY();
    }

    public bool IsOriginChanging()
    {
        return _originHandler.IsOriginChanging();
    }
}
