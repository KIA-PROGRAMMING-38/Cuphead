using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitChecker : MonoBehaviour
{
    // this is bullet animator to check and turn on the bullet-hit animation
    // potato damage refecltion code is gonna be on the potato script
    [SerializeField]
    float _radiusSize;

    [SerializeField]
    public LayerMask whatIsEnemy;

    [SerializeField]
    Animator _bulletAnimator;

    private void Update()
    {
        TurnOnBulletDeath();
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radiusSize);
    }
    public bool isHittingEnemy;
    public bool CheckBulletHitEnemy()
    {
        return Physics2D.OverlapCircle(transform.position, _radiusSize, whatIsEnemy);
    }

    /// <summary>
    /// This code below is enemy-hit checking method.
    /// </summary>
    /// 

    public bool isHit;
    public void TurnOnBulletDeath()
    {
        isHittingEnemy = CheckBulletHitEnemy();
      
        if (isHittingEnemy)
        {
           
          
            _bulletAnimator.SetBool(BulletAnimID.IS_Hit, true);
            isHit = true;


        }
        else
        {
            _bulletAnimator.SetBool(BulletAnimID.IS_Hit, false);
            isHit = false;
        }
    }
 
}
