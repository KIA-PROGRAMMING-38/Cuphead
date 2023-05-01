using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryBounceBehaviour : StateMachineBehaviour
{

    Rigidbody2D playerRigidbody;
    [SerializeField]
    public Vector2 _jumpForce;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        playerRigidbody = animator.GetComponent<Rigidbody2D>();
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, _jumpForce.y);

        
        //플레이어가 지면에 있을때만 점프
        //코루틴 이외 지면에 닿을때도 false대입 해주는 점 주의합니다.

    }
}
