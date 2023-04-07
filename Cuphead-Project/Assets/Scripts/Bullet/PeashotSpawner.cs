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

        if (Input.GetKey(KeyCode.X) && _elapsedTime > 0.4f)
        {
            
            GameObject bullet = ObjectPooler.SpawnFromPool("Bullet", _spawnposition.transform.position);
            
            Debug.Log(_elapsedTime);
            _elapsedTime = 0.0f;
        }
    }



    private void FixedUpdate()
    {
       
    }
}
