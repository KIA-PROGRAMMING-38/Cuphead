using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CarrotProjectileController : MonoBehaviour
{


    private static int CarrotProjectileHP = 2;
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

    private bool died = false;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {


        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag(TagNames.PLAYER);
        }


    }


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
        Invoke(nameof(DeactivateDelay), 5f); //
        carrotRigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    private void FixedUpdate()
    {
     
        if (!died)//발사체가 터지기 전까지는 플레이어를 추격
        {
            RotateAndMoveProjectileTowardsPlayer();
        }

    }
    void DeactivateDelay() => gameObject.SetActive(false)
;
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


    public void RotateAndMoveProjectileTowardsPlayer()
    {
        Vector3 difference = _player.transform.position - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + 90f);
        carrotRigidbody.position = Vector2.Lerp
        (transform.position, _player.transform.position, _playerTrackingSpeed * Time.deltaTime);
    }


    public void FreezeAndDie()
    {
        died = true;

        carrotRigidbody.bodyType = RigidbodyType2D.Static;
        _animator.SetBool(ProjectileAnimID.DEAD, true);
    }

}
