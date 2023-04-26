using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PeashotExMoveController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D _exMoveRigidBody;
    [SerializeField]
    Transform _rangePosition;
    [SerializeField]
    SpriteRenderer ExMoveSpriteRenderer;

    [SerializeField]
    SpriteRenderer _playerSpriteRenderer;

    [SerializeField]
    Vector2 _exMoveForce;

    [SerializeField]
    Vector2 _exMoveForceUp;


    [SerializeField]
    PeashotSpawner peashotSpawner;



    private void OnEnable()
    {
        //플레이어의 플립여부 자료를 받아옵니다.
        //받아온 자료를 조건으로 하여, 총알의 발사 위치를 정합니다.

       //up방향이 아니고, 플레이어 방향이 위쪽인경우.
        if (peashotSpawner.isUp)
        {
            _exMoveRigidBody.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            _exMoveRigidBody.velocity = _exMoveForceUp;
        }
        //up방향이 아니고, 플레이어 방향이 오른쪽인경우.
        else if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT && !peashotSpawner.isUp)
        {
            ExMoveSpriteRenderer.flipX = false;
            _exMoveRigidBody.velocity = _exMoveForce;
        }

        //up방향이 아니고, 플레이어 방향이 왼쪽인경우.
        else if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_LEFT && !peashotSpawner.isUp)
        {
            ExMoveSpriteRenderer.flipX = true;
            _exMoveRigidBody.velocity = -_exMoveForce;
        }

     


    }

    public void Update()
    {


        if (CheckBulletIsHit() == true)
        {
            _exMoveRigidBody.velocity = Vector2.zero;
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
