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
      
        playerAnimator = animator.GetComponent<Animator>();
        playerRigidbody = animator.GetComponent<Rigidbody2D>();
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        ParryVector = Vector2.left + Vector2.up;
        
        
    }

    Vector2 ParryVector;
    [SerializeField] float StopTime = 0.8f;
    [SerializeField] float parryBounceForce;
    float elapsedTime = 0;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > StopTime)
        {
            playerRigidbody.constraints = RigidbodyConstraints2D.None;
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            playerRigidbody.AddForce(ParryVector * parryBounceForce, ForceMode2D.Impulse);
            playerAnimator.SetBool(CupheadAnimID.IS_PARRYING, false);
            playerAnimator.SetBool(CupheadAnimID.HAS_PARRIED, false);
        }
      
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

}
