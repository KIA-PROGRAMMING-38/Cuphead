using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuphead_EXMOVE_Up_Air_Behaviour : StateMachineBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;


    [SerializeField] Vector2 exMoveBounce;
    private bool isExMoveUsed;
 
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        isExMoveUsed = false;
        playerRigidbody = animator.GetComponent<Rigidbody2D>();
        playerRigidbody.isKinematic = true;
        playerRigidbody.velocity = Vector3.zero;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
        Input.ResetInputAxes();
       
        if (stateInfo.normalizedTime > 0.5 && isExMoveUsed == false)
        {
            playerRigidbody.velocity = exMoveBounce;
            playerRigidbody.isKinematic = false;

            isExMoveUsed = true;
        }

        if (stateInfo.normalizedTime > 0.9 && isExMoveUsed == true)
        {
            animator.SetBool(CupheadAnimID.EX_MOVE, false);
        }

    }
}
