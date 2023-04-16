using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsHitChecker : MonoBehaviour, IChecker
{
    [SerializeField]
    public Transform _transform;

    [SerializeField]
    float _radiusSize;

    [SerializeField]
    public LayerMask LayerToCheck;

    Gizmos gizmo;

    public static bool hasHit;

    void Update()
    {
        ControlAnimator();
    }

    public void ControlAnimator()
    {
        hasHit = CheckOverlaying();
        if (hasHit)
        {
            CupheadController.PlayerAnimator.SetBool(CupheadAnimID.HAS_BEEN_HIT, true);
        }
        else
        {
            CupheadController.PlayerAnimator.SetBool(CupheadAnimID.HAS_BEEN_HIT, false);
        }
    }

    public bool CheckOverlaying() 
    {
        return Physics2D.OverlapCircle(_transform.position, _radiusSize, LayerToCheck);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_transform.position, _radiusSize);
    }

   
    public void HitDefreezeAndBoucnce()
    {
       
        CupheadController.playerRigidbody.constraints = RigidbodyConstraints2D.None;
        CupheadController.playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        CupheadController.PlayerAnimator.SetBool(CupheadAnimID.HAS_BEEN_HIT, false);
        hasHit = false;

    }
}
