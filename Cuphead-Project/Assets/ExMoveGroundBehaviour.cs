using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExMoveGroundBehaviour : StateMachineBehaviour
{
    public Rigidbody2D playerRigidbody;
    public Animator playerAnimator;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        if (CupheadController.IsEXMoving == false && CupheadController.IsJumping == false)
        {
            playerRigidbody = animator.GetComponent<Rigidbody2D>();
            animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            playerAnimator = animator.GetComponent<Animator>();
            CupheadController.IsEXMoving = true;

        }
      

    }




    
}


