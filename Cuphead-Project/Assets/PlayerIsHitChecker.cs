using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsHitChecker : MonoBehaviour, IChecker
{
    [SerializeField]
    Transform _transform;

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

    Vector2 BounceVector;
    [SerializeField] float hitBounceForce;
    public void HitDefreezeAndBoucnce()
    {
        BounceVector = Vector2.up;
        CupheadController.PlayerRigidbody.constraints = RigidbodyConstraints2D.None;
        CupheadController.PlayerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        CupheadController.PlayerRigidbody.AddForce(BounceVector * hitBounceForce, ForceMode2D.Impulse);
        
        CupheadController.PlayerAnimator.SetBool(CupheadAnimID.HAS_BEEN_HIT, false);
        hasHit = false;

    }
}
