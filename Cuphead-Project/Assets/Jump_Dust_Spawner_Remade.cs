using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Dust_Spawner_Remade : MonoBehaviour
{
    [SerializeField]
    GameObject _spawnposition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlatformCollision(collision))
        {
            Debug.Log("먼지생성");
            throwProjectile();
        }

    }
    public GameObject throwProjectile()

    {
        return ObjectPooler.SpawnFromPool
        ("JumpDust", _spawnposition.transform.position);
    }

    private bool IsPlatformCollision(Collider2D collision)
    {
        return collision.CompareTag(LayerNames.PLATFORM);
    }
}
