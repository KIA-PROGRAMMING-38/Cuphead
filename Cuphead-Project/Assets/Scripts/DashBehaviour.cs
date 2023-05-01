using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehaviour : StateMachineBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;

    [SerializeField] float _moveSpeed;
    Vector3 DashMovePositionRight;
    Vector3 DashMovePositionLeft;







    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CupheadController.IsDashing = true;

        playerRigidbody = animator.GetComponent<Rigidbody2D>();

        DashMovePositionLeft =
            playerRigidbody.transform.position + Vector3.left * _moveSpeed;

        DashMovePositionRight =
            playerRigidbody.transform.position + Vector3.right * _moveSpeed;

        playerRigidbody.isKinematic = true;



    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {



        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT && CupheadController.HasBeenHit == false)
        {

            playerRigidbody.velocity = Vector3.right * _moveSpeed;
        }

        else
        {
            playerRigidbody.velocity = Vector3.left * _moveSpeed;



        }


    


    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CupheadController.IsDashing = false;
        animator.SetBool(CupheadAnimID.DASH, false);
        playerRigidbody.isKinematic = false;

    }
}
