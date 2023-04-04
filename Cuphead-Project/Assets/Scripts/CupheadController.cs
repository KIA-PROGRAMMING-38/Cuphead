using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class CupheadController : MonoBehaviour
{



    public Animator _animator;
    private AudioSource _audioSource;
    private Vector2 _inputVec;
    SpriteRenderer _playerSpriteRenderer;
    Rigidbody2D _playerRigidbody;

    private const float RUNNING = 1.0f;
    private const float IDLE = 0.0f;

    private Rigidbody2D _playerPosition;
    [SerializeField] private float _moveSpeed;

    private void Awake()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
         PLATFORM_LAYER = LayerMask.NameToLayer("Platform");
    }

    void Update()
    {
        // 실시간으로 반영하여, 자료 전달. 
        _playerPosition = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        FlipPlayer();
        CheckRunning();
    }

    private void FixedUpdate()
    {

        MovePlayer();
        Jump();
        Duck();
    }

    private int PLATFORM_LAYER;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == PLATFORM_LAYER)
        {
            _animator.SetBool(CupheadAnimID.IS_JUMPING, false);
        }

     
    }

    private void MovePlayer()
    {
        _inputVec.x = Input.GetAxisRaw("Horizontal");
       
        _playerRigidbody.velocity = new Vector2(_inputVec.x * _moveSpeed, _playerPosition.velocity.y);
        
        FlipPlayer();
        
    }

    private void FlipPlayer()
    {
        
        if (_inputVec.x != 0)
        {
            _playerSpriteRenderer.flipX = _inputVec.x < 0;
        }
    }

    private void CheckRunning()
    {
        if (Math.Abs(_inputVec.x) < 0.1f)
        {
            _animator.SetBool(CupheadAnimID.IS_RUNNING, false);
        }
        else if (Math.Abs(_inputVec.x) > 0.1f)
        {
            _animator.SetBool(CupheadAnimID.IS_RUNNING, true);
        }

        Debug.Log(_inputVec.x);
    }

    private void Duck()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _animator.SetBool(CupheadAnimID.IS_DUCKING, true);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _animator.SetTrigger(CupheadAnimID.IS_JUMPING);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}


