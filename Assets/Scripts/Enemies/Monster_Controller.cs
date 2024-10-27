using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Controller : MonoBehaviour
{
    [SerializeField] string _deathSentence = "You dead!"; 
    [SerializeField] float _movementSpeed = 1f;
    [SerializeField] float _coolDownMin = 4f;
    [SerializeField] float _coolDownMax = 10f;
    [SerializeField] int _attackTimer = 5;
    [SerializeField] Animator _animator;
    [SerializeField] private AnimationClip _idleAnimationClip;
    [SerializeField] private AnimationClip _attackAnimationClip;
    [SerializeField] private AnimationClip _walkAnimationClip;
    [SerializeField] private AnimationClip _hideAnimationClip;

    private GameObject _player;
    private Transform _playerFound;
    private Level_Controller _levelController;

    private bool isPlayerLooking = false;
    private bool isAttacking = false;
    private bool isOnCoolDown = false;
    private bool isTransforming = false;
    private int coolDownCounter = 0;

    private int _animMonsterIdle;
    private int _animMonsterAttack;
    private int _animMonsterWalk;
    private int _animMonsterHide;

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
            _animator.CrossFade(_animMonsterHide, 0);
            isAttacking = false;
        }

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        isTransforming = false;
        _animator.CrossFade(_animMonsterIdle, 0);
    }
    private IEnumerator beginChaseTransition()
    {
        isTransforming = true;
        _animator.CrossFade(_animMonsterAttack, 0);

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        isTransforming = false;
        _animator.CrossFade(_animMonsterWalk, 0);
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
        _animMonsterIdle = Animator.StringToHash(_idleAnimationClip.name);
        _animMonsterAttack = Animator.StringToHash(_attackAnimationClip.name);
        _animMonsterWalk = Animator.StringToHash(_walkAnimationClip.name);
        _animMonsterHide = Animator.StringToHash(_hideAnimationClip.name);
        StartCoroutine(delayTheHunt());
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerFound = _player.GetComponent<Transform>();
        _levelController = GameObject.FindGameObjectWithTag("Level").GetComponent<Level_Controller>();
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
        if (other.gameObject == _player && isAttacking)
        {
            _levelController.endGame(_deathSentence, false);
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
