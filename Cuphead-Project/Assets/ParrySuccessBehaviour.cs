using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySuccessBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CupheadController.PlayerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
