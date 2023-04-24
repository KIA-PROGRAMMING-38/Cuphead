using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Hit_Ground_Behaviour : StateMachineBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;

   
    [SerializeField] Vector2 BounceVectorByHit = new Vector2 (5, 5);

    [SerializeField] Script peashotSpawner;





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

        peashotSpawner.SetActive(false);
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.SetBool(CupheadAnimID.HAS_BEEN_HIT, false);
       
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        peashotSpawner.SetActive(true);
    }
}
