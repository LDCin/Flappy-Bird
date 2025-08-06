using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PipePool : MonoBehaviour
{
    [SerializeField] private int _poolSize = 100;
    private int _usedPoolSize = 10;
    [SerializeField] private Pipe[] _pipeList;
    [SerializeField] private Pipe _pipePrefab;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _pipeList = new Pipe[_poolSize];
        for (int i = 0; i < _usedPoolSize; i++)
        {
            Pipe pipe = Instantiate(_pipePrefab);
            _pipeList[i] = pipe;
            pipe.gameObject.SetActive(false);
        }
        
    }

    public Pipe GetPipe()
    {
        for (int i = 0; i < _usedPoolSize; i++)
        {
            if (!_pipeList[i].gameObject.activeSelf)
            {
                _pipeList[i].gameObject.SetActive(true);
                return _pipeList[i];
            }
        }
        _usedPoolSize++;
        Pipe newPipe = Instantiate(_pipePrefab);
        _pipeList[_usedPoolSize - 1] = newPipe; // xem lai
        return newPipe;
    }
    public int GetUsedPoolSize()
    {
        return _usedPoolSize;
    }
}
