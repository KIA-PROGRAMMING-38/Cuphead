using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpingBehaviour : StateMachineBehaviour
{

    [SerializeField] Vector2 _jumpPower = new Vector2(0f, 3000f);

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rigidbody2D rigidbody = animator.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(Vector2.up * _jumpPower);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(CupheadAnimID.IS_JUMPING, false);
    }


    
}

