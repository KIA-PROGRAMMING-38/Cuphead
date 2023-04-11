using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class PlayerParryChecker : MonoBehaviour, IChecker
{
    public static bool hasParried;


    [SerializeField]
    Transform _transform;

    [SerializeField]
    float _radiusSize;

    [SerializeField]
    public LayerMask LayerToCheck;

    void Update()
    {
        ControlAnimator();

    }

    /// <summary>
    /// 플레이어와 패리객체가 겹치는지 판별합니다. 
    /// </summary>
    /// <returns></returns>
    public bool CheckOverlaying()
    {
        if (CupheadController.IsParrying == true)
        {
            return Physics2D.OverlapCircle(_transform.position, _radiusSize, LayerToCheck);
        }
        return false;
    }


    bool ParryOver;
    float playerHoldTimeSeconds;
    [SerializeField]
    private WaitForSeconds playerHoldTime = new WaitForSeconds(0.8f);

    
    IEnumerator stopPlayerCoroutine;
    
    public void ControlAnimator()
    {
       

        hasParried = CheckOverlaying();

        if (hasParried && CupheadController.IsParrying)
        {

           
            CupheadController.PlayerAnimator.SetBool(CupheadAnimID.HAS_PARRIED, true);
        }

       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _radiusSize);
    }

    Vector2 ParryVector;

    [SerializeField] float bounceForce;
    public void Defreeze()
    {
        ParryVector = Vector2.left + Vector2.up;
        Debug.Log("Defreeze!");  Debug.Log(ParryVector);
        CupheadController.PlayerRigidbody.constraints = RigidbodyConstraints2D.None;
        CupheadController.PlayerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        CupheadController.PlayerRigidbody.AddForce(ParryVector * bounceForce, ForceMode2D.Impulse);
        CupheadController.PlayerAnimator.SetBool(CupheadAnimID.IS_PARRYING, false);
        CupheadController.PlayerAnimator.SetBool(CupheadAnimID.HAS_PARRIED, false);
        CupheadController.IsParrying = false;
    }

}
