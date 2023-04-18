using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExMoveGroundBehaviour : StateMachineBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;


    [SerializeField] Vector2 exMoveBounce;
    private bool isExMoveUsed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //if (CupheadController.IsEXMoving == false && CupheadController.IsJumping == false)
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
            animator.SetBool(CupheadAnimID.IS_EX_MOVING, false);
        }

    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}






