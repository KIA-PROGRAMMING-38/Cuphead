using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCarrotProjectileController : MonoBehaviour
{
    Rigidbody2D BackgroundCarrotRigidbody;

    [SerializeField]
    float _speed;
    private void OnEnable()
    {
        BackgroundCarrotRigidbody = GetComponent<Rigidbody2D>();
        Invoke(nameof(DeactivateDelay), 2f);
    }

    private void Update()
    {
        BackgroundCarrotRigidbody.velocity = _speed * Vector2.up;
    }


    void DeactivateDelay() => gameObject.SetActive(false)
;
    private void OnDisable()
    {

        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke(); //코루틴과 다르게 반드시 해제해주어야 합니다. 
    }

}
