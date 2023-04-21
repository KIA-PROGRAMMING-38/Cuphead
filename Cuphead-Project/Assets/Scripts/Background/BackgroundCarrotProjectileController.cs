using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCarrotProjectileController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    [SerializeField]
    float _speed;
    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Invoke(nameof(DeactivateDelay), 5f);
    }

    private void Update()
    {
        rigidbody.velocity = _speed * Vector2.up;
    }



  

    void DeactivateDelay() => gameObject.SetActive(false)
;
    private void OnDisable()
    {

        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke(); //코루틴과 다르게 반드시 해제해주어야 합니다. 
    }

}
