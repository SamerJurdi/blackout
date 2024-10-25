using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 100f;
    [SerializeField] Rigidbody2D _rb;

    private Vector2 _moveDir = Vector2.zero;

    private void gatherInput()
    {
        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = Input.GetAxisRaw("Vertical");
    }

    private void updateMovement()
    {
        _rb.velocity = _moveDir * _movementSpeed * Time.fixedDeltaTime;
    }

    private void Update()
    {
        gatherInput();
    }
    private void FixedUpdate()
    {
        updateMovement();
    } 
}
