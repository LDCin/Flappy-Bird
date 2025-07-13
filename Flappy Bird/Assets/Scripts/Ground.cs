using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private float _loopSpeed = 2f;
    [SerializeField] private float _width = 6f;
    private Vector2 _startSize;
    private SpriteRenderer _sr;
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _startSize = new Vector2(_sr.size.x, _sr.size.y);
    }

    void Update()
    {
        _sr.size = new Vector2(_sr.size.x + _loopSpeed * Time.deltaTime, _sr.size.y);
        if (_sr.size.x > _width)
        {
            _sr.size = _startSize;
        }
    }
}
