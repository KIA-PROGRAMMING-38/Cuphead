using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupheadDeadBehaviour : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().isKinematic= true;
        animator.GetComponent<Rigidbody2D>().velocity = Vector3.up;
    }
}
