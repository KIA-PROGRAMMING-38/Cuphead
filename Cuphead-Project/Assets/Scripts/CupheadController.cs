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
    public Animator _bulletSparkAnimator;

    [SerializeField]
    public float _playerMoveSpeed;
    public static bool isDucking;
   
    [SerializeField]
    public float _exMoveWaitingTime;

   

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
        Shoot();
        ExMove();
        Parry();
    }
    private void LateUpdate()
    {
        FlipPlayer();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }




    Vector2 tempVector;
    /// <summary>
    /// 필살기를 쓰는 함수입니다. 
    /// 필살기를 쓰는 동안 일정시간 멈춰야 하기때문에
    /// 일시적으로 플레이어 이동을 멈추고 점프속도를 없애주는
    /// 코드를 작성했습니다. 
    /// </summary>
    public void ExMove()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            tempVector = new Vector2(0, -_playerRigidbody.velocity.y);
            StartCoroutine(DelayExMove());
            
            _animator.SetBool(CupheadAnimID.IS_EX_MOVING, true);
            _playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        IEnumerator DelayExMove()
        {
            yield return new WaitForSeconds(_exMoveWaitingTime);

            _animator.SetBool(CupheadAnimID.IS_EX_MOVING, false);
            _playerRigidbody.constraints = RigidbodyConstraints2D.None;
            _playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            _playerRigidbody.velocity = tempVector;


        }
    }
   
    /// <summary>
    /// 플레이어 이동을 위한 입력값을 받아 움직이는 함수입니다.
    /// </summary>
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
            _animator.SetBool(CupheadAnimID.IS_RUNNING, true);
        }
        else { _animator.SetBool(CupheadAnimID.IS_RUNNING, false); }

    }

    /// <summary>
    /// 플레이어를 숙이는(ducking) 함수입니다. 점프에 제약이 걸립니다.
    /// 제약은 FSM 파라미터값 설정으로 구현했습니다. 
    /// </summary>
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

    public static bool isJumping;
    [SerializeField]
    public Vector2 _jumpForce = new Vector2(0f, 17);

    private WaitForSeconds waitTime = new WaitForSeconds(2.0f);
    /// <summary>
    /// 점프를 단계별로 구현할 수 있도록, 키를 누르고있는 시간에 따라
    /// 점프높이가 달라지도록 구현했습니다. 
    /// </summary>
    ///  [SerializeField]
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
        //master first branch test

    }

    /// <summary>
    /// 패링을 작동하는 함수입니다. 점프시에만 동작할 수 있으며,
    /// 업데이트로 isJumping 불린 자료값을 OnGroundChecker.cs에서 반환해줍니다.
    /// 이 불값을 조건으로 패링을 실행할 지 여부를 정합니다. 
    /// </summary>
    public void Parry()
    {
        //점프 상태에서 한 번 더 누르면
        if(isJumping == true && Input.GetKeyDown(KeyCode.A)) 
        {
            // 패링상태 실행 
            _animator.SetBool(CupheadAnimID.IS_PARRYING, true);
            StartCoroutine(StopParry());
        }

        //패링 애니메이션 재생을 위해 일정 시간 뒤에 패링상태 중지
        IEnumerator StopParry()
        {
            yield return waitTime;
            _animator.SetBool(CupheadAnimID.IS_PARRYING, false);
        }
    }

    public void Shoot()
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


