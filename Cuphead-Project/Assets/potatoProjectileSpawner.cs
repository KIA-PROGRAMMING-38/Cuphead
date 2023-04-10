using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potatoProjectileSpawner : MonoBehaviour
{


    [SerializeField] float _waitingTime;
    public Animator _animator;
    public GameObject _gameObject;
    [SerializeField]
    float _spawnCoolTime;


    [SerializeField]
    GameObject _spawnposition;


    int count = 0;
    readonly int bossProjectileCounts = 3;
    GameObject throwProjectile()
    {
        if (count < bossProjectileCounts)
        {
            count++;
            return ObjectPooler.SpawnFromPool
                ("PotatoProjectile", _spawnposition.transform.position);

        }
        else
        {
            count = 0;
            return ObjectPooler.SpawnFromPool
                ("PotatoProjectileParryable", _spawnposition.transform.position);
        }
    }


}
