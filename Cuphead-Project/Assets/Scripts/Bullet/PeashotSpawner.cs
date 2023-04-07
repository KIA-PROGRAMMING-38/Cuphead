using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PeashotSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _spawnposition;
    ObjectPooler objectPooler;
 


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            
            GameObject bullet = ObjectPooler.SpawnFromPool("Bullet", _spawnposition.transform.position);
           
        }
    }
}
