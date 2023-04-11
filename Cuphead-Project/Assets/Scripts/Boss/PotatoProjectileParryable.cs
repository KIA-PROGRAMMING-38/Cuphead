using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoProjectileParryable : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D projectileRigidbody;

    SpriteRenderer projectile;

    [SerializeField]
    Vector2 projectileForce;

    BulletHitChecker bulletHitChecker;

    float _spawnMoveDistance;



    private void OnEnable()
    {
        //이후 패링객체 플레이어 상호작용 시 사용할 함수를 미리 작성했습니다.
        //bulletHitChecker = GetComponent<BulletHitChecker>();
        projectileRigidbody.velocity = projectileForce;
        Invoke(nameof(DeactiveDelay), 1.5f);

    }

    //특정 시간이 지나면 비활성화가 되도록 합니다. 
    void DeactiveDelay() => gameObject.SetActive(false)
;
    private void OnDisable()
    {
        // 패리어블 오브젝트 풀에 다시 리턴합니다. 
        ObjectPooler.ReturnToPool(gameObject);

        CancelInvoke();
    }
}