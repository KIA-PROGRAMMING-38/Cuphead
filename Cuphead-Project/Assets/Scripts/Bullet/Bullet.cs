using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D _bulletRigidbody;

    [SerializeField]
    Renderer render;

    [SerializeField]
    Vector2 _bulletForce;


    private void OnEnable()
    {
       _bulletForce = new Vector2(30f, 0f);
        _bulletRigidbody.velocity = _bulletForce; //* Time.deltaTime;
        Invoke(nameof(DeactiveDelay), 10);
    }

    void DeactiveDelay() => gameObject.SetActive(false)
;  
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
       
        CancelInvoke(); //unlike coroutine, using Invoke have to be used with CancelInvoke
    }



}
