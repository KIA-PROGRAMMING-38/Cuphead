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
        //state 진입 시에만 속도를 제어합니다. 
        changeAnimationSpeed(animator);
    }

    /// <summary>
    ///보스는 2번의 턴 동안 일반속도의 공격을 하고, 
    // 3번째 공격에서 빠른공격을 합니다. 
    // 이부분을 제어하는 부분입니다. 
    /// </summary>
    /// <param name="animator"></param>
    void changeAnimationSpeed(Animator animator)
    {
        if (animation_turn % POTATO_RAPID_ANIMATION_CYCLE == POTATO_RAPID_ANIMATION_TURN)
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
