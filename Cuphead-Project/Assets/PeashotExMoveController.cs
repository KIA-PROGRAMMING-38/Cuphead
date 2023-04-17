using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeashotExMoveController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D _bulletRigidbody;
    [SerializeField]
    Transform _rangePosition;
    [SerializeField]
    SpriteRenderer ExMoveSpriteRenderer;

    [SerializeField]
    SpriteRenderer _playerSpriteRenderer;

    [SerializeField]
    Vector2 _exMoveForce;




    private void OnEnable()
    {
        //플레이어의 플립여부 자료를 받아옵니다.
        //받아온 자료를 조건으로 하여, 총알의 발사 위치를 정합니다.

        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
        {
            ExMoveSpriteRenderer.flipX = false;
            _bulletRigidbody.velocity = _exMoveForce;
         

        }
        else if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_LEFT)
        {
            ExMoveSpriteRenderer.flipX = true;
            _bulletRigidbody.velocity = -_exMoveForce;

        }

    }

    public void Update()
    {


        if (CheckBulletIsHit() == true)
        {
            _bulletRigidbody.velocity = Vector2.zero;
            Invoke(nameof(DeactiveDelay), 0.2f);
        }

        TurnOnBulletDeath();
    }

    void DeactiveDelay() => gameObject.SetActive(false)
;
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);

        CancelInvoke(); //unlike coroutine, using Invoke have to be used with CancelInvoke
    }

    // this is bullet animator to check and turn on the bullet-hit animation
    // potato damage refecltion code is gonna be on the potato script
    [SerializeField]
    float _radiusSize;

    [SerializeField]
    public LayerMask whatIsEnemy;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_rangePosition.position, _radiusSize);
    }
    public bool isHittingEnemy;
    public bool CheckBulletIsHit()
    {
        return Physics2D.OverlapCircle(_rangePosition.position, _radiusSize, whatIsEnemy);
    }



    /// <summary>
    /// This code below is enemy-hit checking method.
    /// </summary>
    /// 



    [SerializeField]
    Animator _bulletAnimator;


    public void TurnOnBulletDeath()
    {
        //bool hitWall = CheckBulletHitsWall();
        isHittingEnemy = CheckBulletIsHit();

        if (isHittingEnemy)
        {
            _bulletAnimator.SetBool(BulletAnimID.HIT_ENEMY_OR_ITS_PROJECTILES, true);

        }
        else
        {
            _bulletAnimator.SetBool(BulletAnimID.HIT_ENEMY_OR_ITS_PROJECTILES, false);
        }
    }

}
