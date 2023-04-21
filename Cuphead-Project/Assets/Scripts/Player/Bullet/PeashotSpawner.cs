
using System.Threading;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class PeashotSpawner : MonoBehaviour
{
    [SerializeField]
    Animator _playerAnimator;
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

    Vector3 originalPositionAndRotation;



    //총알이 위아래로 왔다갔다하면서 발사하기 위해서 배열을 만들어 대입합니다. 
    private void Awake()
    {
        originalPositionAndRotation = _spawnposition.transform.position;
        moveSpawnPosition[0] = Vector3.up * _spawnMoveDistance;
        moveSpawnPosition[1] = Vector3.up * _spawnMoveDistance;
        moveSpawnPosition[2] = Vector3.down * _spawnMoveDistance;
        moveSpawnPosition[3] = Vector3.down * _spawnMoveDistance;
    }

    enum UpperDirection
    {
        up = 0,
        down = 0,
        topLeft = -1,
        topright = 1,
        bottomLeft = -1,
        bottomRigt = 1
    }

    int inputvecX;
    int inputvecY;



    private void Update()
    {
        ShootPeashot();

        // 스폰 시 포지션을 정해줍니다. 
        // 숙인 경우(Ducking) 발사 위치를 밑으로 정해줍니다.

    }

    private void ShootBulletDucking()
    {
      GameObject bullet = ObjectPooler.SpawnFromPool("Bullet", _spawnpositionDucking.transform.position);
      _elapsedTime = 0.0f;

    }
    private void ShootBullet()
    {
        _spawnposition.transform.position += moveSpawnPosition[countTomove % 4];
        GameObject bullet = ObjectPooler.SpawnFromPool("Bullet", _spawnposition.transform.position);
        countTomove++;
        _elapsedTime = 0.0f;
    }
     
    
           
    private void TurnOffAnimator()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            _bulletSparkAnimator.SetBool(BulletAnimID.IS_LAUNCHED, false);
         
        }
    }


    private void ShootPeashot()
    {
        _elapsedTime += Time.deltaTime;

        // 총알 발사 시간 제한
        if (Input.GetKey(KeyCode.X) && _elapsedTime > _spawnCoolTime)
        {
            // 발사 방향 계산
            int inputvecX = Mathf.CeilToInt(CupheadController._inputVec.x);
            int inputvecY = Mathf.CeilToInt(CupheadController._inputVec.y);
            bool isUp = inputvecY > 0;
            bool isDown = inputvecY < 0;
            bool isRight = inputvecX > 0;
            bool isLeft = inputvecX < 0;

            // 애니메이션 처리
            if (isUp)
            {
                if (isRight) _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_RIGHT, true);
                else if (isLeft) _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_LEFT, true);
                else _playerAnimator.SetBool(CupheadAnimID.SHOOT_UP, true);
            }
            else if (isDown)
            {
                if (isRight) _playerAnimator.SetBool(CupheadAnimID.SHOOT_BOTTOM_RIGHT, true);
                else if (isLeft) _playerAnimator.SetBool(CupheadAnimID.SHOOT_BOTTOM_LEFT, true);
                else _playerAnimator.SetBool(CupheadAnimID.SHOOT_DOWN, true);
            }

            // 총알 발사
            ShootBullet();
        }


        else return;

        
    }

    //private void ShootPeashot()
    //{
    //    _elapsedTime += Time.deltaTime;
    //    inputvecX = Math.Max((int)Math.Ceiling(CupheadController._inputVec.x), 0);
    //    inputvecY = Math.Max((int)Math.Ceiling(CupheadController._inputVec.y), 0);


    //    // 총알 발사시간을 제한하는 조건을 넣어, 지나치게 많은 총알이 생성되는 것을 방지합니다. 
    //    if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.C) && _elapsedTime > _spawnCoolTime)
    //    {

    //        if (inputvecY == 0)
    //        {
    //            if (inputvecY > 0)
    //            {
    //                switch (inputvecX)
    //                {
    //                    case (int)UpperDirection.topright:
    //                        _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_RIGHT, true);
    //                        _spawnposition.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
    //                        break;

    //                    case (int)UpperDirection.topLeft:
    //                        _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_LEFT, true);
    //                        _spawnposition.transform.rotation = Quaternion.Euler(0f, 0f, 135f);
    //                        break;

    //                    case (int)UpperDirection.up:
    //                        _playerAnimator.SetBool(CupheadAnimID.SHOOT_UP, true);
    //                        _spawnposition.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    //                        break;

    //                    default:
    //                        break;
    //                }

    //            }
    //            else
    //            {
    //                switch (inputvecX)
    //                {
    //                    case (int)UpperDirection.bottomRigt:
    //                        _playerAnimator.SetBool(CupheadAnimID.SHOOT_BOTTOM_RIGHT, true);
    //                        _spawnposition.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
    //                        break;

    //                    case (int)UpperDirection.bottomLeft:
    //                        _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_LEFT, true);
    //                        _spawnposition.transform.rotation = Quaternion.Euler(0f, 0f, -135f);
    //                        break;

    //                    case (int)UpperDirection.down:
    //                        _playerAnimator.SetBool(CupheadAnimID.SHOOT_DOWN, true);
    //                        _spawnposition.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
    //                        break;


    //                    default:
    //                        break;
    //                }
    //            }
    //        }
    //        ShootBullet();
    //    }

    //    else if (Input.GetKey(KeyCode.X) && _elapsedTime > _spawnCoolTime)
    //    {
    //        if (inputvecY > 0)
    //        {
    //            switch (inputvecX)
    //            {
    //                case (int)UpperDirection.topright:
    //                    _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_RIGHT, true);
    //                    break;

    //                case (int)UpperDirection.topLeft:
    //                    _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_LEFT, true);
    //                    break;

    //                case (int)UpperDirection.up:
    //                    _playerAnimator.SetBool(CupheadAnimID.SHOOT_UP, true);
    //                    break;

    //                default:
    //                    break;
    //            }

    //        }
    //        else
    //        {
    //            switch (inputvecX)
    //            {
    //                case (int)UpperDirection.bottomRigt:
    //                    _playerAnimator.SetBool(CupheadAnimID.SHOOT_BOTTOM_RIGHT, true);
    //                    break;

    //                case (int)UpperDirection.bottomLeft:
    //                    _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_LEFT, true);
    //                    break;

    //                default:
    //                    break;
    //            }
    //        }
    //        ShootBullet();
    //    }
    //}
}


