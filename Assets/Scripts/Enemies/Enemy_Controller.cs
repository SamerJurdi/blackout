using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 2f;
    [SerializeField] float _coolDownMin = 4f;
    [SerializeField] float _coolDownMax = 10f;
    [SerializeField] int _attackTimer = 5;
    [SerializeField] Vector3 teleportPosition;

    private GameObject _player;
    private Transform _playerLocation;
    private bool isPlayerLooking = false;
    private bool isAttacking = false;

    private void updateMovement()
    {
        if (_playerLocation && !isPlayerLooking && isAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerLocation.position, _movementSpeed * Time.deltaTime);
        }
    }

    private IEnumerator delayNextAttack()
    {
        isAttacking = false;
        float delay = Random.Range(_coolDownMin, _coolDownMax);

        yield return new WaitForSeconds(delay);

        isAttacking = true;
    }

    private IEnumerator delayTheHunt()
    {
        yield return new WaitForSeconds(_attackTimer);
        isAttacking = true;
    }

    private void Start()
    {
        StartCoroutine(delayTheHunt());
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerLocation = _player.GetComponent<Transform>();
    }

    private void Update()
    {
        updateMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("LightBeam"))
        {
            isPlayerLooking = true;
            StartCoroutine(delayNextAttack());
        }
        if (other.gameObject == _player)
        {
            _player.transform.position = teleportPosition;
            Debug.Log("Player was teleported to the specified position.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("LightBeam"))
        {
            isPlayerLooking = false;
        }
    }
}
