using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stump_Monster_Controller : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 2f;
    [SerializeField] float _coolDownMin = 4f;
    [SerializeField] float _coolDownMax = 10f;
    [SerializeField] int _attackTimer = 5;
    [SerializeField] Animator _animator;

    private GameObject _player;
    private Transform _playerFound;

    private bool isPlayerLooking = false;
    private bool isAttacking = false;
    private bool isOnCoolDown = false;
    private bool isTransforming = false;
    private int coolDownCounter = 0;

    private readonly int _animStumpMonsterIdle = Animator.StringToHash("Anim_Stump_Monster_Idle");
    private readonly int _animStumpMonsterAttack = Animator.StringToHash("Anim_Stump_Monster_Attack");
    private readonly int _animStumpMonsterWalk = Animator.StringToHash("Anim_Stump_Monster_Walk");
    private readonly int _animStumpMonsterHide = Animator.StringToHash("Anim_Stump_Monster_Hide");

    private void updateMovement()
    {
        if (_playerFound && !isPlayerLooking && isAttacking && !isOnCoolDown && coolDownCounter == 0) {
            transform.position = Vector2.MoveTowards(transform.position, _playerFound.position, _movementSpeed * Time.deltaTime);
        }
        if (_playerFound && !isPlayerLooking && !isAttacking && !isOnCoolDown && !isTransforming && coolDownCounter == 0) {
            StartCoroutine(beginChaseTransition());
        }
    }

    private IEnumerator endChaseTransition()
    {
        isTransforming = true;
        if (isAttacking) {
            _animator.CrossFade(_animStumpMonsterHide, 0);
            isAttacking = false;
        }

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        isTransforming = false;
        _animator.CrossFade(_animStumpMonsterIdle, 0);
    }
    private IEnumerator beginChaseTransition()
    {
        isTransforming = true;
        _animator.CrossFade(_animStumpMonsterAttack, 0);

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        isTransforming = false;
        _animator.CrossFade(_animStumpMonsterWalk, 0);
        isAttacking = true;
    }

    private IEnumerator delayTheHunt()
    {
        isOnCoolDown = true;
        yield return new WaitForSeconds(_attackTimer);

        isOnCoolDown = false;
        StartCoroutine(beginChaseTransition());
    }
    private IEnumerator delayNextAttack()
    {
        float delay = Random.Range(_coolDownMin, _coolDownMax);
        coolDownCounter++;

        yield return new WaitForSeconds(delay);

        coolDownCounter--;
    }

    private void Start()
    {
        StartCoroutine(delayTheHunt());
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerFound = _player.GetComponent<Transform>();
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
            StartCoroutine(endChaseTransition());
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
            StartCoroutine(delayNextAttack());
        }
    }
}