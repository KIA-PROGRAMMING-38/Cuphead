using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PeashotSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _spawnposition;


    [SerializeField]
    float _spawnCoolTime;
    float _elapsedTime;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.X) && _elapsedTime > _spawnCoolTime)
        {
            
            GameObject bullet = ObjectPooler.SpawnFromPool("Bullet", _spawnposition.transform.position);
            
           
            _elapsedTime = 0.0f;
        }
    }



    private void FixedUpdate()
    {
       
    }
}
