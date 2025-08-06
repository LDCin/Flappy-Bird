using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float delayTime = 1.5f;
    [SerializeField] private float distanceSpawn = 1.0f;
    [SerializeField] private GameObject _pipe;
    [SerializeField] private PipePool _pipePool;
    private float _timer = 0;
    void Start()
    {
        SpawnPipe();
    }

    void Update()
    {
        if (_timer >= delayTime)
        {
            SpawnPipe();
            _timer = 0;
        }
        _timer += Time.deltaTime;
        
    }

    void SpawnPipe()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-distanceSpawn, distanceSpawn));
        for (int i = 0; i < _pipePool.GetUsedPoolSize(); i++)
        {
            Pipe pipe = _pipePool.GetPipe();
            pipe.transform.position = spawnPos;
        }
    }
}
