using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionTearsSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _spawnpositionLeft;

    [SerializeField]
    GameObject _spawnpositionRight;


    private int spawnPosition = 0;
    private readonly int SPAWN_POSITION_LEFT = 0;
    private readonly int SPAWN_POSITION_RIGHT = 10;

    private int tearDecider = 0;
    private readonly float PARRYABLE_TEAR = 90;
    private readonly float NORMAL_TEAR = 0;

    Vector3 spawnPositionMove;

    Vector3 _decidedpawnpositionLeft;
    Vector3 _decidedpawnpositionRight;


    /// <summary>
    /// Tear 생성 함수입니다. Onion의 애니메이션에 맞게 동작합니다. 
    /// </summary>
    /// <returns></returns>
    GameObject throwProjectile()
    {

        spawnPosition = Random.Range(SPAWN_POSITION_LEFT, SPAWN_POSITION_RIGHT);
        tearDecider = Random.Range(0, 100);

        //기준값 초기화.
        _decidedpawnpositionLeft = Vector3.zero;
        _decidedpawnpositionRight = Vector3.zero;

        Debug.Log(spawnPosition);

        if (spawnPosition < 5)
        {
            float rangeToMovespawnPosition = Random.Range(0,7);
            spawnPositionMove = new Vector3(rangeToMovespawnPosition, 0, 0);

            // 기준점(왼쪽,오른쪽 총 두개) 에서 랜덤값을 더한값을 최종값으로 입력.
            _decidedpawnpositionLeft =
            _spawnpositionLeft.transform.position + spawnPositionMove;

            Debug.Log(tearDecider);

            if (tearDecider >= PARRYABLE_TEAR)
            {
                return ObjectPooler.SpawnFromPool
                (ObjectPoolNameID.ONION_TEARS_PARRYABLE, _decidedpawnpositionLeft);
            }

            else
            {
                return ObjectPooler.SpawnFromPool
                (ObjectPoolNameID.ONION_TEARS, _decidedpawnpositionLeft);
            }


        }

        else
        {
            float rangeToMovespawnPosition = Random.Range(0, 7);
            spawnPositionMove = new Vector3(rangeToMovespawnPosition, 0, 0);

            // 기준점(왼쪽,오른쪽 총 두개) 에서 랜덤값을 더한값을 최종값으로 입력.
            _decidedpawnpositionRight =
            _spawnpositionRight.transform.position + spawnPositionMove;



            Debug.Log(tearDecider);



            if (tearDecider >= PARRYABLE_TEAR)
            {
                return ObjectPooler.SpawnFromPool
                (ObjectPoolNameID.ONION_TEARS_PARRYABLE, _decidedpawnpositionRight);
            }
            else
            {
                return ObjectPooler.SpawnFromPool
                (ObjectPoolNameID.ONION_TEARS, _decidedpawnpositionRight);
            }

        }



    }
}
