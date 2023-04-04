using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerJumpBehaviour : StateMachineBehaviour
{
    [SerializeField] Vector2 _jumpPower = new Vector2(0f, 1000f);

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rigidbody2D rigidbody = animator.GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.up * _jumpPower ;
    }
}