using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Jumping_Behaviour : StateMachineBehaviour
{
 

    
    Rigidbody2D playerRigidbody;
    [SerializeField]
    public Vector2 _jumpForce = new Vector2(0f, 17);

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        playerRigidbody = animator.GetComponent<Rigidbody2D>();
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, _jumpForce.y);

        //입력을 초기화 (점프값을 한 번 더 받기 위함입니다.
        //플레이어가 지면에 있을때만 점프
        //코루틴 이외 지면에 닿을때도 false대입 해주는 점 주의합니다.




    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ParryPlayer();
        
        void ParryPlayer()
        {
            //점프 상태에서 한 번 더 누르면
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // 패링상태 실행 
                CupheadController.TryParrying = true;
                animator.SetBool(CupheadAnimID.TRY_PARRYING, true);
            }
        }
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}






