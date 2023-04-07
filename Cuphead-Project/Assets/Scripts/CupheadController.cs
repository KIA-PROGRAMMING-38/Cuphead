using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using TMPro;

public class CupheadController : MonoBehaviour
{
    public Animator _animator;
    private AudioSource _audioSource;
    private Vector2 _inputVec;

    SpriteRenderer _playerSpriteRenderer;
    Rigidbody2D _playerRigidbody;
    Vector2 size = new Vector2(10, 3);

    //[SerializeField]
    //private float _shortJump;

    //[SerializeField] 
    //private float _longJump;

    //[SerializeField] 
    //private float _shortJumpTimingLimit;

    [SerializeField]
    private float _playerMoveSpeed;


    private void Awake()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }
    private int PLATFORM_LAYER;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, size);
    }

    void Start()
    {
       
        Physics2D.OverlapBox(_playerRigidbody.position, size, 0);

        PLATFORM_LAYER = LayerMask.NameToLayer("Platform");
    }
    private void Update()
    {
        DuckPlayer();
        JumpPlayer();
        ShootStanding();
    }
    private void LateUpdate()
    {
        FlipPlayer();
        CheckRunning();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        
        
    }





    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == PLATFORM_LAYER || collision.gameObject.CompareTag("Platform"))
        {
            _animator.SetBool(CupheadAnimID.IS_JUMPING, false);
        }

    }

    private void MovePlayer()
    {
        _inputVec.x = Input.GetAxisRaw("Horizontal");
        _inputVec.y = Input.GetAxisRaw("Vertical");

        _playerRigidbody.velocity = new Vector2
            (_inputVec.x * _playerMoveSpeed, _playerRigidbody.velocity.y);

       

        FlipPlayer();

    }

    /// <summary>
    /// Flip player's image according to direction of the player's movement.
    /// </summary>
    private void FlipPlayer()
    {
        _playerSpriteRenderer.flipX = _inputVec.x < 0;
    }

    private void CheckRunning()
    {
        if (_inputVec.x != 0.0f)
        {
            _animator.SetBool(CupheadAnimID.IS_RUNNING, true);
        }

        if (Math.Abs(_inputVec.x) < 0.1f)
        {
            _animator.SetBool(CupheadAnimID.IS_RUNNING, false);
        }

    }

    private void DuckPlayer()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _animator.SetBool(CupheadAnimID.IS_DUCKING, true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _animator.SetBool(CupheadAnimID.IS_DUCKING, false);
        }
    }


    /// <summary>
    /// The player's jump has three fixed heights, 
    /// rather than a gradual increase in height. 
    /// </summary>
    private void JumpPlayer()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            _animator.SetBool(CupheadAnimID.IS_JUMPING, true);
        }

        if (Input.GetKey(KeyCode.A))
        {
           
            _playerRigidbody.gravityScale = 1.0f;
           
        }

        else if (Input.GetKeyUp(KeyCode.A) && _playerRigidbody.velocity.y > 5.0f)
        {
            _playerRigidbody.gravityScale = 2.5f;
        }


     
    }

    private void ShootStanding()
    {
        if (Input.GetKey(KeyCode.X))
        {
            _animator.SetBool(CupheadAnimID.IS_STANDSHOOTING, true);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            _animator.SetBool(CupheadAnimID.IS_STANDSHOOTING, false);
        }


    }
}


