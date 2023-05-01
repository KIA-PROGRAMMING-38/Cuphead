using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitWallChecker : MonoBehaviour
{
    // this is bullet animator to check and turn on the bullet-hit animation
    // potato damage refecltion code is gonna be on the potato script
    [SerializeField]
    float _radiusSize;

 
    [SerializeField]
    public LayerMask whatIsWall;

    [SerializeField]
    Animator _bulletAnimator;

    private void Update()
    {
        TurnOnBulletDeath();

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusSize);
    }
  
    public bool isHittingWall;


    public bool CheckBulletIsHitWall()
    {
        return Physics2D.OverlapCircle(transform.position, _radiusSize, whatIsWall);
    }


    /// <summary>
    /// This code below is enemy-hit checking method.
    /// </summary>
    /// 


    public void TurnOnBulletDeath()
    {
        //bool hitWall = CheckBulletHitsWall();
    
        isHittingWall = CheckBulletIsHitWall();
        Debug.Log(isHittingWall);

        if (isHittingWall)
        {

            _bulletAnimator.SetBool(BulletAnimID.HIT_ENEMY_OR_ITS_PROJECTILES, true);

        }
        
    }

}
