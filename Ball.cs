using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : NetworkBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public static event Action leftGoal;
    public static event Action rightGoal;

    public override void OnStartServer()
    {
        _rigidbody2D.simulated = true;
        _rigidbody2D.velocity = Vector2.right * _speed;
    }

    [ServerCallback]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            float x = _rigidbody2D.velocity.x > 0 ? -1 : 1;
            float y = (transform.position.y - collision.transform.position.y) / collision.collider.bounds.size.y;
            Vector2 direction = new Vector2(x, y).normalized;
            _rigidbody2D.velocity = direction * _speed;
        }
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out LeftTrigger leftTrigger))
        {
            leftGoal?.Invoke();
        }
        if (collision.TryGetComponent(out RightTrigger rightTrigger))
        {
            rightGoal?.Invoke();
        }
    }
}
