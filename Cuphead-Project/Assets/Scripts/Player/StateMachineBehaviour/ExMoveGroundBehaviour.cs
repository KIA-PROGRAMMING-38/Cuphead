using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExMoveGroundBehaviour : StateMachineBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;


    [SerializeField] Vector2 exMoveBounce;
    Vector2 decidedExMoveBounceForceByPlayerDirection;

    private bool isExMoveUsed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

     
        isExMoveUsed = false;
        playerRigidbody = animator.GetComponent<Rigidbody2D>();
        playerRigidbody.isKinematic = true;
        playerRigidbody.velocity = Vector3.zero;


        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
        {
            decidedExMoveBounceForceByPlayerDirection = exMoveBounce;
        }
        else if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_LEFT)

        {
            decidedExMoveBounceForceByPlayerDirection = -exMoveBounce;
        }
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Input.ResetInputAxes();
     
        if (stateInfo.normalizedTime > 0.5 && stateInfo.normalizedTime < 0.6)
        {
            isExMoveUsed = true;
            playerRigidbody.velocity = decidedExMoveBounceForceByPlayerDirection;
           
        }
        if (stateInfo.normalizedTime > 0.7 && isExMoveUsed == true)
        {
            playerRigidbody.isKinematic = false;
            animator.SetBool(CupheadAnimID.EX_MOVE, false);
        }

    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     
    }
}






