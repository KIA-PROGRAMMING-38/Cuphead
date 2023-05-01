using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotLaserController : MonoBehaviour
{

    [SerializeField]
    float _initialLaserSpeed;


    [SerializeField]
    Rigidbody2D _carrotEye;

    [SerializeField]
    float _laserSpeed;

    // 플레이어의 Y값은 지정값으로 고정합니다.
    //런타임 동안 고정되므로 readonly로 선언했습니다. 
    [SerializeField]
    readonly float PLAYER_ORIGINAL_POSITION_Y = -4f;


    Rigidbody2D laserRigidbody;
    SpriteRenderer laserSpriteRenderer;
    Animator animator;
    new Collider2D collider;

    private bool isDead; //플레이어 움직임 추적을 한번만 하도록 하기위한 bool 변수 설정 
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        isDead = false;
        laserSpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        laserRigidbody = GetComponent<Rigidbody2D>();
        Invoke(nameof(DeactivateDelay), 5f);
    }

    /*
       레이저는 플레이어의 X위치만을 추적하여 레이저를 발사합니다.
       Lerp를 사용하기에 Y가 변하면 속도에 영향을 줄 수 있으므로 
       플레이어의 y좌표값을 변하지 않도록 설정합니다. 
    */

    private void FixedUpdate()
    {
        if (!isDead)
        {
            ShootLaser();
        }

    }

    /// <summary>
    /// 캐롯의 눈에서 플레이어를 향해 레이저를 발사하는 함수입니다.
    /// Player.X값을 발사 직전 참조합니다. 
    /// 발사속도를 조정할 수 있습니다. 
    /// </summary>

    public static Vector2 _lastlyDetectedPlayerPosition;
    public void ShootLaser()
    {
        // 플레이어의 Y값은 고정합니다. 
        _lastlyDetectedPlayerPosition.y = PLAYER_ORIGINAL_POSITION_Y;

        //지면에 조금 더 확실히 닿을 수 있도록 외삽법 적용.
        Vector2 extrapolation = (_lastlyDetectedPlayerPosition - _carrotEye.position).normalized;
        laserRigidbody.position = Vector2.Lerp
        (transform.position, _lastlyDetectedPlayerPosition + (10 * extrapolation), _laserSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 일정시간이 지나면 자동적으로 리턴하도록 해주는 함수 입니다. 
    /// </summary>
    void DeactivateDelay() => gameObject.SetActive(false)
;
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke(); //코루틴과 다르게 반드시 해제해주어야 합니다. 
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        //지면과 닿았다면 ~
        if (HasBeenHitCollision(collision))
        {
            //레이저의 Death 애니메이션을 재생.
            animator.SetBool(ProjectileAnimID.DEAD, true);
            isDead = true;
        }
    }

    private bool HasBeenHitCollision(Collider2D collision)
    {  //지면과 닿는지 검사.
        return collision.CompareTag(TagNames.PLATFROM);
    }


    /// <summary>
    /// 애니메이션 이벤트로 특정 애니메이션에서, 
    /// 레이저객체를 Active,Inactive 상태로 만들어줍니다.
    /// </summary>
    public void ShowLaser() => gameObject?.SetActive(true);
    public void HideLaser() => gameObject?.SetActive(false);
    /// <summary>
    /// 플레이어와 부딫힌 경우 중복 데미지를 막기위해 콜라이더를 바로 꺼줍니다
    /// </summary>
    public void DeactivatieLaserCollider() => collider.enabled = false;
}
