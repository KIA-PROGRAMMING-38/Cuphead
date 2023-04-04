using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpingBehaviour : StateMachineBehaviour
{
    public static bool isLongJump;
    Rigidbody2D rigidbody;

   [SerializeField] Vector2 _jumpPower = new Vector2(0f, 3000f);

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        rigidbody = animator.GetComponent<Rigidbody2D>();

      
        rigidbody.AddForce(Vector2.up * _jumpPower);
        

       
         


    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(CupheadAnimID.IS_JUMPING, false);
    }


    
}

