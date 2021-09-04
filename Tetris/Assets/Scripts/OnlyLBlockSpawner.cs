using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyLBlockSpawner : MonoBehaviour, IBlockSpawner
{
    private BlockFactory _blockFactory;

    void Start()
    {
        _blockFactory = GetComponent<BlockFactory>();
    }

    public Block GetNextBlock()
    {
        return _blockFactory.CreateLBlock();
    }
}
