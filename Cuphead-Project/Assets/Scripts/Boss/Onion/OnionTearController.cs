
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionTearController : MonoBehaviour
{


    Rigidbody2D projectileRigidbody;
    SpriteRenderer projectile;
    Animator animator;
    Collider2D collider;



    private void OnEnable()
    {
       
        projectileRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

        
        projectileRigidbody.gravityScale = Random.Range(0.2f, 0.5f);

        Invoke(nameof(DeactiveDelay), 2.0f);
    }
    void DeactiveDelay() => gameObject.SetActive(false);

   
    enum TearDeathAnimType
    {
        DeathTypeA,
        DeathTypeB,
        DeathTypeC
    }
    int RandomNumberToSetBool;
    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        Debug.Log("플랫폼 - 물방울");
        //애니메이션 랜덤재생을 위한 랜덤 구조.
        RandomNumberToSetBool = Random.Range(0, 3);
        if (HasHitPlayerCollision(collision) || HasHitGroundCollision(collision))
        {
            switch ((TearDeathAnimType)RandomNumberToSetBool)
            {
                case TearDeathAnimType.DeathTypeA:
                    animator.SetTrigger("Tear_Dead_A");
                    projectileRigidbody.isKinematic = true;
                    projectileRigidbody.velocity = Vector3.zero;
                    break;

                case TearDeathAnimType.DeathTypeB:
                    animator.SetTrigger("Tear_Dead_B");
                    projectileRigidbody.isKinematic = true;
                    projectileRigidbody.velocity = Vector3.zero;
                    break;

                case TearDeathAnimType.DeathTypeC:
                    animator.SetTrigger("Tear_Dead_C");
                    projectileRigidbody.isKinematic = true;
                    projectileRigidbody.velocity = Vector3.zero;
                    break;

                default:Debug.Log("Tear애니메이션 오류");
                    break;
            }
           
        }
       
        //오브젝트를 멈춤(속도)
       
    }


    private void OnDisable()
    {
        projectileRigidbody.isKinematic = false;
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();

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
