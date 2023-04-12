using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Potato_Attack_Behaviour : StateMachineBehaviour
{
    int animation_turn = 0;
    readonly int POTATO_RAPID_ANIMATION_TURN =2;
    readonly int POTATO_RAPID_ANIMATION_CYCLE = 3;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(animation_turn % POTATO_RAPID_ANIMATION_CYCLE == POTATO_RAPID_ANIMATION_TURN)
        {
            animator.speed = 2.0f;
            animation_turn++;
        }
        else
        {
            animator.speed = 1.2f;
            animation_turn++;
        }
        
    }
}
