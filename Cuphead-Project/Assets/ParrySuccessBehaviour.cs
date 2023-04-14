using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParrySuccessBehaviour : StateMachineBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;

    float _radiusSize;

    
    public bool hasParried;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CupheadController.TryParrying = false;
        playerAnimator = animator.GetComponent<Animator>();
        playerRigidbody = animator.GetComponent<Rigidbody2D>();
        animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
       
    }


    [SerializeField] Vector2 ParryVector;
    [SerializeField] float StopTime = 0.4f;
    float elapsedTime = 0;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //일정시간 플레이를 멈추고, 플레이 재개 후 플레이어 반동
        elapsedTime += Time.time;
        if(elapsedTime > StopTime && CupheadController.HasParried == false)
        {
            CupheadController.HasParried = true;
            
            animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            playerRigidbody.velocity = ParryVector;
            playerAnimator.SetBool(CupheadAnimID.IS_PARRYING, false);
            playerAnimator.SetBool(CupheadAnimID.HAS_PARRIED, false);
        }
      
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //시간 관련 변수 초기화 작업
        elapsedTime = 0f;
        Time.timeScale = 1.0f;
    }

}
