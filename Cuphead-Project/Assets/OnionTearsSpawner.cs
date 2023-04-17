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
    private readonly int SPAWN_POSITION_MIN = 0;
    private readonly int SPAWN_POSITION_MAX = 10;

    private int tearDecider = 0;
    private readonly float PARRYABLE_TEAR = 85;
    private readonly float NORMAL_TEAR = 0;

    Vector3 spawnPositionMove;


    /// <summary>
    /// Tear 생성 함수입니다. Onion의 애니메이션에 맞게 동작합니다. 
    /// </summary>
    /// <returns></returns>
    GameObject throwProjectile()
    {

        spawnPosition = Random.Range(SPAWN_POSITION_MIN, SPAWN_POSITION_MAX);
        tearDecider = Random.Range(0, 100);

        Debug.Log(spawnPosition);

        if (spawnPosition < 5)
        {
           
            spawnPositionMove = new Vector3(spawnPosition, 0, 0);

            _spawnpositionLeft.transform.position =
            _spawnpositionLeft.transform.position + spawnPositionMove;

           

            if (tearDecider >= PARRYABLE_TEAR)
            {
                return ObjectPooler.SpawnFromPool
                (ObjectPoolNameID.ONION_TEARS_PARRYABLE, _spawnpositionLeft.transform.position);
            }

            else
            {
                return ObjectPooler.SpawnFromPool
                (ObjectPoolNameID.ONION_TEARS, _spawnpositionLeft.transform.position);
            }


        }

        else
        {
            spawnPositionMove = new Vector3(spawnPosition, 0, 0);

            _spawnpositionRight.transform.position =
            _spawnpositionRight.transform.position + spawnPositionMove;

          




            if (tearDecider >= PARRYABLE_TEAR)
            {
                return ObjectPooler.SpawnFromPool
                (ObjectPoolNameID.ONION_TEARS_PARRYABLE, _spawnpositionRight.transform.position);
            }
            else
            {
                return ObjectPooler.SpawnFromPool
                (ObjectPoolNameID.ONION_TEARS, _spawnpositionRight.transform.position);
            }

        }



    }
}
