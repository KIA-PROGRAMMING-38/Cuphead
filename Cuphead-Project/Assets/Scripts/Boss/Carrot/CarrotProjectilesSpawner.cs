using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotProjectilesSpawner : MonoBehaviour
{

    //캐럿 보스 체력
    [SerializeField]
    private static int CarrotHP = 30;

    [SerializeField]
    GameManager GameManager;

    //CarrotProjectile을 랜덤으로 생성 할 상단 플랫폼을 두가지 구간으로 나누어줍니다. 
    [SerializeField]
    Transform _LeftSpawnPositionOfCarrotProjectile;

    [SerializeField]
    Transform _RightSpawnPositionOfCarrotProjectile;

    [SerializeField]
    Animator _carrotEyeForLaserAnimator;

    [SerializeField]
    SpriteRenderer _carrotEyeForLaserSpriteRenderer;

    Vector3 decidedPositionToSpawnCarrotProjectile;
    Vector3 decidedPositionToSpawnBackgroundCarrotProjectile;

    private int spawnPosition = 0;
    private readonly int SPAWN_POSITION_LEFT = 0;
    private readonly int SPAWN_POSITION_RIGHT = 10;

    private int projectileCreationChance = 0;
    private int positionDeciderOfCarrotProjectile = 0;
    float durationOfHitMaterial = 0.15f;

    Animator _animator;
    SpriteRenderer carrotSpriteRenderer;


    private void Start()
    {
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        _waitTimeForMaterial = new WaitForSeconds(durationOfHitMaterial);
        _animator = GetComponent<Animator>();
        carrotSpriteRenderer = GetComponent<SpriteRenderer>();

    }


    [SerializeField]
    GameObject CarrotLaserSpawner;
    [SerializeField]
    GameObject CarrotEyes;

    public void ActivateCarrotThirdEyeObject()
    {
        CarrotLaserSpawner.SetActive(true);
    }

    public void DeactivateCarrotThirdEyeObject()
    {
        CarrotEyes.SetActive(false);
        CarrotLaserSpawner.SetActive(false);
    }


    Vector3 spawnPositionMove;



    [SerializeField]
    Transform _LeftSpawnPositionOfBackgroundCarrotProjectile;

    [SerializeField]
    Transform _RightSpawnPositionOfBackgroundCarrotProjectile;


    GameObject throwBackGroundCarrotProjectile()
    {
        // 백분위로 랜덤함수 판별
        projectileCreationChance = Random.Range(0, 100);

        positionDeciderOfCarrotProjectile = Random.Range(0, 100);

        Debug.Log(positionDeciderOfCarrotProjectile);

        if (projectileCreationChance > 35)
        {
            if (positionDeciderOfCarrotProjectile < 50)
            {
                float rangeToMovespawnPosition = Random.Range(0, 3);
                spawnPositionMove = new Vector3(rangeToMovespawnPosition, 0, 0);

                // 기준점(왼쪽,오른쪽 총 두개) 에서 랜덤값을 더한값을 최종값으로 입력.

                decidedPositionToSpawnCarrotProjectile
                    = _LeftSpawnPositionOfBackgroundCarrotProjectile.position
                    + spawnPositionMove;

                return ObjectPooler.SpawnFromPool
                (ObjectPoolNameID.CARROT_BACKGROUND_PROJECTILE, decidedPositionToSpawnCarrotProjectile);
            }

            else
            {
                float rangeToMovespawnPosition = Random.Range(0, 3);
                spawnPositionMove = new Vector3(rangeToMovespawnPosition, 0, 0);

                // 기준점(왼쪽,오른쪽 총 두개) 에서 랜덤값을 더한값을 최종값으로 입력.

                decidedPositionToSpawnCarrotProjectile =
                    _RightSpawnPositionOfBackgroundCarrotProjectile.position +
                    spawnPositionMove;

                return ObjectPooler.SpawnFromPool
                (ObjectPoolNameID.CARROT_BACKGROUND_PROJECTILE, decidedPositionToSpawnCarrotProjectile);
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 당근 투사체 애니메이션 이벤트 입니다.
    /// 위치값을 랜덤으로 하도록 구성하였습니다. 
    /// </summary>
    /// <returns></returns>
    GameObject throwCarrotProjectile()
    {

        if (projectileCreationChance > 35)
        {
            positionDeciderOfCarrotProjectile = Random.Range(0, 100);
            // 백분위로 랜덤함수 판별
            positionDeciderOfCarrotProjectile = Random.Range(0, 100);

            //너무 많은 당근투사체 생성 방지를 위한 랜덤변수.
            projectileCreationChance = Random.Range(0, 100);
            Debug.Log(positionDeciderOfCarrotProjectile);

            if (positionDeciderOfCarrotProjectile < 50)
            {
                float rangeToMovespawnPosition = Random.Range(0, 3);
                spawnPositionMove = new Vector3(rangeToMovespawnPosition, 0, 0);

                // 기준점(왼쪽,오른쪽 총 두개) 에서 랜덤값을 더한값을 최종값으로 입력.
                decidedPositionToSpawnBackgroundCarrotProjectile =
                _LeftSpawnPositionOfCarrotProjectile.position + spawnPositionMove;

                return ObjectPooler.SpawnFromPool
                (ObjectPoolNameID.CARROT_PROJECTILE, decidedPositionToSpawnBackgroundCarrotProjectile);
            }

            else if (positionDeciderOfCarrotProjectile > 50)
            {
                float rangeToMovespawnPosition = Random.Range(0, 3);
                spawnPositionMove = new Vector3(rangeToMovespawnPosition, 0, 0);

                // 기준점(왼쪽,오른쪽 총 두개) 에서 랜덤값을 더한값을 최종값으로 입력.
                decidedPositionToSpawnBackgroundCarrotProjectile
                = _RightSpawnPositionOfCarrotProjectile.position
                + spawnPositionMove;

                return ObjectPooler.SpawnFromPool
                (ObjectPoolNameID.CARROT_PROJECTILE, decidedPositionToSpawnBackgroundCarrotProjectile);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 캐롯보스가 데미지를 입었을때 매터리얼을 변경하여
    /// 플레이어가 보스를 성공적으로 공격하였음을 알 수 있습니다. 
    /// </summary>
    /// <returns></returns>

    [SerializeField] Material _MaterialDuringDamaged;
    [SerializeField] Material _defaultMaterial;

    [SerializeField] SpriteRenderer _carrotEyeSpriteRenderer;

    // 매터리얼을 바꿀 시간을 정해줍니다. 
    WaitForSeconds _waitTimeForMaterial;
    private static float hitMaterialDurationTime = 0.18f;
    public void changeMaterial()
    {
        carrotSpriteRenderer.material = _MaterialDuringDamaged;
        _carrotEyeSpriteRenderer.material = _MaterialDuringDamaged;
        StartCoroutine(TurnBackToOriginalMaterial());
    }


    IEnumerator TurnBackToOriginalMaterial()
    {
        yield return _waitTimeForMaterial;
        _carrotEyeSpriteRenderer.material = _defaultMaterial;
        carrotSpriteRenderer.material = _defaultMaterial;
    }

    public void OnCarrotDead()
    {
        GameManager.OnCarrotDead();
        GameManager.StopTimerAndLoadNewScene();
    }

    /// <summary>
    /// 데미지를 맞으면 HP를 1씩 감소 시킵니다. 
    /// 필살기 데미지는 3으로 구현 할 계획입니다. 
    /// </summary>
    private static void DecreaseHP() => CarrotHP -= 1;
    /// <summary>
    /// 보스가 죽었는지 계속해서 검사합니다. 
    /// 추후에 이벤트로 처리 할 수 있는 함수 구조인 것 같습니다. 
    /// </summary>
    private void CheckCarrotAlive()
    {
        if (CarrotHP < 0)
        {
            _animator.SetBool(ProjectileAnimID.DEAD, true);
        }
    }

    private bool IsBulletCollision(Collider2D collision)
    {
        return collision.CompareTag(TagNames.BULLET);
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (IsBulletCollision(collision))
        {

            DecreaseHP();
            CheckCarrotAlive();
            changeMaterial();
        }

    }

    /// <summary>
    /// 캐롯의 두번째 공격 방식이며, 투사체 방식이 끝난 직후
    /// 호출하기 위해서 아래와 같이 _carrotLaserAnimator를 받아오고
    /// 재생하여줍니다. 
    /// </summary>



    public void EnableAnimatorAndSpriteCarrotEyeDuringLaserShoot()
    {
        _carrotEyeForLaserAnimator.enabled = true;
        _carrotEyeForLaserSpriteRenderer.enabled = true;
    }
    public void disableAnimatorAndSpriteCarrotEyeDuringLaserShoot()
    {
        _carrotEyeForLaserAnimator.enabled = false;
        _carrotEyeForLaserSpriteRenderer.enabled = false;
    }




    /// <summary>
    /// 레이저는 다른 투사체와 다르게,
    /// 플랫폼에 부딫혀야 사라지는 애니메이션이 재생됩니다.
    /// 
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>


}

