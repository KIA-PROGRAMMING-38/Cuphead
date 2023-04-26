
using System.Threading;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using static Bullet;

public class PeashotSpawner : MonoBehaviour
{
    [SerializeField]
    Animator _playerAnimator;


    [SerializeField]
    float _spawnCoolTime;

    [SerializeField]
    float _spawnMoveDistance;

    [SerializeField]
    Bullet bullet;


    float _elapsedTime;
    int countTomove = 0;

    Vector3[] moveSpawnPositionRight = new Vector3[4];
    Vector3[] moveSpawnPositionLeft = new Vector3[4];

    Vector3 originalPositionAndRotation;

    public int inputvecX { get; private set; }
    public int inputvecY { get; private set; }



    public bool isUp { get; set; }
    public bool isDown { get; private set; }
    public bool isRight { get; private set; }
    public bool isLeft { get; private set; }


    public bool isUpperRight { get; private set; }
    public bool isUpperLeft { get; private set; }

    public bool isBottomRight { get; private set; }
    public bool isBottomLeft { get; private set; }



    //총알이 위아래로 왔다갔다하면서 발사하기 위해서 배열을 만들어 대입합니다. 
    private void Awake()
    {


        moveSpawnPositionLeft[0] = Vector3.up * _spawnMoveDistance; // 중단
        moveSpawnPositionLeft[1] = Vector3.up * _spawnMoveDistance; //최상단
        moveSpawnPositionLeft[2] = Vector3.down * _spawnMoveDistance; // 중단
        moveSpawnPositionLeft[3] = Vector3.down * _spawnMoveDistance; //최하단

        moveSpawnPositionRight[0] = Vector3.up * _spawnMoveDistance; // 중단
        moveSpawnPositionRight[1] = Vector3.up * _spawnMoveDistance; //최상단
        moveSpawnPositionRight[2] = Vector3.down * _spawnMoveDistance; // 중단
        moveSpawnPositionRight[3] = Vector3.down * _spawnMoveDistance; //최하단
    }

    enum Direction
    {
        up = 0,
        down = 0,
        topLeft = -1,
        topright = 1,
        bottomLeft = -1,
        bottomRigt = 1
    }





    private void Update()
    {
        SetShootingDirection();

        // 스폰 시 포지션을 정해줍니다. 
        // 숙인 경우(Ducking) 발사 위치를 밑으로 정해줍니다.

    }



    [SerializeField]
    GameObject _spawnpositionDuckingRight;

    [SerializeField]
    GameObject _spawnpositionDuckingLeft;

    readonly float TIME_ZERO = 0f;
    /// <summary>
    /// 플레이어가 Ducking 상태일때 발사체위치 조정 및 풀에서 꺼내오기
    /// </summary>
    private void ShootBulletDucking()
    {

        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
        {
            GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _spawnpositionDuckingRight.transform.position);
            _elapsedTime = TIME_ZERO;
        }
        else if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_LEFT)
        {
            GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _spawnpositionDuckingLeft.transform.position);
            _elapsedTime = TIME_ZERO;
        }
    }




    private void ShootWhileJumping()
    {
        if (CupheadController.IsShooting)
        {
            if (isUp)
            {
                GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootUpPosition.transform.position);
                _elapsedTime = TIME_ZERO;
            }

            else if (isDown)
            {
                GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootDownPosition.transform.position);
                _elapsedTime = TIME_ZERO;
            }

            else if(isUpperRight)
            {
                GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootTopRightPosition.transform.position);
                _elapsedTime = TIME_ZERO;
            }
            else if(isUpperLeft)
            {
                GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootTopLeftPosition.transform.position);
                _elapsedTime = TIME_ZERO;
            }

            else if(isRight)
            {
                spawnPositionRight = _shootRightPosition.transform.position + moveSpawnPositionRight[countTomove % 4];
                countTomove++;
                _elapsedTime = TIME_ZERO;
                GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", spawnPositionRight);
                //초기화
                spawnPositionRight = _shootRightPosition.transform.position;

            }
            else if(isLeft)
            {
                spawnPositionLeft = _shootLeftPosition.transform.position + moveSpawnPositionLeft[countTomove % 4];
                countTomove++;
                _elapsedTime = TIME_ZERO;
                GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", spawnPositionLeft);
                //초기화
                spawnPositionLeft = _shootLeftPosition.transform.position;
            }

            else if(isBottomRight)
            {
                GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootBottomRightPosition.transform.position);
                _elapsedTime = TIME_ZERO;
            }
            else if(isBottomLeft)
            {
                GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootBottomLeftPosition.transform.position);
                _elapsedTime = TIME_ZERO;
            }
        }

    }

    /// <summary>
    /// 플레이어가 옆으로 발사할때 (위,아래,대각선 X) 발사체위치 조정 및 풀에서 꺼내오기
    /// </summary>

    [SerializeField] GameObject _shootRightPosition;
    [SerializeField] GameObject _shootLeftPosition;
    Vector3 spawnPositionRight;
    Vector3 spawnPositionLeft;

    private void ShootSideways()
    {
        Debug.Log($"ShootSideways():  {bullet.MoveDirection}");
        if (isRight)
        {
            spawnPositionRight = _shootRightPosition.transform.position + moveSpawnPositionRight[countTomove % 4];
            countTomove++;
            _elapsedTime = TIME_ZERO;
            GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", spawnPositionRight);
            //초기화
            spawnPositionRight = _shootRightPosition.transform.position;


        }
        if (isLeft)
        {
            spawnPositionLeft = _shootLeftPosition.transform.position + moveSpawnPositionLeft[countTomove % 4];
            countTomove++;
            _elapsedTime = TIME_ZERO;
            GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", spawnPositionLeft);
            //초기화
            spawnPositionLeft = _shootLeftPosition.transform.position;


        }
    }





    /// <summary>
    /// 플레이어가 위로 발사할때 (대각선 X) 발사체위치 조정 및 풀에서 꺼내오기
    /// </summary>
    [SerializeField] GameObject _shootUpPosition;
    [SerializeField] GameObject _shootDownPosition;

    private void ShootingUp()
    {
        GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootUpPosition.transform.position);
        _elapsedTime = TIME_ZERO;

    }



    /// <summary>
    /// 플레이어가 아래로 발사할때 (대각선 X) 발사체위치 조정 및 풀에서 꺼내오기
    /// </summary>

    private void ShootingDown()
    {

        GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootDownPosition.transform.position);
        _elapsedTime = TIME_ZERO;

    }






    /// <summary>
    /// 플레이어가 대각선 발사 시, 발사체위치 조정 및 풀에서 꺼내오기
    /// </summary>
    [SerializeField] GameObject _shootTopRightPosition;
    [SerializeField] GameObject _shootTopLeftPosition;
    private void ShootingTopSidewatys()
    {
        Debug.Log($"ShootingTopSidewatys():  {bullet.MoveDirection}");
        if (isUpperRight)
        {
            GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootTopRightPosition.transform.position);
            _elapsedTime = TIME_ZERO;
        }
        if (isUpperLeft)
        {
            GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootTopLeftPosition.transform.position);
            _elapsedTime = TIME_ZERO;
        }
    }


    [SerializeField] GameObject _shootBottomRightPosition;
    [SerializeField] GameObject _shootBottomLeftPosition;
    private void ShootingBottomSideways()
    {
        Debug.Log($"ShootingBottomSideways():  {bullet.MoveDirection}");
        if (isBottomRight)
        {
            GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootBottomRightPosition.transform.position);
            _elapsedTime = TIME_ZERO;
        }
        if (isBottomLeft)
        {
            GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootBottomLeftPosition.transform.position);
            _elapsedTime = TIME_ZERO;
        }
    }







    private void SetShootingDirection()
    {
        inputvecX = Mathf.CeilToInt(CupheadController._inputVec.x);
        inputvecY = Mathf.CeilToInt(CupheadController._inputVec.y);


        isDown = inputvecY < 0;
        isRight = inputvecX > 0;
        isLeft = inputvecX < 0;
        isUp = inputvecY > 0;
        isUpperRight = inputvecY > 0 && inputvecX > 0;
        isUpperLeft = inputvecY > 0 && inputvecX < 0;
        isBottomRight = inputvecY < 0 && inputvecX > 0;
        isBottomLeft = inputvecY < 0 && inputvecX < 0;

        if (inputvecY == 0)
        {
            _playerAnimator.SetBool(CupheadAnimID.SHOOT_UP, false);
            _playerAnimator.SetBool(CupheadAnimID.SHOOT_DOWN, false);
            _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_SIDEWAYS, false);
            _playerAnimator.SetBool(CupheadAnimID.SHOOT_BOTTOM_SIDEWAYS, false);

            if (inputvecX == 0)
            {
                if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
                {
                    _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_SIDEWAYS, false);
                    _playerAnimator.SetBool(CupheadAnimID.SHOOT_BOTTOM_SIDEWAYS, false);
                    isRight = true;
                }
                if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_LEFT)
                {
                    _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_SIDEWAYS, false);
                    _playerAnimator.SetBool(CupheadAnimID.SHOOT_BOTTOM_SIDEWAYS, false);
                    isLeft = true;
                }
            }

        }


        // 애니메이션 선처리 
        if (isUp)
        {

            if (isRight)
            {
                _playerAnimator.SetBool(CupheadAnimID.SHOOT_UP, false);
                _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_SIDEWAYS, true);
            }
            else if (isLeft)
            {
                _playerAnimator.SetBool(CupheadAnimID.SHOOT_UP, false);
                _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_SIDEWAYS, true);
            }
            else
            {
                _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_SIDEWAYS, false);
                _playerAnimator.SetBool(CupheadAnimID.SHOOT_UP, true);
            }
        }


        else if (isDown)
        {


            if (isRight)
            {
                _playerAnimator.SetBool(CupheadAnimID.SHOOT_DOWN, false);
                _playerAnimator.SetBool(CupheadAnimID.SHOOT_BOTTOM_SIDEWAYS, true);
            }
            else if (isLeft)
            {
                _playerAnimator.SetBool(CupheadAnimID.SHOOT_DOWN, false);
                _playerAnimator.SetBool(CupheadAnimID.SHOOT_BOTTOM_SIDEWAYS, true);
            }

            else
            {
                _playerAnimator.SetBool(CupheadAnimID.SHOOT_BOTTOM_SIDEWAYS, false);
                _playerAnimator.SetBool(CupheadAnimID.SHOOT_DOWN, true);
            }

        }



    }

}

