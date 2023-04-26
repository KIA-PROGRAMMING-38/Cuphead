using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryEffectController : MonoBehaviour
{
    WaitForSeconds _parryWaitTime;

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

        _parryWaitTime = new WaitForSeconds(_parryingWaitTime);
    }
    private void OnEnable()
    {

        CupheadEffectsController.ParryEffect += CustomEventHandler;
    }

    private void OnDisable()
    {

        CupheadEffectsController.ParryEffect -= CustomEventHandler;
    }

    // 이벤트 핸들러 함수
    private void CustomEventHandler()
    {
        Debug.Log("이벤트함수 호출성공 개축하:");
        spriteRenderer.enabled = true;
        animator.enabled = true;
        StartCoroutine(DelayDeactivatingParryEffect());
    }

    IEnumerator DelayDeactivatingParryEffect()
    {
        yield return _parryWaitTime;
        spriteRenderer.enabled = false;
        animator.enabled = false;
    }
}
