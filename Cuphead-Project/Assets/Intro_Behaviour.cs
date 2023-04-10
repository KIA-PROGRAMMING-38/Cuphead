using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Behaviour : StateMachineBehaviour
{
    [SerializeField]
    GameObject player;

    Vector2 fixPosition;
    float elapsedTime;
    [SerializeField]
    float playerFixingTime;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fixPosition = player.transform.position;

        elapsedTime += Time.deltaTime;
        while (elapsedTime < playerFixingTime)
        {
            player.transform.position = fixPosition;
           
        }
        
    }
}
