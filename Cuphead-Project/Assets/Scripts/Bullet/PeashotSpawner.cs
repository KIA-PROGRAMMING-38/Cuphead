using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeashotSpawner : MonoBehaviour
{
    [SerializeField]
    Transform _spawnposition;


    private void Start()
    {
       // _parentTransfrom = GetComponentInParent<Transform>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            GameObject peashot = ObjectPooler.SpawnFromPool("Bullet", _spawnposition.position);
           
        }
    }
}
