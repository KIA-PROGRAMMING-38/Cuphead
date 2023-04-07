using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class IdleBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    //.마일스톤  -< 이슈발행,라벨, 담당자, 상태 설정 -> submit -> 브랜치생성 # 체크아웃, 
    //git checkout #1 fetch origin  -> pull request 설정: 양식 등록 
    //Creat Request => code review 가능 
    //squash and merge: 커밋갯수 줄이기 : 여러개로 쪼갠경우 스쿼시하여 압축-> 사라짐 
    //합치고 난 후 delete branch 
    //Pull request 끝나면 이슈 자동 종료 
}
