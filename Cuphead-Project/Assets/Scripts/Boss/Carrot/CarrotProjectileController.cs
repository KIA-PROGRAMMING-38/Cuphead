using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CarrotProjectileController : MonoBehaviour
{


    private static int CarrotProjectileHP = 4;
    Rigidbody2D carrotRigidbody;

    SpriteRenderer projectile;
    Animator _animator;
    [SerializeField]
    float _initialProjectileSpeed;


    new Collider2D collider;
    float _spawnMoveDistance;
    [SerializeField]
    GameObject _player;

    [SerializeField]
    float _playerTrackingSpeed;

    SpriteRenderer _spriteRenderer;

    [SerializeField]
    Collider2D DamageableCollider; 

    private bool died = false;
    private void Awake()
    {
        
    }
    private void Update()
    {



    }
    bool start = false;

    private void OnEnable()
    {

        died = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _waitTimeForMaterial = new WaitForSeconds(hitMaterialDurationTime);
        _animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        carrotRigidbody = GetComponent<Rigidbody2D>();
        carrotRigidbody.constraints = RigidbodyConstraints2D.None;
        carrotRigidbody.velocity = _initialProjectileSpeed * Vector2.down;
        carrotRigidbody.bodyType = RigidbodyType2D.Dynamic;

        if (!start)
        {
            start = true;
            Invoke(nameof(DeactivateDelay), 1f);
        }
        else
        {
            Invoke(nameof(DeactivateDelay), 20f); //
        }
       
    }

    private void FixedUpdate()
    {
     
        if (!died)//발사체가 터지기 전까지는 플레이어를 추격
        {
            RotateAndMoveProjectileTowardsPlayer();
        }

    }
    void DeactivateDelay() => gameObject.SetActive(false);

    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke(); //코루틴과 다르게 반드시 해제해주어야 합니다. 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (HasBeenHitCollision(collision))
        {
            FreezeAndDie();
        }
    }

    private bool HasBeenHitCollision(Collider2D collision)
    {
        return collision.CompareTag(TagNames.PLAYER);
    }

    public void ShowProjectile() => gameObject?.SetActive(true);
    public void HideProjectile() => gameObject?.SetActive(false);
    public void DeactivatieProjectileCollider() => collider.enabled = false;

    WaitForSeconds _waitTimeForMaterial;
    private static float hitMaterialDurationTime = 0.15f;
    [SerializeField] Material _MaterialDuringDamaged;
    [SerializeField] Material _defaultMaterial;


    private static void DecreaseHP() => CarrotProjectileHP -= 1;
    private void CheckCarrotAlive()
    {
        RotateAndMoveProjectileTowardsPlayer();
        if (CarrotProjectileHP < 0)
        {
            FreezeAndDie();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (IsBulletCollision(collision))
        {
            FreezeAndDie();
            DecreaseHP();
            CheckCarrotAlive();
            changeMaterial();
        }

    }

    public void changeMaterial()
    {
        _spriteRenderer.material = _MaterialDuringDamaged;
        StartCoroutine(TurnBackToOriginalMaterial());
    }


    IEnumerator TurnBackToOriginalMaterial()
    {
        yield return _waitTimeForMaterial;
        _spriteRenderer.material = _defaultMaterial;
    }
    private bool IsBulletCollision(Collider2D collision)
    {
        return collision.CompareTag(TagNames.BULLET);
    }

    void TurnOffBackround()
    {
        Deactivate();
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }


    [SerializeField]
    float rotationSpeed = 1f;

    Quaternion TargetRotation;
    public void RotateAndMoveProjectileTowardsPlayer()
    {
        //당근 투사체 실시간 회전구현 부분
        Vector3 difference = _player.transform.position - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        TargetRotation = Quaternion.Euler(0f, 0f, rotation_z + 90f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, rotationSpeed);


        //당근 투사체 실시간 트랙킹 부분
        carrotRigidbody.position = Vector2.Lerp
        (transform.position, _player.transform.position, _playerTrackingSpeed * Time.deltaTime);
    }


    public void FreezeAndDie()
    {
        //터지는 순간 더이상 플레이어를 공격 할 수 없기 때문에 콜라이더 끄기.
        DamageableCollider.enabled= false;
        GetComponent<Collider2D>().enabled = false;
        //더이상 플레이어를 향해 움직이거나 회전하지않음.
        died = true;
        carrotRigidbody.bodyType = RigidbodyType2D.Static;

        //Death 애니메이션 재생.
        _animator.SetBool(ProjectileAnimID.DEAD, true);
    }


    /// <summary>
    /// 포테이토와,어니언 사망시, 백그라운드(흙 구덩이)가 서서히 사라지도록 하는 함수입니다.
    /// 각 Material 을 받아와 서서히 투명도를 0으로 만들어줍니다. 
    /// </summary>
    /// 
  

}
