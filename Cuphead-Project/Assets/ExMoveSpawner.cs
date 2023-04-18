using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExMoveSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _spawnposition;
    [SerializeField]
    Animator _playerAnimator;
    [SerializeField]
    Animator _exMoveAnimator;

    [SerializeField]
    float _spawnCoolTime;

    float _elapsedTime;
   

                                                                                                                                             

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        // 총알 발사시간을 제한하는 조건을 넣어, 지나치게 많은 총알이 생성되는 것을 방지합니다. 
        if (Input.GetKey(KeyCode.V) && _elapsedTime > _spawnCoolTime)
        {
            _playerAnimator.SetBool(CupheadAnimID.IS_EX_MOVING, true);
            _exMoveAnimator.SetBool(BulletAnimID.IS_LAUNCHED, true);
          
            // 스폰 시 포지션을 정해줍니다. 
            // 숙인 경우(Ducking) 발사 위치를 밑으로 정해줍니다. 
            _elapsedTime = 0.0f;

        }
      
    }
    public void LaunchExMove()
    {
        GameObject bullet = ObjectPooler.SpawnFromPool(ObjectPoolNameID.EX_MOVE, _spawnposition.transform.position);
    }
}
