using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potatoProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    float _spawnCoolTime;

    float _elapsedTime;

    [SerializeField]
    GameObject _spawnposition;


    void Update()
    {
        _elapsedTime += Time.deltaTime;


        if (_elapsedTime > _spawnCoolTime)
        {
            GameObject PotatoProjectile = ObjectPooler.SpawnFromPool("PotatoProjectile", _spawnposition.transform.position);

            _elapsedTime = 0.0f;

        }
    }
}
