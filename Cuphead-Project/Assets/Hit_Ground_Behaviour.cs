using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Ground_Behaviour : StateMachineBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;

   
    [SerializeField] Vector2 BounceVectorByHit = new Vector2 (5, 5);
  
  



    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //초기화만 진행?
        playerRigidbody = animator.GetComponent<Rigidbody2D>();
    

        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT && CupheadController.HasBeenHit == false)
        {
            Debug.Log($"velociy changed by projectile");
            playerRigidbody.velocity = BounceVectorByHit;
            CupheadController.HasBeenHit = true;
        }
        else
        {
            Debug.Log($"velociy changed by projectile");
            playerRigidbody.velocity = BounceVectorByHit;
            CupheadController.HasBeenHit = true;
        }
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

       
    }
}
