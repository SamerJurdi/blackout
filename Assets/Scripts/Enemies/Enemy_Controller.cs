using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 2f;
    [SerializeField] float _coolDownMin = 1f;
    [SerializeField] float _coolDownMax = 5f;

    private GameObject _player;
    private Transform _playerLocation;
    private bool isPlayerLooking = false;
    private bool isCoolDownActive = false;

    private void updateMovement()
    {
        if (_playerLocation && !isPlayerLooking && !isCoolDownActive) {
            transform.position = Vector2.MoveTowards(transform.position, _playerLocation.position, _movementSpeed * Time.deltaTime);
        }
    }
    private IEnumerator delayNextAttack()
    {
        isCoolDownActive = true;
        float delay = Random.Range(_coolDownMin, _coolDownMax);

        yield return new WaitForSeconds(delay);

        isCoolDownActive = false;
    }

    private void Start()
    {
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
            Destroy(other.gameObject);
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
