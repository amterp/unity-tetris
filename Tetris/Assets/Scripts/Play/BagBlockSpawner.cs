using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BlockFactory))]
public class BagBlockSpawner : MonoBehaviour, IBlockSpawner
{

    public int InstancesPerBag = 1;

    private BlockFactory _blockFactory;
    private List<Block> _blockBag = new List<Block>();

    void Awake()
    {
        _blockFactory = GetComponent<BlockFactory>();
    }

    public Block GetNextBlock()
    {
        if (_blockBag.Count == 0) RefillBag();

        int randomIndexToPickFrom = UnityEngine.Random.Range(0, _blockBag.Count);
        Block nextBlock = _blockBag[randomIndexToPickFrom];
        _blockBag.RemoveAt(randomIndexToPickFrom);

        return nextBlock;
    }

    private void RefillBag()
    {
        for (int i = 0; i < InstancesPerBag; i++)
        {
            _blockBag.Add(_blockFactory.CreateLineBlock());
            _blockBag.Add(_blockFactory.CreateLBlock());
            _blockBag.Add(_blockFactory.CreateJBlock());
            _blockBag.Add(_blockFactory.CreateSquareBlock());
            _blockBag.Add(_blockFactory.CreateSBlock());
            _blockBag.Add(_blockFactory.CreateZBlock());
            _blockBag.Add(_blockFactory.CreateTBlock());
        }
    }
}
