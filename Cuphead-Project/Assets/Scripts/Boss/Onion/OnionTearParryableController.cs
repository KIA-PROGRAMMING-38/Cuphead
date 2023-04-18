using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionTearParryableController : MonoBehaviour
{

    private Rigidbody2D projectileRigidbody;

    private SpriteRenderer projectile;
    private Animator animator;
    private Collider2D collider;



    private void OnEnable()
    {
        projectileRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        Invoke(nameof(DeactiveDelay), 1.0f);
    }
    void DeactiveDelay() => gameObject.SetActive(false);

    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();
        projectileRigidbody.gravityScale = Random.Range(0.1f, 0.4f);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (HasHitPlayerCollision(collision) || HasHitGroundCollision(collision))
        {
            animator.SetBool(ProjectileAnimID.DEAD, true);
        }
    }

    // 양파눈물은 플레이어 혹은 플랫폼과 상호작용합니다. 
    private bool HasHitPlayerCollision(Collider2D collision)
    {
        return collision.CompareTag(TagNames.PLAYER);
    }

    private bool HasHitGroundCollision(Collider2D collision)
    {
        return collision.CompareTag(TagNames.PLATFROM);
    }

    public void ShowTear() => gameObject?.SetActive(true);
    public void HideTear() => gameObject?.SetActive(false);
    public void DeactivateCollider() => collider.enabled = false;
}
