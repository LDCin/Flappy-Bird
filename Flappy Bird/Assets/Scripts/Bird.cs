using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float _velocity = 1.5f;
    [SerializeField] private float _rotationSpeed = 10f;
    private bool _hasStarted = false;
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_hasStarted)
            {
                _hasStarted = true;
                GameManager.instance.PlayGame();
                GameManager.instance.HideTutorial();
            }
            _rb.velocity = Vector2.up * _velocity;
        }
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, _rb.velocity.y * _rotationSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.GameOver();
    }
}
