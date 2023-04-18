using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoProjectile : MonoBehaviour
{

    [SerializeField]
    Rigidbody2D projectileRigidbody;

    SpriteRenderer projectile;
    Animator animator;
    [SerializeField]
    Vector2 projectileForce;

    BulletHitChecker bulletHitChecker;
    Collider2D collider;
    float _spawnMoveDistance;


    public void Update()
    {

    }
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        bulletHitChecker = GetComponent<BulletHitChecker>();
        projectileRigidbody.velocity = projectileForce;
        Invoke(nameof(DeactiveDelay), 1.5f);

    }

    void DeactiveDelay() => gameObject.SetActive(false)
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