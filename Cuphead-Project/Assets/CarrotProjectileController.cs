using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotProjectileController : MonoBehaviour
{


  
    Rigidbody2D carrotRigidbody;

    SpriteRenderer projectile;
    Animator animator;
    [SerializeField]
    float _initialProjectileSpeed;

    
    new Collider2D collider;
    float _spawnMoveDistance;
    [SerializeField]
    Transform _playerTransform;

    [SerializeField]
    float _playerTrackingSpeed;


  
    private void FixedUpdate()
    {
        carrotRigidbody.position = Vector2.Lerp
            (transform.position, _playerTransform.position, _playerTrackingSpeed * Time.deltaTime);
    }
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
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
            animator.SetBool(ProjectileAnimID.HIT_PLAYER, true);
        }
    }

    private bool HasBeenHitCollision(Collider2D collision)
    {
        return collision.CompareTag(TagNames.PLAYER);
    }

    public void ShowProjectile() => gameObject?.SetActive(true);
    public void HideProjectile() => gameObject?.SetActive(false);
    public void DeactivatieProjectileCollider() => collider.enabled = false;
}
