using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionTearParryableController : MonoBehaviour
{

    private Rigidbody2D tearParryableRigidbody;
    private SpriteRenderer tearParryableSpriteRenderer;
    private Animator tearParryableAnimator;
    private Collider2D tearParryableCollider;


   
    private void OnEnable()
    {
        tearParryableRigidbody = GetComponent<Rigidbody2D>();// 직전 키네마틱 해제
        tearParryableAnimator = GetComponent<Animator>();
        tearParryableCollider = GetComponent<Collider2D>();
        tearParryableRigidbody.gravityScale = Random.Range(0.1f, 0.3f);
      
        Invoke(nameof(DeactiveDelay), 2.0f);
   
    }
    void DeactiveDelay() => gameObject.SetActive(false);


    //OnEnable때 작성하면, Null에러가 발생할 위험이 있으므로, 
    //OnDisable에서 isKinematic을 false로 잡아줍니다. 
    private void OnDisable()
    {
        tearParryableRigidbody.isKinematic = false;
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();
       
     
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        // 플레이어 혹은, 지면과 충돌했다면~
        if (HasHitPlayerCollision(collision) || HasHitGroundCollision(collision))
        {
            
            /*오브젝트를 멈추고 Tear_Death애니메이션 재생하며, 
            위해 키네마틱을 true로 하고 속도를 0으로 하여 위치 고정*/
            tearParryableRigidbody.isKinematic = true;
            tearParryableRigidbody.velocity = Vector3.zero;
            tearParryableAnimator.SetBool(ProjectileAnimID.DEAD, true);
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
    public void DeactivateCollider() => tearParryableCollider.enabled = false;
}
