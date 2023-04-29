using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Sparker_Controller : MonoBehaviour
{
    SpriteRenderer projectile;
    Animator animator;


    private void OnEnable()
    {
        animator = GetComponent<Animator>();
       
        animator.SetTrigger(BulletAnimID.IS_LAUNCHED);
        Invoke(nameof(DeactiveDelay), 0.7f);
       
        animator.enabled = true;
    }


    private void OnDisable()
    {
       
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();
    }


    void DeactiveDelay() => gameObject.SetActive(false);

}
