using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExMoveJumpingBehaviour : StateMachineBehaviour
{
    public Rigidbody2D playerRigidbody;
    public Animator playerAnimator;



    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        if (CupheadController.IsJumpEXMoving == false && CupheadController.IsJumping == true)
        {
            playerRigidbody = animator.GetComponent<Rigidbody2D>();
            animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            playerAnimator = animator.GetComponent<Animator>();
            CupheadController.IsJumpEXMoving = true;
        }


    }






    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {






    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
