using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathEffectController : MonoBehaviour
{

    Animator animator;
    Vector3[] vectorToMoveSpawnPosition = new Vector3[12];

    //게임매니저가 보스 위치를 정해줍니다. 
    public Vector3 bossPosition { get; set; }

  
    float _spawnMoveDistance;


    int positionCount;
    readonly int ANIMATE_COUNT_TOTAL = 12;
    
    void Start()
    {
        gameObject.SetActive(false);
        animator.GetComponent<Animator>();
    }


    /// <summary>
    /// BossDeathAnimation위치를 움직여주는 함수입니다. 
    /// 어느정도 규칙성을 띄나, 약간의 랜덤한 요소를 넣어주었습니다. 
    /// </summary>
    public void ActivateAnimation()
    {
        _spawnMoveDistance = Random.Range(1, 5);

        vectorToMoveSpawnPosition[1] = Vector3.left * _spawnMoveDistance + Vector3.up * _spawnMoveDistance;
        vectorToMoveSpawnPosition[2] = Vector3.right * _spawnMoveDistance + Vector3.down * _spawnMoveDistance* 3f;
        vectorToMoveSpawnPosition[3] = Vector3.left * _spawnMoveDistance + Vector3.up * _spawnMoveDistance;
        vectorToMoveSpawnPosition[4] = Vector3.right * _spawnMoveDistance + Vector3.down * _spawnMoveDistance;
        vectorToMoveSpawnPosition[5] = Vector3.left * _spawnMoveDistance + Vector3.up * _spawnMoveDistance * 2f;
        vectorToMoveSpawnPosition[6] = Vector3.right * _spawnMoveDistance + Vector3.down * _spawnMoveDistance * 3f;
        vectorToMoveSpawnPosition[7] = Vector3.left * _spawnMoveDistance + Vector3.up * _spawnMoveDistance*1.5f;
        vectorToMoveSpawnPosition[8] = Vector3.right * _spawnMoveDistance + Vector3.down * _spawnMoveDistance * 2f;
        vectorToMoveSpawnPosition[9] = Vector3.left * _spawnMoveDistance + Vector3.up * _spawnMoveDistance;
        vectorToMoveSpawnPosition[10] = Vector3.right * _spawnMoveDistance + Vector3.down * _spawnMoveDistance;
        vectorToMoveSpawnPosition[11] = Vector3.left * _spawnMoveDistance + Vector3.up * _spawnMoveDistance * 1.5f; ;


  

       
        

    }

    public void MovePosition()
    {

        Debug.Log("OnPoatoDie");

        if (positionCount < ANIMATE_COUNT_TOTAL)
        {
            transform.position += vectorToMoveSpawnPosition[positionCount % ANIMATE_COUNT_TOTAL];
            positionCount++;
        }
        else
        {
            positionCount = 0;
            gameObject.SetActive(false);
        }

    }
}
