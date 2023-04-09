using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoProjectile : MonoBehaviour
{

    [SerializeField]
    Rigidbody2D projectileRigidbody;

    SpriteRenderer projectile;

    [SerializeField]
    Vector2 projectileForce;

    BulletHitChecker bulletHitChecker;

    float _spawnMoveDistance;


    public void Update()
    {
      
           //EXMOVE BRANCH TEST
       

    }
    private void OnEnable()
    {
        bulletHitChecker = GetComponent<BulletHitChecker>();
        projectileRigidbody.velocity = projectileForce;
        Invoke(nameof(DeactiveDelay), 0.5f);

    }

    void DeactiveDelay() => gameObject.SetActive(false)
;
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);

        CancelInvoke(); //unlike coroutine, using Invoke has to be used with CancelInvoke
    }




}