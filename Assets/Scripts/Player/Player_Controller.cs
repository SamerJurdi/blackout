using System;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 100f;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;

    private Vector2 _moveDir = Vector2.zero;
    private Vector3 _mousePosition = Vector3.zero;
    private Vector3 _playerPosition = Vector3.zero;
    private enum Directions {UP, DOWN, LEFT, RIGHT};
    private Directions _facingDirection = Directions.DOWN;

    private readonly int _animWalkRight = Animator.StringToHash("Anim_Player_Walk_Right");
    private readonly int _animWalkUp = Animator.StringToHash("Anim_Player_Walk_Up");
    private readonly int _animWalkDown = Animator.StringToHash("Anim_Player_Walk_Down");
    private readonly int _animIdleDown = Animator.StringToHash("Anim_Player_Idle_Down");
    private readonly int _animIdleUp = Animator.StringToHash("Anim_Player_Idle_Up");
    private readonly int _animIdleRight = Animator.StringToHash("Anim_Player_Idle_Right");

    private void gatherInput()
    {
        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = Input.GetAxisRaw("Vertical");
        _mousePosition = Input.mousePosition;
        _playerPosition = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void updateMovement()
    {
        _rb.velocity = _moveDir.normalized * _movementSpeed * Time.fixedDeltaTime;
    }
    private void updateFacingDirection()
    {
        switch (_moveDir)
        {
            case var dir when dir.x > 0:
                _facingDirection = Directions.RIGHT;
                break;
            case var dir when dir.x < 0:
                _facingDirection = Directions.LEFT;
                break;
            case var dir when dir.y > 0:
                _facingDirection = Directions.UP;
                break;
            case var dir when dir.y < 0:
                _facingDirection = Directions.DOWN;
                break;
            default:
                _facingDirection = getPoniterDirection();
                break;
        }
    }
    private Directions getPoniterDirection()
    {
        switch (_mousePosition - _playerPosition)
        {
            case var dir when dir.x > 0 && Math.Abs(dir.x) > Math.Abs(dir.y):
                return Directions.RIGHT;
            case var dir when dir.x < 0 && Math.Abs(dir.x) > Math.Abs(dir.y):
                return Directions.LEFT;
            case var dir when dir.y > 0 && Math.Abs(dir.x) < Math.Abs(dir.y):
                return Directions.UP;
            case var dir when dir.y < 0 && Math.Abs(dir.x) < Math.Abs(dir.y):
                return Directions.DOWN;
            default:
                return Directions.DOWN;
        }
    }
    private void updateAnimation()
    {
        if (_moveDir.SqrMagnitude() == 0)
        {
            switch (_facingDirection)
            {
                case Directions.RIGHT:
                    _spriteRenderer.flipX = false;
                    _animator.CrossFade(_animIdleRight, 0);
                    break;
                case Directions.LEFT:
                    _spriteRenderer.flipX = true;
                    _animator.CrossFade(_animIdleRight, 0);
                    break;
                case Directions.UP:
                    _animator.CrossFade(_animIdleUp, 0);
                    break;
                default:
                    _animator.CrossFade(_animIdleDown, 0);
                    break;

            }
        } else {
            switch (_facingDirection)
            {
                case Directions.RIGHT:
                    _spriteRenderer.flipX = false;
                    _animator.CrossFade(_animWalkRight, 0);
                    break;
                case Directions.LEFT:
                    _spriteRenderer.flipX = true;
                    _animator.CrossFade(_animWalkRight, 0);
                    break;
                case Directions.UP:
                    _animator.CrossFade(_animWalkUp, 0);
                    break;
                default:
                    _animator.CrossFade(_animWalkDown, 0);
                    break;

            }
        }
    }
    
    private void Update()
    {
        gatherInput();
        updateFacingDirection();
    }
    private void FixedUpdate()
    {
        updateMovement();
        updateAnimation();
    } 
}
