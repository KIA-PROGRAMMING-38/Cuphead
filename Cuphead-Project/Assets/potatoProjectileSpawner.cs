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
    float _elapsedTime;

    [SerializeField]
    GameObject _spawnposition;


    void Start()
    {
       
    }


    void Update()
    {
       

    }

    GameObject throwProjectile()
    {
       return ObjectPooler.SpawnFromPool("PotatoProjectile", _spawnposition.transform.position);
    }


}
