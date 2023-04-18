using UnityEngine;

using UnityEditor;
using UnityEngine.Events;
using System.Collections;
using Unity.VisualScripting;

public class CupheadStateInfo
{
    public static readonly int IS_NOT_MOVING = 0;
    public static readonly int IS_MOVING = 1;
}



public class CupheadController : MonoBehaviour
{

    [SerializeField]
    GameObject PlayerGameObject;

    public delegate void
        Event();

    GameManager gameManager;

    public static Animator PlayerAnimator;
    public static SpriteRenderer PlayerSpriteRenderer;
    public static Rigidbody2D playerRigidbody;

    [SerializeField]
    public static Animator _bulletSparkAnimator;

    [SerializeField]
    public float _playerMoveSpeed;

    [SerializeField]
    public Vector2 ExmoveBounceForce;

    private int playerHP = 3; 



    #region//각 동작을 한 번만 실행되도록 하기위해 아래와 같은 조건값을 사용합니다. 
    //플레이어 고유 상태 여부를 스태틱으로 저장했습니다.
    //다른 객체와 구별됨에 주의합니다. 
    public static bool IsDucking;
    public static bool IsJumping;
    public static bool HasParried;
    public static bool TryParrying;
    public static bool IsEXMoving;
    public static bool IsOnGround;
    public static bool IsJumpEXMoving;
    public static bool HasBeenHit;
    #endregion

    //인풋벡터 (스태틱으로 만들어 EXMOVE에서 정지 시 사용)
    static Vector2 _inputVec;
    bool parrySucceed;

    public AudioSource _audioSource;

    Try_Parrying_Behaviour try_Parrying_Behaviour;

    [SerializeField]
    Collider2D playerOnGround;
    ParrySuccessBehaviour parrySuccessBehaviour;



    private void OnEnable()
    {
        

    }
    private void Awake()
    {
        //플레이어 초기 방향 설정
        playerDirection = PLAYER_DIRECTION_RIGHT;
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidbody = GetComponent<Rigidbody2D>();
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


    /// <summary>
    /// 플레이어 이동을 위한 입력값을 받아 움직이는 함수입니다.
    /// </summary>
    public void MovePlayer()
    {
        _inputVec.x = Input.GetAxisRaw("Horizontal");
        _inputVec.y = Input.GetAxisRaw("Vertical");


        if (IsDucking == false)
        {
            playerRigidbody.velocity = new Vector2
           (_inputVec.x * _playerMoveSpeed, playerRigidbody.velocity.y);
        }

        FlipPlayer();

    }



    #region  플레이어의 이동방향에 맞게 애니메이터 스프라이터를 x축방향으로 플립합니다. 
    /// <summary>
    /// 플레이어의 이동방향에 맞게 애니메이터 스프라이터를 x축방향으로 플립합니다. 
    /// </summary>
    public static int playerDirection;
    public readonly static int PLAYER_DIRECTION_LEFT = 1;
    public readonly static int PLAYER_DIRECTION_RIGHT = 2;
    public void FlipPlayer()
    {


        if (_inputVec.x != 0f)
        {
            PlayerAnimator.SetBool(CupheadAnimID.IS_RUNNING, true);

            if (_inputVec.x < 0.0f)
            {
                PlayerSpriteRenderer.flipX = true;
                playerDirection = PLAYER_DIRECTION_LEFT;
            }
            else
            {
                PlayerSpriteRenderer.flipX = false;
                playerDirection = PLAYER_DIRECTION_RIGHT;
            }

        }
        else { PlayerAnimator.SetBool(CupheadAnimID.IS_RUNNING, false); }

    }
    #endregion 







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
   
    public void JumpPlayer()
    {

        if (Input.GetKeyDown(KeyCode.Z) && IsJumping == false)
        {
            PlayerAnimator.SetBool(CupheadAnimID.IS_JUMPING, true);
            IsJumping = true;

        }

        if (Input.GetKey(KeyCode.Z))
        {
            playerRigidbody.gravityScale = 1.0f;
        }

        else if (Input.GetKeyUp(KeyCode.Z) && playerRigidbody.velocity.y > 5.0f)
        {

            playerRigidbody.gravityScale = 2.5f;
        }
        //master first branch test
    }






    /// <summary>
    /// 애니메이션 이벤트로 실행되는 함수 입니다. 
    /// 패링 에니메이션이 끝나면 패링상태를 끝내줍니다.
    /// </summary>


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



    /// <summary>
    /// EXMOVE 발동 시 애니메이션 이벤트를 통해 작동 될 함수 입니다.
    /// 애니메이터 파라미터 값만 수정하고 나머지 행동은 FSM에서 구현됩니다. 
    /// </summary>
    Vector2 BounceVector;
    [SerializeField] float hitBounceForce;


 



    #region // 애니메이션 이벤트로 동작할 함수들의 목록입니다. 

    //AddForce에서 vel로 바꿀예정
    //public void AddForceRightAfterDefreezeExMove()
    //{
    //    playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
    //    if (playerDirection == PLAYER_DIRECTION_RIGHT)
    //    {
    //        playerRigidbody.AddForce(-ExmoveBounceForce, ForceMode2D.Impulse);
    //    }

    //    else
    //    {
    //        playerRigidbody.AddForce(ExmoveBounceForce, ForceMode2D.Impulse);
    //    }


    //}


    public void SetFalseTryParrying() => PlayerAnimator.SetBool(CupheadAnimID.TRY_PARRYING, false);
    public void SetFalseHasParried()
    {       
        PlayerAnimator.SetBool(CupheadAnimID.HAS_PARRIED, false);

    }
    public void SetFalseHasBeenHit()
    {
        PlayerAnimator.SetBool(CupheadAnimID.HAS_BEEN_HIT, false);
        StartCoroutine(DelayMakingHasBeenHitFalse());
    }

    [SerializeField]
    private float _parryingPauseTime = 0.3f;
    private float _elapsedTime = 0f;
    private float _pausedTime;


    public void PauseAndResumeGame()
    {
        StartCoroutine(ResumeGame());
        Time.timeScale = 0f;
       
    }
    IEnumerator ResumeGame()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1f;
    }

    public void TemporaryPause() => isPaused = true;
 

    //아래는 코루틴 함수 사용 부분입니다

    public static readonly WaitForSeconds _waitTime = new WaitForSeconds(0.8f);
    IEnumerator DelayMakingIsJumpEXMobingFalse()
    {
        yield return _waitTime;
        IsJumpEXMoving = false;
        IsEXMoving = false;

    }
    IEnumerator DelayMakingHasBeenHitFalse()
    {
        yield return _waitTime;
        HasBeenHit = false;

    }
    #endregion



    #region // 플레이어가, 플랫폼, 투사체등에 맞는 경우를 감지합니다. 
    private void OnTriggerStay2D(Collider2D collision)
    {


        if (HasBeenHitCollision(collision))
        {
            PlayerAnimator.SetBool(CupheadAnimID.HAS_BEEN_HIT, true);
        }

     

    }
    #endregion


    /// <summary>
    /// OnTriggerStay2D 내부 조건을 간단하게 만들기 위한 충돌감지 태그 함수 목록입니다. 
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private bool IsPlatformCollision(Collider2D collision)
    {
        return collision.CompareTag(LayerNames.PLATFORM);
    }

    /// <summary>
    /// 플레이어 데미지 피해 관련 함수 
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private bool HasBeenHitCollision(Collider2D collision)
    {
      
        if(HitParryableCollision(collision) && TryParrying)
        {
            return false;
        }

        if (collision.CompareTag(TagNames.PROJECTILE))
        {
            return true;
        }



        Collider2D[] childColliders
        = collision.gameObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D childCollider in childColliders)
        {
            if (childCollider.CompareTag(TagNames.PROJECTILE))
            {
                return true;
            }
        }


        return false;



    }

    private bool HitParryableCollision(Collider2D collision)
    {
        return collision.CompareTag(TagNames.PARRYABLE);
    }
    private bool isPaused = false;
    
   
  
    public static readonly WaitForSeconds _pauseTime = new WaitForSeconds(1);

    private void DecreaseHP() => playerHP -= 1;
    private void CheckPlayerAlive()
    {
        if (playerHP < 0)
        {
            PlayerAnimator.SetBool(CupheadAnimID.DIED, true);
        }
    }

}





