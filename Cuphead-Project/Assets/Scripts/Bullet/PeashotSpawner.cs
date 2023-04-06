using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeashotSpawner : MonoBehaviour
{
    Transform _parentTransfrom;


    private void Start()
    {
        _parentTransfrom = GetComponentInParent<Transform>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            GameObject peashot = ObjectPooler.SpawnFromPool("Bullet", _parentTransfrom.position);
           
        }
    }
}
