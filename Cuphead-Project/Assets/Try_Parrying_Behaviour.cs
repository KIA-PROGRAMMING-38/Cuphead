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


    private bool SucceedInParry;
    [SerializeField]
    public LayerMask LayerToCheck;
    Transform playerTransform;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     
        playerTransform =  animator.GetComponent<Transform>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        SucceedInParry = CheckOverlaying(playerTransform);
       

        if (SucceedInParry == true)
        {
            animator.SetBool(CupheadAnimID.HAS_PARRIED, true);
            CupheadController.TryParrying = false;
        }

        /// <summary>
        /// 패리객체가 플레이어가 패링상태인 동안 플레이어 객체에게 닿았는지 검사합니다.
        /// 이벤트등 다른 함수를 사용하고자 하였으나, Monobehaviour사용 제약등으로 사용방법에
        /// 어려움을 겪어 사용하지 못했습니다. 
        /// </summary>
        /// <param name="animator"></param>
        /// <returns></returns>
    }

    public bool CheckOverlaying(Transform playerTransform)
    {
        float offset = 0.5f;// 오프셋  사용하여 반경이 플레이어 중심에 위치하도록 합니다.
        Vector2 playerPosition = playerTransform.transform.position;
        playerPosition.y -= offset;
        return Physics2D.OverlapCircle(playerTransform.position, ParryAndHitCheckGizmo.PLAYER_COLLIDER_GIZMO_SIZE, LayerToCheck);

    }
}


