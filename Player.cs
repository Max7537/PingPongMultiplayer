using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private float _speed = 1500;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            _rigidbody.velocity = new Vector2(0, Input.GetAxis("Vertical") * Time.fixedDeltaTime * _speed);
        }
    }
}
