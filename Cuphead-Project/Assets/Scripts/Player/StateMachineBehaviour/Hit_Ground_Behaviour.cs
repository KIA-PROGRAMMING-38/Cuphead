using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Hit_Ground_Behaviour : StateMachineBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;


    Vector3 positionToMoveByHitRight;
    Vector3 positionToMoveByHitLeft;







    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(CupheadAnimID.EX_MOVE, false);
        playerRigidbody = animator.GetComponent<Rigidbody2D>();
        positionToMoveByHitLeft =
            playerRigidbody.transform.position + Vector3.left + Vector3.up;
        positionToMoveByHitRight = 
            playerRigidbody.transform.position + Vector3.right + Vector3.up;
        playerRigidbody.isKinematic = true;

      
      
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CupheadController.HasBeenHit = true;

        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT && CupheadController.HasBeenHit == false)
        {
            Debug.Log($"velociy changed by projectile");
            playerRigidbody.transform.position =
                Vector3.Lerp(playerRigidbody.transform.position, positionToMoveByHitRight, 0.5f);


        }

        else
        {
            playerRigidbody.transform.position =
            Vector3.Lerp(playerRigidbody.transform.position, positionToMoveByHitLeft, 0.5f);
            Debug.Log($"velociy changed by projectile");

        }

       
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerRigidbody.isKinematic = false;
        animator.SetBool(CupheadAnimID.HAS_BEEN_HIT, false);
        CupheadController.HasBeenHit = false;
    }
}
