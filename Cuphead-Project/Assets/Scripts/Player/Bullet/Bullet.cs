using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D _bulletRigidbody;

    [SerializeField]
    SpriteRenderer peashotSpriteRenderer;

    [SerializeField]
    SpriteRenderer _playerSpriteRenderer;


    //벡터의 값을 미리 룩업테이블로 저장.
    Vector2[] _bulletForceTable = new Vector2[]
    {
        new Vector2(0f, 30f), // up
        new Vector2(0f, -30f), // down
        new Vector2(30f, 0f), // right
        new Vector2(-30f, 0f), // left
        new Vector2(30f, 30f), // top right
        new Vector2(-30f, 30f), // top left
        new Vector2(30f, -30f), // bottom right
        new Vector2(-30f, -30f) // bottom left
    };

    Quaternion[] rotationTable = new Quaternion[]
    {
     Quaternion.Euler(0f, 0f, 90f),
     Quaternion.Euler(0f, 0f, -90f),
     Quaternion.Euler(0f, 0f, 0f),
     Quaternion.Euler(0f, 0f, -180f),
     Quaternion.Euler(0f, 0f, 45f),
     Quaternion.Euler(0f, 0f, 135f),
     Quaternion.Euler(0f, 0f, -45f),
     Quaternion.Euler(0f, 0f, -135f),
    };


    bool[,] flipTable = new bool[,]
   {
        {false, false}, // up
        {true, false},  // down
        {false, false}, // right
        {true, false},  // left
        {false, false}, // top-right
        {true, true},   // top-left
        {false, true},  // bottom-right
        {true, true}    // bottom-left
   };

    Vector2 bulletDirection;

    [SerializeField]
    BulletHitChecker bulletHitChecker;

    [SerializeField]
    PeashotSpawner peashotSpawner;

    public enum LaunchDirection
    {
        Up,
        Down,
        Right,
        Left,
        TopRight,
        TopLeft,
        BottomRight,
        BottomLeft
    }

    public LaunchDirection moveDirection { get; private set; }
    public LaunchDirection MoveDirection { get; private set; }

    private void Start()
    {

    }

    public void Update()
    {
        _bulletRigidbody.velocity = bulletDirection;
        if (bulletHitChecker.CheckBulletIsHit())
        {
            _bulletRigidbody.velocity = Vector2.zero;
            Invoke(nameof(DeactiveDelay), 0.2f);
        }
    }

    private void OnEnable()
    {
        if (peashotSpawner == null) return;

        if (peashotSpawner.isUp)
        {
            peashotSpriteRenderer.flipY = false;

            if (peashotSpawner.isRight) moveDirection = LaunchDirection.TopRight;
            else if (peashotSpawner.isLeft)
            {
                peashotSpriteRenderer.flipX = true;
                moveDirection = LaunchDirection.TopLeft;
            }
            else moveDirection = LaunchDirection.Up;

        }
        else if (peashotSpawner.isDown)
        {
            peashotSpriteRenderer.flipY = true;

            if (peashotSpawner.isRight) moveDirection = LaunchDirection.BottomRight;
            else if (peashotSpawner.isLeft)
            {
                peashotSpriteRenderer.flipX = true;
                moveDirection = LaunchDirection.BottomLeft;
            }
            else moveDirection = LaunchDirection.Down;
        }
        else if (peashotSpawner.isRight)
        {
            peashotSpriteRenderer.flipX = false;
            moveDirection = LaunchDirection.Right;
        }
        else if (peashotSpawner.isLeft)
        {
            peashotSpriteRenderer.flipX = true;
            moveDirection = LaunchDirection.Left;
        }
        else // 전부아니라면 기본값을 할당.
        {
            if(CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
            {
                moveDirection = LaunchDirection.Right;
                peashotSpriteRenderer.flipX = false;
            }
            else if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_LEFT)
            {
                moveDirection = LaunchDirection.Left;
                peashotSpriteRenderer.flipX = true;
            }

        }

        transform.rotation = rotationTable[(int)moveDirection];
        peashotSpriteRenderer.flipX = flipTable[(int)moveDirection, 0] ;
        peashotSpriteRenderer.flipY = flipTable[(int)moveDirection, 1] ;
       

        //할당된 값을 Lookuptable로 호출
        _bulletRigidbody.velocity = _bulletForceTable[(int)moveDirection];

        bulletDirection = _bulletRigidbody.velocity;



        MoveDirection = moveDirection; // 자료전달용으로 MoveDirection에 저장
    }

    void DeactiveDelay() => gameObject.SetActive(false)
;
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);

        CancelInvoke(); //unlike coroutine, using Invoke have to be used with CancelInvoke
    }




}