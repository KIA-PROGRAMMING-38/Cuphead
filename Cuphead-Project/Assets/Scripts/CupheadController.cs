using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using TMPro;

public class CupheadController : MonoBehaviour
{
    public Animator _animator;
    public AudioSource _audioSource;
    public Vector2 _inputVec;


    public static SpriteRenderer _playerSpriteRenderer;
    Rigidbody2D _playerRigidbody;

    [SerializeField]
    Animator _bulletSparkAnimator;

    [SerializeField]
    public float _playerMoveSpeed;
    public static bool isDucking;

    [SerializeField]
    public float _exMoveWaitingTime;

    RigidbodyConstraints2D _playerRigidbodyConstraints;

    private void Awake()
    {
       
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }
    private int PLATFORM_LAYER;


    void Start()
    {
        PLATFORM_LAYER = LayerMask.NameToLayer("Platform");
    }
    private void Update()
    {
        DuckPlayer();
        JumpPlayer();
        ShootStanding();
        ExMove();
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
    Vector2 tempVector;
    public void ExMove()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            tempVector = new Vector2(0, _playerRigidbody.velocity.y);
            
            _animator.SetBool(CupheadAnimID.IS_EX_MOVING, true);
            _playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
            //gameObject.GetComponent<CupheadController>().enabled = false;
            StartCoroutine(DelayExMove());
        }

        IEnumerator DelayExMove()
        {
            yield return new WaitForSeconds(_exMoveWaitingTime);
            _animator.SetBool(CupheadAnimID.IS_EX_MOVING, false);
           // gameObject.GetComponent<CupheadController>().enabled = true;
            _playerRigidbody.constraints = RigidbodyConstraints2D.None;
            _playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            tempVector = new Vector2(_inputVec.x * _playerMoveSpeed, -_playerRigidbody.velocity.y);



        }
    }
   
    public void MovePlayer()
    {
        _inputVec.x = Input.GetAxisRaw("Horizontal");
        _inputVec.y = Input.GetAxisRaw("Vertical");


        if (isDucking == false)
        {
            _playerRigidbody.velocity = new Vector2
           (_inputVec.x * _playerMoveSpeed, _playerRigidbody.velocity.y);
        }




        FlipPlayer();

    }

    /// <summary>
    /// Flip player's image according to direction of the player's movement.
    /// </summary>

    public void FlipPlayer()
    {
        if (_inputVec.x != 0f)
        {
            _playerSpriteRenderer.flipX = _inputVec.x < 0.0f;
        }

    }

    public void CheckRunning()
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
    public void DuckPlayer()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            isDucking = true;
            _animator.SetBool(CupheadAnimID.IS_DUCKING, true);
            //_bulletSparkAnimator.SetBool(CupheadAnimID.IS_DUCKING, true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isDucking = false;
            _animator.SetBool(CupheadAnimID.IS_DUCKING, false);
            //_bulletSparkAnimator.SetBool(CupheadAnimID.IS_DUCKING, false);
        }
    }
    /// <summary>
    /// The player's jump has three fixed heights, 
    /// rather than a gradual increase in height. 
    /// </summary>
    [SerializeField]
    Vector2 _jumpForce = new Vector2(0f, 500);
    public void JumpPlayer()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (IsOnGroundChecker.isOnGround == true && isDucking == false)
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _jumpForce.y);
            }
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

    public void ShootStanding()
    {
        if (Input.GetKey(KeyCode.X))
        {
            _animator.SetBool(CupheadAnimID.IS_STANDSHOOTING, true);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            _animator.SetBool(CupheadAnimID.IS_STANDSHOOTING, false);
            _bulletSparkAnimator.SetBool(BulletAnimID.IS_LAUNCHED, false);
        }


    }
}


