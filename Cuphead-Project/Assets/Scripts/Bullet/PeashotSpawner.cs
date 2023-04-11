
using System.Threading;
using UnityEngine;

public class PeashotSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _spawnposition;

    [SerializeField]
    GameObject _spawnpositionDucking;

    [SerializeField]
    Animator _bulletSparkAnimator;

    [SerializeField]
    float _spawnCoolTime;

    [SerializeField]
    float _spawnMoveDistance;

    float _elapsedTime;
    int countTomove = 0;

    Vector3[] moveSpawnPosition = new Vector3[4];



    //총알이 위아래로 왔다갔다하면서 발사하기 위해서 배열을 만들어 대입합니다. 
    private void Awake()
    {
        moveSpawnPosition[0] = Vector3.up * _spawnMoveDistance;
        moveSpawnPosition[1] = Vector3.up * _spawnMoveDistance;
        moveSpawnPosition[2] = Vector3.down * _spawnMoveDistance;
        moveSpawnPosition[3] = Vector3.down * _spawnMoveDistance;
    }


    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        // 총알 발사시간을 제한하는 조건을 넣어, 지나치게 많은 총알이 생성되는 것을 방지합니다. 
        if (Input.GetKey(KeyCode.X) && _elapsedTime > _spawnCoolTime)
        {
            if (IsOnGroundChecker.isOnGround == true)
            { _bulletSparkAnimator.SetBool(BulletAnimID.IS_LAUNCHED, true); }

            

            Debug.Log(CupheadController.isDucking);

            // 스폰 시 포지션을 정해줍니다. 
            // 숙인 경우(Ducking) 발사 위치를 밑으로 정해줍니다. 
            if (CupheadController.isDucking == true)
            { 
                GameObject bullet = ObjectPooler.SpawnFromPool("Bullet", _spawnpositionDucking.transform.position);
            }
            else if (CupheadController.isDucking == false)
            {
                _spawnposition.transform.position += moveSpawnPosition[countTomove % 4];
                GameObject bullet = ObjectPooler.SpawnFromPool("Bullet", _spawnposition.transform.position);
                countTomove++;
            }

            _elapsedTime = 0.0f;

        }
    }
}


