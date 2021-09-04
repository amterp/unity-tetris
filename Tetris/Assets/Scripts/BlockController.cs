using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    private IBlockSpawner _blockSpawner;
    private Block _currentBlock;

    // Start is called before the first frame update
    void Start()
    {
        _blockSpawner = GetComponent<IBlockSpawner>();
    }

    void Update()
    {
        SpawnBlockIfNone();
    }

    private void SpawnBlockIfNone()
    {
        if (_currentBlock != null) return;

        _currentBlock = _blockSpawner.GetNextBlock();
    }
}
