using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 2f;

    private Transform _target;

    private void updateMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _movementSpeed * Time.deltaTime);
    }

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        updateMovement();
    }
}
