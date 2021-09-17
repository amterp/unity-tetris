using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerBuffer : MonoBehaviour, IBlockSpawner
{

    public event Action BufferUpdatedEvent;

    public GameObject BlockSpawnerContainer;
    public int BufferSize;

    private List<Block> _orderedBlockList;
    private IBlockSpawner _delegateBlockSpawner;

    void Awake()
    {
        _orderedBlockList = new List<Block>();
        _delegateBlockSpawner = BlockSpawnerContainer.GetComponent<IBlockSpawner>();
        GameState gameState = GoUtil.FindGameState();
        gameState.GameStartedEvent += OnGameStarted;
    }

    public List<Block> GetBlocksInBuffer()
    {
        return new List<Block>(_orderedBlockList);
    }

    public Block GetNextBlock()
    {
        PopulateBufferIfEmpty();

        Block nextBlock = _orderedBlockList[0];
        _orderedBlockList.RemoveAt(0);
        _orderedBlockList.Add(_delegateBlockSpawner.GetNextBlock());
        EventUtil.SafeInvoke(BufferUpdatedEvent);
        return nextBlock;
    }

    private void OnGameStarted()
    {
        _orderedBlockList.Clear();
        PopulateBuffer();
    }

    private void PopulateBufferIfEmpty()
    {
        if (_orderedBlockList.Count > 0) return;
        PopulateBuffer();
    }

    private void PopulateBuffer()
    {
        for (int i = 0; i < BufferSize; i++)
        {
            _orderedBlockList.Add(_delegateBlockSpawner.GetNextBlock());
        }
        EventUtil.SafeInvoke(BufferUpdatedEvent);
    }
}
