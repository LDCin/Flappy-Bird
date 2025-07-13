using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 0.5f;
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * _moveSpeed);
    }
}
