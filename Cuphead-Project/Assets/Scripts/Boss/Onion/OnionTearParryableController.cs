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
        projectileRigidbody = GetComponent<Rigidbody2D>();// 직전 키네마틱 해제
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

       
        projectileRigidbody.gravityScale = Random.Range(0.1f, 0.3f);
      

        Invoke(nameof(DeactiveDelay), 2.0f);
   
    }
    void DeactiveDelay() => gameObject.SetActive(false);


    private void OnDisable()
    {
        projectileRigidbody.isKinematic = false;
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();
       
     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (HasHitPlayerCollision(collision) || HasHitGroundCollision(collision))
        {
            
            /* 플레이어 or 플랫폼과 충돌시 오브젝트를 멈추고 애니메이션 재생을
            위해 키네마틱을 true로 하고 정지*/
            projectileRigidbody.isKinematic = true;
            projectileRigidbody.velocity = Vector3.zero;
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
