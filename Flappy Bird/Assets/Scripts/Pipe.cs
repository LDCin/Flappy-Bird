using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 0.5f;
    [SerializeField] private float _existTime = 10f;
    private float _currentTimeExist = 10f;
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * _moveSpeed);
        _currentTimeExist--;
        if (_currentTimeExist == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
