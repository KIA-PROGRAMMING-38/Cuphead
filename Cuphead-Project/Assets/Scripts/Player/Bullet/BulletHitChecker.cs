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

    //[SerializeField]
    //public LayerMask whatIsWall;

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
    //public bool isHittingWall;
    public bool CheckBulletIsHit()
    {
        return Physics2D.OverlapCircle(transform.position, _radiusSize, whatIsEnemy);
    }

    //public bool CheckBulletIsHitWall()
    //{
    //    return Physics2D.OverlapCircle(transform.position, _radiusSize, whatIsWall);
    //}


    /// <summary>
    /// This code below is enemy-hit checking method.
    /// </summary>
    /// 


    public void TurnOnBulletDeath()
    {
        //bool hitWall = CheckBulletHitsWall();
        isHittingEnemy = CheckBulletIsHit();
        //isHittingWall = CheckBulletIsHitWall();

        if (isHittingEnemy )
        {
            _bulletAnimator.SetBool(BulletAnimID.HIT_ENEMY_OR_ITS_PROJECTILES, true);
           
        }
        else
        {
            _bulletAnimator.SetBool(BulletAnimID.HIT_ENEMY_OR_ITS_PROJECTILES, false);
        }
    }

   
}
