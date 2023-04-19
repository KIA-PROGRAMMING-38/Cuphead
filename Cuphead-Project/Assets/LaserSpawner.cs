using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{

    readonly int LASER_COUNT_TOTAL = 5;
    Vector3 _lastlyTrackedPlayerPosition;

    [SerializeField]
    Transform _playerTransform;
    WaitForSeconds _waitTimeToSpawnNextLaser;
    private float _waitTime = 0.1f;

    private void OnEnable()
    {
        _waitTimeToSpawnNextLaser = new WaitForSeconds(_waitTime);
    }

    int laserCount = 0;
    GameObject throwLaserProjectile()
    {
        if (laserCount < LASER_COUNT_TOTAL)
        {
            StartCoroutine(SpawnLaser());
        }
        else
        {
            laserCount = 0;
        }

        return ObjectPooler.SpawnFromPool
            (ObjectPoolNameID.CARROT_LASER, transform.position);
    }

    IEnumerator SpawnLaser()
    {
        yield return _waitTimeToSpawnNextLaser;
        laserCount++;
        throwLaserProjectile();
        
    }
    /// <summary>
    /// 애니메이션 이벤트에서 작동합니다.
    /// 캐롯의 레이저가 향할 플레이어 위치 값을 바꿔줍니다.
    /// </summary>
    public void GetPlayerPosition()
    {
        CarrotLaserController._lastlyDetectedPlayerPosition = _playerTransform.position;
    }

}
