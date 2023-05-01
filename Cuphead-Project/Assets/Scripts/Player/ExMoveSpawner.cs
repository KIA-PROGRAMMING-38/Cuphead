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
    PeashotSpawner peashotSpawner;

    [SerializeField]
    float _spawnCoolTime;

    float _elapsedTime;


    public void DecreaseExGauge()
    {
        if (CupheadController.CurrentExMoveGauge > CupheadController.ExMoveGaugeCountPerOne)
        {
            CupheadController.CurrentExMoveGauge -= CupheadController.ExMoveGaugeCountPerOne;
        }

    }

    [SerializeField]
    AudioSource _soundmanager;
    [SerializeField]
    AudioClip ExmoveSound;
    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        // 총알 발사시간을 제한하는 조건을 넣어, 지나치게 많은 총알이 생성되는 것을 방지합니다. 
        if (Input.GetKeyDown(KeyCode.V) && _elapsedTime > _spawnCoolTime &&
            UIController.ExCount > 0)
        {
            _soundmanager.clip = ExmoveSound;
            _soundmanager.PlayOneShot(ExmoveSound);

            CupheadController.SuperMeter++;

            DecreaseExGauge();

            _playerAnimator.SetBool(CupheadAnimID.EX_MOVE, true);
          
            // 스폰 시 포지션을 정해줍니다. 
            // 숙인 경우(Ducking) 발사 위치를 밑으로 정해줍니다. 
            _elapsedTime = 0.0f;

        }
      
    }
    public void LaunchExMove()
    {
        peashotSpawner.isUp = false;
        GameObject bullet = ObjectPooler.SpawnFromPool(ObjectPoolNameID.EX_MOVE, _spawnposition.transform.position);
    }
    public void LaunchExMoveUp()
    {
        peashotSpawner.isUp = true;
        GameObject bullet = ObjectPooler.SpawnFromPool(ObjectPoolNameID.EX_MOVE, _spawnposition.transform.position);
    }
}
