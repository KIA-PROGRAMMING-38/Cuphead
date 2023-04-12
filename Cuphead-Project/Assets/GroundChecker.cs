using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator PlayerAnimator;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsPlatformCollision(collision))
        {
            Debug.Log("Jump and Parry Off");
            PlayerAnimator.SetBool(CupheadAnimID.IS_JUMPING, false);
            PlayerAnimator.SetBool(CupheadAnimID.IS_PARRYING, false);
            CupheadController.IsJumping = false;
            
        }

    }
    private bool IsPlatformCollision(Collider2D collision)
    {
        return collision.CompareTag(LayerNames.PLATFORM);
    }
}
