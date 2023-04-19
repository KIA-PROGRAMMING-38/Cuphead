using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Transform _playerTransform;

    [SerializeField]
    float _playerTrackingSpeed;
    private static float hitMaterialDurationTime = 0.15f;

    SpriteRenderer _spriteRenderer;


    private void FixedUpdate()
    {
        carrotRigidbody.position = Vector2.Lerp
            (transform.position, _playerTransform.position, _playerTrackingSpeed * Time.deltaTime);
        transform.LookAt(_playerTransform);
    }
    private void OnEnable()
    {
        _spriteRenderer =GetComponent<SpriteRenderer>();
        _waitTimeForMaterial = new WaitForSeconds(hitMaterialDurationTime);
        _animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        carrotRigidbody = GetComponent<Rigidbody2D>();
        carrotRigidbody.velocity = _initialProjectileSpeed * Vector2.down;
        Invoke(nameof(DeactivateDelay), 5f); //

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
            _animator.SetBool(ProjectileAnimID.HIT_PLAYER, true);
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
   
    [SerializeField] Material _MaterialDuringDamaged;
    [SerializeField] Material _defaultMaterial;
 

    private static void DecreaseHP() => CarrotProjectileHP -= 1;
    private void CheckCarrotAlive()
    {
        if (CarrotProjectileHP < 0)
        {
            _animator.SetBool(CupheadAnimID.DIED, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (IsBulletCollision(collision))
        {
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
}
