using UnityEngine;

using UnityEditor;
using UnityEngine.Events;
using System.Collections;
using Unity.VisualScripting;
using TMPro.Examples;




public class CupheadController : MonoBehaviour
{
    [SerializeField]
    public AudioSource SoundManager;
    [SerializeField]
    public AudioClip peashotClip;
 

    public delegate void CustomEventHandler();
    public static event CustomEventHandler HpControllEffect;



    // 이벤트 발생 함수
    public void HpChangedEvent()
    {
        HpControllEffect?.Invoke();
        // CustomEvent 이벤트 발생
    }

    // Try-Parrying 애니메이션에서도 간접적으로
    // Trigger 를 활용할 수 있도록하는 bool값입니다. 

    /// <summary>
    /// 기본공격 30개 = ExMove(필살기)1회
    /// ExMoveSpawner에서 Decrease, Bullet Oncollision에서 Increase함수 사용하여 접근
    /// </summary>
    public static int CurrentExMoveGauge=100;
    public static int ExMoveGaugeCountPerOne =50; // 피샷 몇개당 ExMOVE를 하나 줄건지
  

    // 플레이 후 통계 기록에서 보이는 화면 입니다. 
    public bool isParryTriggered { get; private set; }

    //PlayTime만 GameManager가 관리.
    //Record System 관리 목록
    public static int playerHP = 3;
    public static int HpBonus; //잔량계산하여 표출
    public static int ParrySucceedCount = 0;//parrySucessBehavior에서 증가
    public static int SuperMeter = 0; //peashotSpawner에서 계산
    
    public static string Grade = string.Empty;
    public static string ShowPreviousGrade = string.Empty;


    [SerializeField]
    GameObject PlayerGameObject;

    public delegate void
        Event();

    [SerializeField]
    GameManager GameManager;


  

    public static Animator PlayerAnimator;
    public static SpriteRenderer PlayerSpriteRenderer;
    public static Rigidbody2D playerRigidbody;

    [SerializeField]
    public Animator _bulletSparkAnimator;

    [SerializeField]
    public float _playerMoveSpeed;

    [SerializeField]
    public PeashotSpawner _peashotSpawner;






    #region//각 동작을 한 번만 실행되도록 하기위해 아래와 같은 조건값을 사용합니다. 
    //플레이어 고유 상태 여부를 스태틱으로 저장했습니다.
    //다른 객체와 구별됨에 주의합니다. 
    public static bool StopRunning;
    public static bool IsDucking;
    public static bool IsJumping;
    public static bool HasParried;
    public static bool TryParrying;
    public static bool IsShooting;
    public static bool IsEXMoving;
    public static bool IsOnGround;
    public static bool IsJumpEXMoving;
    public static bool HasBeenHit;
    public static bool IsInvincible;
    public static bool IsDashing;
    #endregion

    //인풋벡터 (스태틱으로 만들어 EXMOVE에서 정지 시 사용)
    public static Vector2 _inputVec;
    bool parrySucceed;

   
    [SerializeField]
    Collider2D playerOnGround;


    CupheadController _cupheadController;



    private  WaitForSeconds exmovingTime = new WaitForSeconds(1.0f);
    private  WaitForSeconds ParryTime = new WaitForSeconds(0.1f);
    private WaitForSeconds invincibleTime;
    private WaitForSeconds timeToWaitForIdle;
    private WaitForSeconds peashotDisableTime;
    private WaitForSeconds parryValidTime;
    private static readonly float INVINCILBE_TIME_FLOAT = 2.5f;
    private static readonly float PARRY_VALID_TIME = 0.5f;
    private static readonly float PEASHOT_DISABLE_TIME = 0.3f;
    private static readonly float  TIME_TO_WAIT_FOR_IDLE = 0.4f;//idle때 blinkPlayer함수 실행위함.
   

    private void Start()
    {
      
        parryValidTime = new WaitForSeconds(PARRY_VALID_TIME);
        _cupheadController.enabled = false;
        invincibleTime = new WaitForSeconds(INVINCILBE_TIME_FLOAT);
        peashotDisableTime = new WaitForSeconds(PEASHOT_DISABLE_TIME);

        //플레이어 초기 방향 설정.
        playerDirection = PLAYER_DIRECTION_RIGHT;
    }

    
  
    private void Awake()
    {
        
        _cupheadController = GetComponent<CupheadController>();
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// 플레이어의 인트로가 끝날때 플레이어를 재생시켜줍니다. 
    /// </summary>
    public void StartPlay()
    {
        GameManager.OnGameStart();

    }




    private void Update()
    {

        DuckPlayer();
        JumpPlayer();
        Shoot();
        StopPlayerRunning();
        MovePlayer();
        DashPlayer();
    }
    private void LateUpdate()
    {
        FlipPlayer();
    }





    /// <summary>
    /// 플레이어 이동을 위한 입력값을 받아 움직이는 함수입니다.
    /// </summary>
    public void MovePlayer()
    {
       
            _inputVec.x = Input.GetAxisRaw("Horizontal");
            _inputVec.y = Input.GetAxisRaw("Vertical");


        if (StopRunning == false && HasBeenHit == false && IsDucking == false)
        {
            playerRigidbody.velocity = new Vector2 //  플레이어를 움직여줌 
           (_inputVec.x * _playerMoveSpeed, playerRigidbody.velocity.y);

            if (playerRigidbody.velocity != Vector2.zero)
            {
                PlayerAnimator.SetBool(CupheadAnimID.RUN, true);
            }


            FlipPlayer();
        }

    }

    public void StopPlayerRunning()
    {
        if (Input.GetKeyDown(KeyCode.C) && !IsJumping)
        {
            playerRigidbody.velocity = Vector2.zero;
            
            StopRunning = true;
            PlayerAnimator.SetBool(CupheadAnimID.RUN, false);
            PlayerAnimator.SetBool(CupheadAnimID.STOP_MOVING, true);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            PlayerAnimator.SetBool(CupheadAnimID.STOP_MOVING, false);
          
            StopRunning = false;
        }
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
        else { PlayerAnimator.SetBool(CupheadAnimID.RUN, false); }

    }
    #endregion 







    /// <summary>
    /// 플레이어를 숙이는(ducking) 함수입니다. 점프에 제약이 걸립니다.
    /// 제약은 FSM 파라미터값 설정으로 구현했습니다. 
    /// </summary>
    public void DuckPlayer()
    {
        if (Input.GetKey(KeyCode.DownArrow) && !StopRunning && !IsJumping)
        {
            playerRigidbody.velocity = Vector3.zero;
            IsDucking = true;
            PlayerAnimator.SetBool(CupheadAnimID.DUCK, true);

        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            IsDucking = false;
            PlayerAnimator.SetBool(CupheadAnimID.DUCK, false);

        }
    }





    /// <summary>
    /// 점프를 단계별로 구현할 수 있도록, 키를 누르고있는 시간에 따라
    /// 점프높이가 달라지도록 구현했습니다. 
    /// </summary>
    ///  [SerializeField]
    ///   

    [SerializeField]
    public AudioSource JumpAudioSource;

    [SerializeField]
    public AudioClip jumpClip;

    public void JumpPlayer()
    {

        if (Input.GetKeyDown(KeyCode.Z) && IsJumping == false)
        {
            PlayerAnimator.SetBool(CupheadAnimID.JUMP, true);
            IsJumping = true;
            JumpAudioSource.clip = jumpClip;
            JumpAudioSource.PlayOneShot(jumpClip);

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
    public bool isPlaying = false;

    public void Shoot()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (isPlaying == false)
            {
                isPlaying = true;
                SoundManager.clip = peashotClip;
                SoundManager.Play();
            }
           
            IsShooting = true;
            PlayerAnimator.SetBool(CupheadAnimID.SHOOT, true);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            isPlaying = false;
            SoundManager.Stop();

            IsShooting = false;
            PlayerAnimator.SetBool(CupheadAnimID.SHOOT, false);

        }


    }




    Vector2 BounceVector;
    [SerializeField] float hitBounceForce;






    #region // 애니메이션 이벤트로 동작할 함수들의 목록입니다. 



    public void SetFalseTryParrying() => PlayerAnimator.SetBool(CupheadAnimID.TRY_PARRYING, false);
    public void SetFalseHasParried()
    {
        PlayerAnimator.SetBool(CupheadAnimID.HAS_PARRIED, false);

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


    /// <summary>
    /// 패링 성공시 극적인 표현을 위해 잠시
    /// 게임플레이를 멈춥니다.
    /// 애니메이션 이벤트로 작동합니다. 
    /// </summary>
    private bool isPaused = false;
    public void TemporaryPause() => isPaused = true;


    //아래는 코루틴 함수 사용 부분입니다
    //플레이어 무적상태를 구현하기 위해 작성했습니다.



    [SerializeField]
    AudioClip _hasParriedSound;
    private bool isParrySoundPlayed;

    #region // 플레이어가, 플랫폼, 투사체,보스,등에 맞는 경우를 감지합니다. 
    private void OnTriggerStay2D(Collider2D collision)
    {
        //패링시도중 패링투사체 충돌 시
        if (IsParryableObjectCollision(collision))
        {
           
            if(isParrySoundPlayed == false && TryParrying)
            {
                SoundManager.clip = _hasParriedSound;
                SoundManager.PlayOneShot(_hasParriedSound);
                isParrySoundPlayed = true;
            }

            isParryTriggered = true;

            
            PlayerAnimator.SetBool(CupheadAnimID.HAS_PARRIED, true);
            PlayerAnimator.SetBool(CupheadAnimID.HAS_BEEN_HIT, false);

            TryParrying = false;

            StartCoroutine(DelayMakingParrySucceedFalse());
        }
        else
        {
            isParryTriggered = false;
        }

        //플레이어가 충돌체에 맞은 경우
        if (HasBeenHitCollision(collision) && isParryTriggered == false)
        {
            if (IsInvincible == false && HasBeenHit == false)
            {
                PlayerAnimator.SetBool(CupheadAnimID.HAS_BEEN_HIT, true);

            }

        }
    }


    IEnumerator DelayMakingIsJumpEXMobingFalse()
    {
        yield return exmovingTime;
        IsJumpEXMoving = false;
        IsEXMoving = false;

    }

    /// <summary>
    /// 맞을때 작동할 함수입니다. 
    /// </summary>
    public void OnHasBeenHit()
    {

        //플레이어 이동 제약에 사용
        

        PeashotSpawner peashotSpawner = GetComponent<PeashotSpawner>();
        peashotSpawner.enabled = false;
      
        StartCoroutine(DelayMakingParrySucceedFalse());
        StartCoroutine(DelayMakingIsJumpEXMobingFalse());

        IsInvincible = true;
        //플레이어에게 맞은 상태면 bool값으로 인해, 논리적으로 일정시간 중복데미지 X
        StartCoroutine(SetInvincibleAndDelayMakingHasBeenHitFalse());

        //패링에 실패했는데 불구하고, 패링객체를 만나서 중복처리되는 경우를 방지.
        StartCoroutine(DelayMakingParrySucceedFalse());

        //맞을경우 일정시간 동안 총알을 발사 할 수 없음
        StartCoroutine(EnableScriptAfterDelay(1.0f));

        //플레이어 무적상태표시 
        StartCoroutine(BlinkPlayerCoroutine(2.2f, 0.23f));
    }




    IEnumerator SetInvincibleAndDelayMakingHasBeenHitFalse()
    {
        yield return invincibleTime;

        isParryTriggered = false;
        PlayerAnimator.SetBool(CupheadAnimID.HAS_PARRIED, false);
        PlayerAnimator.SetBool(CupheadAnimID.HAS_BEEN_HIT, false);
        HasBeenHit = false;

    }

    IEnumerator EnableScriptAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        PeashotSpawner peashotSpawner = GetComponent<PeashotSpawner>();
        peashotSpawner.enabled = true;
    }

    IEnumerator BlinkPlayerCoroutine(float blinkTime, float blinkInterval)
    {
        yield return timeToWaitForIdle;
        Material playerMaterial = GetComponent<Renderer>().material;
        Color color = playerMaterial.color;

        float startTime = Time.time;

        while (Time.time < startTime + blinkTime)
        {
            color.a = 0.6f;
            playerMaterial.color = color;

            yield return new WaitForSeconds(blinkInterval);

            color.a = 1.0f;
            playerMaterial.color = color;

            yield return new WaitForSeconds(blinkInterval);
        }

        color.a = 1.0f;
        playerMaterial.color = color;
        IsInvincible = false;
    }

    /// <summary>
    /// 패링성공 직후에만 패리상태로 변할 수 있도록 합니다.
    /// </summary>
    /// <returns></returns>


    // 0.0f에서 1.0f 사이의 값을 지정합니다. 1.0f이 완전 불투명, 0.0f이 완전 투명입니다.

    IEnumerator DelayMakingParrySucceedFalse()
    {


        yield return parryValidTime;
        PlayerAnimator.SetBool(CupheadAnimID.HAS_PARRIED, false);
        isParryTriggered = false;
        isParrySoundPlayed = false;

    }

    /// <summary>
    /// 플레어어가 물체에 맞은 경우 잠시 무적상태임을 나타내기위해 
    /// 플레이어 애니메이션(Sprite)의 alpha값을 조절하여 깜박이는 함수입니다. 
    /// </summary>

    float _elapsedTimeForBlinkPlayer;
    float _timeForTransparentSprite;
    float _timeForNormalSprite;


   
    /// <summary>
    /// 일정시간 동안, 플레이어를 무적상태로 만들어주는 함수 입니다.
    /// 무적상태 일 동안, 플레이어의 Material은 깜박입니다. 
    /// </summary>
    /// <returns></returns>

    #endregion



    #endregion


    /// <summary>
    /// OnTriggerStay2D 내부 조건을 간단하게 만들기 위한 충돌감지 태그 함수 목록입니다. 
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    /// 
    private bool IsParryableObjectCollision(Collider2D collision)
    {
        return collision.CompareTag(TagNames.PARRYABLE);
    }
    private bool IsPlatformCollision(Collider2D collision)
    {
        return collision.CompareTag(LayerNames.PLATFORM);
    }

    private Coroutine peashotDisableCoroutine;
    IEnumerator TemporarilyDisablePeashotSpawner()
    {

        yield return peashotDisableTime;

       
        _peashotSpawner.enabled = true;
        

    }
    [SerializeField] AudioClip _dashSound;
    private void DashPlayer()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
           
            PlayerAnimator.SetBool(CupheadAnimID.DASH,true);

            SoundManager.PlayOneShot(_dashSound);

            // 이전에 실행된 코루틴이 있다면 중지.
            if (peashotDisableCoroutine != null)
                StopCoroutine(peashotDisableCoroutine);

            _peashotSpawner.enabled = false;
            peashotDisableCoroutine = StartCoroutine(TemporarilyDisablePeashotSpawner());
        }

        // TemporarilyDisablePeashotSpawner()를 실행.
       
      
    }

    /// <summary>
    /// 플레이어 데미지 피해 관련 함수 
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private bool HasBeenHitCollision(Collider2D collision)
    {
        if (collision.CompareTag(TagNames.PROJECTILE) ||
            collision.CompareTag(TagNames.ENEMY))
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






    public static readonly WaitForSeconds _pauseTime = new WaitForSeconds(1);
   
    [SerializeField] AudioClip _playerDiedAudioClip;
    private void DecreaseHP() 
    {
        HpChangedEvent();
        CheckPlayerAlive();
        playerHP -= 1;
        Debug.Log("decrease");
        
       
    } 
    private void CheckPlayerAlive()
    {
        if (playerHP <= 0)
        {
            SoundManager.clip = _playerDiedAudioClip;
            SoundManager.PlayOneShot(_playerDiedAudioClip);
            PlayerAnimator.SetBool(CupheadAnimID.DIED, true);
            _cupheadController.enabled = false;

        }
    }

}





