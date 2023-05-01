using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionTearParryableController : MonoBehaviour
{

<<<<<<< HEAD
    private Rigidbody2D tearParryableRigidbody;
    private SpriteRenderer tearParryableSpriteRenderer;
    private Animator tearParryableAnimator;
    private Collider2D tearDamageableCollider;
=======
    private Rigidbody2D projectileRigidbody;
    private SpriteRenderer projectile;
    private Animator animator;
    private Collider2D collider;
>>>>>>> 4a852836704d737aa2115b708427c05389db532a


   
    private void OnEnable()
    {
<<<<<<< HEAD
        tearParryableRigidbody = GetComponent<Rigidbody2D>();// 직전 키네마틱 해제
        tearParryableAnimator = GetComponent<Animator>();
        tearDamageableCollider = GetComponentInChildren<Collider2D>();
        tearParryableRigidbody.gravityScale = Random.Range(0.1f, 0.3f);
      
=======
        projectileRigidbody = GetComponent<Rigidbody2D>();// 직전 키네마틱 해제
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

       
        projectileRigidbody.gravityScale = Random.Range(0.1f, 0.3f);
      

>>>>>>> 4a852836704d737aa2115b708427c05389db532a
        Invoke(nameof(DeactiveDelay), 2.0f);
   
    }
    void DeactiveDelay() => gameObject.SetActive(false);


<<<<<<< HEAD
    //OnEnable때 작성하면, Null에러가 발생할 위험이 있으므로, 
    //OnDisable에서 isKinematic을 false로 잡아줍니다. 
    private void OnDisable()
    {
        tearParryableRigidbody.isKinematic = false;
=======
    private void OnDisable()
    {
        projectileRigidbody.isKinematic = false;
>>>>>>> 4a852836704d737aa2115b708427c05389db532a
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();
       
     
    }
<<<<<<< HEAD
    private void OnTriggerStay2D(Collider2D collision)
    {
        // 플레이어 혹은, 지면과 충돌했다면~
        if (HasHitPlayerCollision(collision) || HasHitGroundCollision(collision))
        {
            DeactivateCollider();
            /*오브젝트를 멈추고 Tear_Death애니메이션 재생하며, 
            위해 키네마틱을 true로 하고 속도를 0으로 하여 위치 고정*/
            tearParryableRigidbody.isKinematic = true;
            tearParryableRigidbody.velocity = Vector3.zero;
            tearParryableAnimator.SetBool(ProjectileAnimID.DEAD, true);
=======
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (HasHitPlayerCollision(collision) || HasHitGroundCollision(collision))
        {
            
            /* 플레이어 or 플랫폼과 충돌시 오브젝트를 멈추고 애니메이션 재생을
            위해 키네마틱을 true로 하고 정지*/
            projectileRigidbody.isKinematic = true;
            projectileRigidbody.velocity = Vector3.zero;
            animator.SetBool(ProjectileAnimID.DEAD, true);
>>>>>>> 4a852836704d737aa2115b708427c05389db532a
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
<<<<<<< HEAD
    public void DeactivateCollider() => tearDamageableCollider.enabled = false;
=======
    public void DeactivateCollider() => collider.enabled = false;
>>>>>>> 4a852836704d737aa2115b708427c05389db532a
}
