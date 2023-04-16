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

        playerRigidbody = animator.GetComponent<Rigidbody2D>();
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, parrybounce);
        Debug.Log($"바운스 반영 후 플레이어 속도 {playerRigidbody.velocity})");
        //시간 관련 변수 초기화 작업

    }


    [SerializeField] float parrybounce;


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
 
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(CupheadAnimID.HAS_PARRIED, false);
        animator.SetBool(CupheadAnimID.HAS_BEEN_HIT, false);

    }
}


