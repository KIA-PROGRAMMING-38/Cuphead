
using System.Threading;
using UnityEngine;

public class PeashotSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _spawnposition;

    [SerializeField]
    Animator _bulletSparkAnimator;

    [SerializeField]
    float _spawnCoolTime;

    [SerializeField]
    float _spawnMoveDistance;
    float _elapsedTime;
    int countTomove = 0;

    Vector3[] moveSpawnPosition = new Vector3[4];


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

        if (Input.GetKey(KeyCode.X) && _elapsedTime > _spawnCoolTime)
        {
            if (IsOnGroundChecker.isOnGround == true)
            { _bulletSparkAnimator.SetBool(BulletAnimID.IS_LAUNCHED, true); }

            GameObject bullet = ObjectPooler.SpawnFromPool("Bullet", _spawnposition.transform.position);

            //the position can't be directly changed with other valuables
            _spawnposition.transform.position += moveSpawnPosition[countTomove % 4];
            _elapsedTime = 0.0f;
            countTomove++;
        }
    }
}


