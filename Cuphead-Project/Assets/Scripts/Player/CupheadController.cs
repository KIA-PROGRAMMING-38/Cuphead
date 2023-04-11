using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEditor;

public class CupheadController : MonoBehaviour
{
    public static Animator PlayerAnimator;
    public static SpriteRenderer PlayerSpriteRenderer;
    public static Rigidbody2D PlayerRigidbody;

    [SerializeField]
    public static Animator _bulletSparkAnimator;

    [SerializeField]
    public float _playerMoveSpeed;
    public static bool IsDucking;
    public Vector2 _inputVec;

    [SerializeField]
    public float _exMoveWaitingTime;
    public AudioSource _audioSource;



    private void Awake()
    {
        //플레이어 초기 방향 설정
        playerDirection = RIGHT;

        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();

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
        ParryPlayer();
        MovePlayer();
    }
    private void LateUpdate()
    {
        FlipPlayer();
    }
    private void FixedUpdate()
    {

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
            tempVector = new Vector2(0, -PlayerRigidbody.velocity.y);
            StartCoroutine(DelayExMove());

            PlayerAnimator.SetBool(CupheadAnimID.IS_EX_MOVING, true);
            PlayerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        IEnumerator DelayExMove()
        {
            yield return new WaitForSeconds(_exMoveWaitingTime);

            PlayerAnimator.SetBool(CupheadAnimID.IS_EX_MOVING, false);
            PlayerRigidbody.constraints = RigidbodyConstraints2D.None;
            PlayerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            PlayerRigidbody.velocity = tempVector;


        }
    }

    /// <summary>
    /// 플레이어 이동을 위한 입력값을 받아 움직이는 함수입니다.
    /// </summary>
    public void MovePlayer()
    {
        _inputVec.x = Input.GetAxisRaw("Horizontal");
        _inputVec.y = Input.GetAxisRaw("Vertical");


        if (IsDucking == false)
        {
            PlayerRigidbody.velocity = new Vector2
           (_inputVec.x * _playerMoveSpeed, PlayerRigidbody.velocity.y);
        }

        FlipPlayer();

    }

    /// <summary>
    /// Flip player's image according to direction of the player's movement.
    /// </summary>
    public static bool playerDirection;
    public readonly static bool LEFT = false;
    public readonly static bool RIGHT = true;

    public void FlipPlayer()
    {

        if (_inputVec.x != 0f)
        {
            PlayerSpriteRenderer.flipX = _inputVec.x < 0.0f;

            if (_inputVec.x < 0.0f)
            {
                PlayerSpriteRenderer.flipX = true;
                PlayerAnimator.SetBool(CupheadAnimID.IS_RUNNING, true);
                playerDirection = LEFT;
            }
            else
            {
                PlayerSpriteRenderer.flipX = false;
                PlayerAnimator.SetBool(CupheadAnimID.IS_RUNNING, true);
                playerDirection = RIGHT;
            }

        }

        else
        {

            PlayerAnimator.SetBool(CupheadAnimID.IS_RUNNING, false);

        }

    }

    /// <summary>
    /// 플레이어를 숙이는(ducking) 함수입니다. 점프에 제약이 걸립니다.
    /// 제약은 FSM 파라미터값 설정으로 구현했습니다. 
    /// </summary>
    public void DuckPlayer()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            IsDucking = true;
            PlayerAnimator.SetBool(CupheadAnimID.IS_DUCKING, true);
            //_bulletSparkAnimator.SetBool(CupheadAnimID.IS_DUCKING, true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            IsDucking = false;
            PlayerAnimator.SetBool(CupheadAnimID.IS_DUCKING, false);
            //_bulletSparkAnimator.SetBool(CupheadAnimID.IS_DUCKING, false);
        }
    }

    public static bool isJumping;
    [SerializeField]
    public Vector2 _jumpForce = new Vector2(0f, 17);


    /// <summary>
    /// 점프를 단계별로 구현할 수 있도록, 키를 누르고있는 시간에 따라
    /// 점프높이가 달라지도록 구현했습니다. 
    /// </summary>
    ///  [SerializeField]
    public void JumpPlayer()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (IsOnGroundChecker.isOverlayed == true && IsDucking == false)
            {
                PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, _jumpForce.y);
            }

        }

        if (Input.GetKey(KeyCode.A))
        {

            PlayerRigidbody.gravityScale = 1.0f;
        }

        else if (Input.GetKeyUp(KeyCode.A) && PlayerRigidbody.velocity.y > 5.0f)
        {

            PlayerRigidbody.gravityScale = 2.5f;
        }
        //master first branch test

    }

    /// <summary>
    /// 패링을 작동하는 함수입니다. 점프시에만 동작할 수 있으며,
    /// 업데이트로 isJumping 불린 자료값을 OnGroundChecker.cs에서 반환해줍니다.
    /// 이 불값을 조건으로 패링을 실행할 지 여부를 정합니다. 
    /// </summary>
    /// 
    public static bool IsParrying;
    public void ParryPlayer()
    {

        //점프 상태에서 한 번 더 누르면
        if (isJumping == true && Input.GetKeyDown(KeyCode.A))
        {
            // 패링상태 실행 
            PlayerAnimator.SetBool(CupheadAnimID.IS_PARRYING, true);
            IsParrying = true;
        }


        //패링 애니메이션 재생을 위해 일정 시간 뒤에 패링상태 중지

    }

    
   



    public void Shoot()
    {
        if (Input.GetKey(KeyCode.X))
        {
            PlayerAnimator.SetBool(CupheadAnimID.IS_STANDSHOOTING, true);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            PlayerAnimator.SetBool(CupheadAnimID.IS_STANDSHOOTING, false);
        }


    }
}


