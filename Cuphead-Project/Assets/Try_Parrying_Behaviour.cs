using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Try_Parrying_Behaviour : StateMachineBehaviour
{
    //패리 성공을 의미
    

    public bool SucceedInParry;
    [SerializeField]
    public LayerMask LayerToCheck;

    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SucceedInParry = CheckOverlaying(animator);
        if (SucceedInParry) 
        {
            Debug.Log("parry overlayed!");
            animator.SetBool(CupheadAnimID.HAS_PARRIED, true);
        }
        //SucceedInParry?.Invoke();
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    
    public bool CheckOverlaying(Animator animator)
    {
        float offset = 0.5f;
        Vector2 playerPosition = animator.transform.position;
        playerPosition.y -= offset;
        Debug.Log(animator.transform.position);
        return Physics2D.OverlapCircle(playerPosition, ParryAndHitCheckGizmo.PLAYER_COLLIDER_GIZMO_SIZE, LayerToCheck);
           
    }

   

}




