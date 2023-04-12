using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Jumping_Behaviour : StateMachineBehaviour
{
    public readonly int GET_KEY_COUNT_JUMP = 1;
    public readonly int GET_KEY_COUNT_PARRY = 2;

    [SerializeField]
    public Vector2 _jumpForce = new Vector2(0f, 17);

    Rigidbody2D playerRigidbody;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        playerRigidbody = animator.GetComponent<Rigidbody2D>();
        //입력을 초기화 (점프값을 한 번 더 받기 위함입니다.)
       
        //플레이어가 지면에 있을때만 점프
        if (CupheadController.IsJumping == false)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, _jumpForce.y);
            CupheadController.IsJumping = true;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ParryPlayer();

        void ParryPlayer()
        {
            //점프 상태에서 한 번 더 누르면
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("try Parrying");
                // 패링상태 실행 
                animator.SetBool(CupheadAnimID.IS_PARRYING, true);
                CupheadController.IsParrying = true;
              
            }
        }
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.SetBool(CupheadAnimID.IS_PARRYING, false) ;
    }
}






