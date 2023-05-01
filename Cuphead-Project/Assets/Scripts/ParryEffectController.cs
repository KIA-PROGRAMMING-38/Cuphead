using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryEffectController : MonoBehaviour
{
    WaitForSeconds _parryEffectOnTime;

    [SerializeField]
    float _parryingWaitTime;
    [SerializeField]
    CupheadEffectsController cupheadEffectsController;

    SpriteRenderer spriteRenderer;
    Animator animator;
    private void Start()
    {
       


        animator = GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.enabled = false;
        animator.enabled = false;

        
    }
    private void OnEnable()
    {

        CupheadEffectsController.ParryEffect += ActivateEffect;
    }

    private void OnDisable()
    {

        CupheadEffectsController.ParryEffect -= ActivateEffect;
    }

    // 이벤트 핸들러 함수
    private void ActivateEffect()
    {
        _parryEffectOnTime = new WaitForSeconds(_parryingWaitTime);
        Debug.Log("이벤트호출");
        spriteRenderer.enabled = true;
        animator.enabled = true;
        StartCoroutine(DelayDeactivatingParryEffect());
    }

    IEnumerator DelayDeactivatingParryEffect()
    {
        yield return _parryEffectOnTime;
        spriteRenderer.enabled = false;
        animator.enabled = false;
    }
}
