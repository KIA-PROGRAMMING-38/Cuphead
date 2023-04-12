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

        //Debug.Log($"{hasParried}");

        if (hasParried && CupheadController.IsParrying)
        {
           
            CupheadController.PlayerAnimator.SetBool(CupheadAnimID.HAS_PARRIED, true);
           //패링이 중복되는 것을 방지하기 위해, Isparrying값을 false로 만드는 부분을
           // 플레이어가 패링을 실패한 경우.
           // 플레이어가 패링 성공한경우. 두가지로 설정했습니다. 
            CupheadController.IsParrying = false;
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
       
    }

}
