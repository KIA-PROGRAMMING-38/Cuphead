using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D _bulletRigidbody;

    [SerializeField]
    Renderer render;

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
         bulletHitChecker = GetComponent<BulletHitChecker>();
        _playerSpriteRenderer = CupheadController._playerSpriteRenderer;
        _bulletForce = new Vector2(30f, 0f);

        if (_playerSpriteRenderer.flipX == false)
        {
            _bulletRigidbody.velocity = _bulletForce;

        }
        else if (_playerSpriteRenderer.flipX == true)
        {
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