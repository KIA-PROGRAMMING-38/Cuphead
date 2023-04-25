
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
    GameObject _spawnposition;

    [SerializeField]
    GameObject _spawnpositionDucking;

    [SerializeField]
    Animator _bulletSparkAnimator;

    [SerializeField]
    float _spawnCoolTime;

    [SerializeField]
    float _spawnMoveDistance;

    [SerializeField]
    Bullet bullet;


    float _elapsedTime;
    int countTomove = 0;

    Vector3[] moveSpawnPosition = new Vector3[4];

    Vector3 originalPositionAndRotation;

    public int inputvecX { get; private set; }
    public int inputvecY { get; private set; }



    public bool isUp { get; private set; }
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

        originalPositionAndRotation = _spawnposition.transform.position;
        moveSpawnPosition[0] = Vector3.up * _spawnMoveDistance; // 중단
        moveSpawnPosition[1] = Vector3.up * _spawnMoveDistance; //최상단
        moveSpawnPosition[2] = Vector3.down * _spawnMoveDistance; // 중단
        moveSpawnPosition[3] = Vector3.down * _spawnMoveDistance; //최하단
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




    readonly float TIME_ZERO = 0f;
    private void ShootBulletDucking()
    {

        GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _spawnpositionDucking.transform.position);
        _elapsedTime = TIME_ZERO;
    }




    [SerializeField] GameObject _shootRightPosition;
    [SerializeField] GameObject _shootLeftPosition;
    private void ShootSideways()
    {
        Debug.Log($"ShootSideways():  {bullet.MoveDirection}");
        if (isRight)

        {
            _spawnposition.transform.position += moveSpawnPosition[countTomove % 4];
            GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootRightPosition.transform.position);
            countTomove++;
            _elapsedTime = TIME_ZERO;
        }
        if (isLeft)
        {
            _spawnposition.transform.position += moveSpawnPosition[countTomove % 4];
            GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootLeftPosition.transform.position);
            countTomove++;
            _elapsedTime = TIME_ZERO;
        }
    }




    [SerializeField] GameObject _shootUpPosition;
    [SerializeField] GameObject _shootDownPosition;

    private void ShootingUp()
    {
        GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootUpPosition.transform.position);
        _elapsedTime = TIME_ZERO;

    }



    private void ShootingDown()
    {

        GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootDownPosition.transform.position);
        _elapsedTime = TIME_ZERO;

    }





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
            GameObject Bullet = ObjectPooler.SpawnFromPool("Bullet", _shootTopRightPosition.transform.position);
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




    private void TurnOffAnimator()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            _bulletSparkAnimator.SetBool(BulletAnimID.IS_LAUNCHED, false);

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

        if(inputvecY==0 && inputvecX == 0)
        {
            if(CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
            {
                isRight = true;
            }
            if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_LEFT)
            {
                isLeft = true;
            }
        }


        // 애니메이션 선처리 
        if (isUp)
        {
            _playerAnimator.SetBool(CupheadAnimID.SHOOT_DOWN, false);

            if (isRight) _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_SIDEWAYS, true);
            else if (isLeft) _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_SIDEWAYS, true);
            else _playerAnimator.SetBool(CupheadAnimID.SHOOT_UP, true);
        }


        else if (isDown)
        {
            _playerAnimator.SetBool(CupheadAnimID.SHOOT_UP, false);

            if (isRight) _playerAnimator.SetBool(CupheadAnimID.SHOOT_BOTTOM_SIDEWAYS, true);
            else if (isLeft) _playerAnimator.SetBool(CupheadAnimID.SHOOT_BOTTOM_SIDEWAYS, true);
            else _playerAnimator.SetBool(CupheadAnimID.SHOOT_DOWN, true);

        }

        else
        {
            _playerAnimator.SetBool(CupheadAnimID.SHOOT_UPPER_SIDEWAYS, false);
            _playerAnimator.SetBool(CupheadAnimID.SHOOT_UP, false);
            _playerAnimator.SetBool(CupheadAnimID.SHOOT_BOTTOM_SIDEWAYS, false);
            _playerAnimator.SetBool(CupheadAnimID.SHOOT_DOWN, false);
        }

    }

}

