using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D _bulletRigidbody;

    [SerializeField]
    SpriteRenderer peashotSpriteRenderer;

    [SerializeField]
    SpriteRenderer _playerSpriteRenderer;

    [SerializeField]
    Vector2 _bulletForce;

    BulletHitChecker bulletHitChecker;

    public void Update()
    {

        if (bulletHitChecker.CheckBulletIsHit() == true)
        {
            _bulletRigidbody.velocity = Vector2.zero;
            Invoke(nameof(DeactiveDelay), 0.2f);
        }
    }
    private void OnEnable()
    {
        //플레이어의 플립여부 자료를 받아옵니다.
        //받아온 자료를 조건으로 하여, 총알의 발사 위치를 정합니다.
        bulletHitChecker = GetComponent<BulletHitChecker>();

        _bulletForce = new Vector2(30f, 0f);
       

        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
        {
            peashotSpriteRenderer.flipX = false;
            _bulletRigidbody.velocity = _bulletForce;

        }
        else if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_LEFT)
        {
            peashotSpriteRenderer.flipX = true;
            _bulletRigidbody.velocity = -_bulletForce;

        }

    }

    void DeactiveDelay() => gameObject.SetActive(false)
;
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);

        CancelInvoke(); //unlike coroutine, using Invoke have to be used with CancelInvoke
    }




}