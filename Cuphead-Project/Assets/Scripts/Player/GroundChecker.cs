using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{

    [SerializeField] Animator PlayerAnimator;


   
    private void Awake()
    {
       
    }

    private void Start()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)

    {
        if (IsPlatformCollision(collision))
        {

            PlayerAnimator.SetBool(CupheadAnimID.IS_JUMPING, false);
            PlayerAnimator.SetBool(CupheadAnimID.TRY_PARRYING, false);
            CupheadController.IsJumping = false;

          

        }

    }
    private bool IsPlatformCollision(Collider2D collision)
    {
        return collision.CompareTag(LayerNames.PLATFORM);
    }
}
