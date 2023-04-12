using UnityEngine;

using UnityEditor;
using UnityEngine.Events;
using System.Collections;

public class CupheadController : MonoBehaviour
{
    public delegate void ParryEvent();


    public static Animator PlayerAnimator;
    public static SpriteRenderer PlayerSpriteRenderer;
    public static Rigidbody2D PlayerRigidbody;

    [SerializeField]
    public static Animator _bulletSparkAnimator;

    [SerializeField]
    public float _playerMoveSpeed;

    //플레이어 고유 상태 여부를 스태틱으로 저장했습니다.
    public static bool IsDucking;
    public static bool IsJumping;
    public static bool IsParrying;
    public Vector2 _inputVec;
    public bool parrySucceed;

    [SerializeField]

    public float _exMoveWaitingTime;
    public AudioSource _audioSource;

    Try_Parrying_Behaviour try_Parrying_Behaviour;

     [SerializeField]
     Collider2D playerOnGround;
    ParrySuccessBehaviour parrySuccessBehaviour;
    private void Awake()
    {
       
        
        //플레이어 초기 방향 설정
        playerDirection = RIGHT;
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();

    }



    void Start()
    {
        parrySuccessBehaviour = new ParrySuccessBehaviour();
    }
    private void Update()
    {
        DuckPlayer();
        JumpPlayer();
        Shoot();
        ExMove();
        MovePlayer();
    }
    private void LateUpdate()
    {
        FlipPlayer();
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
        PlayerAnimator.SetBool(CupheadAnimID.IS_RUNNING, false);

        if (_inputVec.x != 0f)
        {
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





    /// <summary>
    /// 점프를 단계별로 구현할 수 있도록, 키를 누르고있는 시간에 따라
    /// 점프높이가 달라지도록 구현했습니다. 
    /// </summary>
    ///  [SerializeField]
    ///  
    public readonly int GET_KEY_COUNT_JUMP = 1;
    public readonly int GET_KEY_COUNT_PARRY = 2;
    public static int GetKeyCount = 0;
    public void JumpPlayer()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
         
          PlayerAnimator.SetBool(CupheadAnimID.IS_JUMPING, true);
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
   
   

    /// <summary>
    /// 애니메이션 이벤트로 실행되는 함수 입니다. 
    /// 패링 에니메이션이 끝나면 패링상태를 끝내줍니다.
    /// </summary>
    public void MakeIsParryingFalse()
    {
        Debug.Log("makeIsParryingFalse");
        IsParrying = false;
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

    
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlatformCollision(collision))
        {
            Debug.Log("Jump Off");
            PlayerAnimator.SetBool(CupheadAnimID.IS_JUMPING, false);
            PlayerAnimator.SetBool(CupheadAnimID.IS_PARRYING, false);
            IsJumping = false;
            Debug.Log(IsJumping);
        }
      
    }

    private bool IsPlatformCollision(Collider2D collision)
    {
        return collision.CompareTag(LayerNames.PLATFORM);
    }

   

    private void OnTriggerStay2D(Collider2D other)
    {
        

       
    }

  
   
  



    //public void OnSucceedParry()
    //{
    //    Debug.Log($"parrySucced?:{parrySucceed}");
    //    if (parrySucceed == true)
    //    {
    //        Debug.Log("Parry On");
    //        PlayerAnimator.SetBool(CupheadAnimID.HAS_PARRIED, true);
    //    }


    //}


}
 




