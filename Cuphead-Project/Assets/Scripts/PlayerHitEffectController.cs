using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CupheadEffectsController;

public class PlayerHitEffectController : MonoBehaviour
{
    [SerializeField]
    WaitForSeconds _hitAnimationTime;

   

    [SerializeField]
    float _hitAnimationOnTime;

    SpriteRenderer spriteRenderer;
    Animator animator;
    private void Awake()
    {
       

        _hitAnimationTime = new WaitForSeconds(_hitAnimationOnTime);
    }
    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer.enabled = true;
        animator.enabled = true;
        Invoke(nameof(DeactivateDelay), 0.6f);
        StartCoroutine(DelayDeactivatingParryEffect());
    }
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke(); 
    }

    void DeactivateDelay() => gameObject.SetActive(false);
    private void SetPlayerHitEffect()
    {
     
       
    }

    IEnumerator DelayDeactivatingParryEffect()
    {
        yield return _hitAnimationTime;
        spriteRenderer.enabled = false;
        animator.enabled = false;
    }
  
}
