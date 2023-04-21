using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotProjectilesSpawner : MonoBehaviour
{
    Animator _animator;

   


    [SerializeField]
    Transform _LeftSpawnPositionOfCarrotProjectile;
    [SerializeField]
    Transform _RightSpawnPositionOfCarrotProjectile;


    private static int CarrotHP = 30;
    private int spawnPosition = 0;
    private readonly int SPAWN_POSITION_LEFT = 0;
    private readonly int SPAWN_POSITION_RIGHT = 10;

    private int PositionDeciderOfCarrotProjectile = 0;
    float durationOfHitMaterial = 0.15f;


    SpriteRenderer CarrotSpriteRenderer;


    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        _waitTimeForMaterial = new WaitForSeconds(durationOfHitMaterial);
        _animator = GetComponent<Animator>();
        CarrotSpriteRenderer = GetComponent<SpriteRenderer>();
      
    }

    Vector3 _decidedSpawnpositionLeft;
    Vector3 _decidedSpawnpositionRight;


    
    [SerializeField]
    GameObject CarrotEye;

    public void ActivateCarrotEye() => CarrotEye.SetActive(true);
    public void DeactivateCarrotEye() => CarrotEye.SetActive(false);


    Vector3 spawnPositionMove;

    /// <summary>
    /// 당근 투사체 애니메이션 이벤트 
    /// </summary>
    /// <returns></returns>
    GameObject throwCarrotProjectile()
    {
       
        // 백분위로 랜덤함수 판별.

        PositionDeciderOfCarrotProjectile = Random.Range(0, 100);

        Debug.Log(PositionDeciderOfCarrotProjectile);

        if (PositionDeciderOfCarrotProjectile < 50)
        {
            float rangeToMovespawnPosition = Random.Range(0, 3);
            spawnPositionMove = new Vector3(rangeToMovespawnPosition, 0, 0);
            Debug.Log("당근 왼쪽");
            // 기준점(왼쪽,오른쪽 총 두개) 에서 랜덤값을 더한값을 최종값으로 입력.
          
            _LeftSpawnPositionOfCarrotProjectile.position += spawnPositionMove;

            return ObjectPooler.SpawnFromPool
            (ObjectPoolNameID.CARROT_PROJECTILE, _LeftSpawnPositionOfCarrotProjectile.position);
        }

        else
        {
            float rangeToMovespawnPosition = Random.Range(0, 3);
            spawnPositionMove = new Vector3(rangeToMovespawnPosition, 0, 0);
            Debug.Log("당근 오른쪽");
            // 기준점(왼쪽,오른쪽 총 두개) 에서 랜덤값을 더한값을 최종값으로 입력.
          
            _RightSpawnPositionOfCarrotProjectile.position += spawnPositionMove;

            return ObjectPooler.SpawnFromPool
            (ObjectPoolNameID.CARROT_PROJECTILE, _RightSpawnPositionOfCarrotProjectile.position);
        }
    }

    /// <summary>
    /// 애니메이션 이벤트로 재생 할 함수들
    /// </summary>
    /// <returns></returns>

    [SerializeField] Material _MaterialDuringDamaged;
    [SerializeField] Material _defaultMaterial;

    WaitForSeconds _waitTimeForMaterial;
    private static float hitMaterialDurationTime = 0.18f;
    public void changeMaterial()
    {
        CarrotSpriteRenderer.material = _MaterialDuringDamaged;
        StartCoroutine(TurnBackToOriginalMaterial());
    }


    IEnumerator TurnBackToOriginalMaterial()
    {
        yield return _waitTimeForMaterial;
        CarrotSpriteRenderer.material = _defaultMaterial;
    }

  

    private static void DecreaseHP() => CarrotHP -= 1;
    private void CheckCarrotAlive()
    {
        if (CarrotHP < 0)
        {
            _animator.SetBool(ProjectileAnimID.DEAD, true);
        }
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
    [SerializeField]
    GameObject _carrotLaserAnimator;

    void ActivateCarrotEyeDuringLaserShoot()
    {
        _carrotLaserAnimator.SetActive(true);
    }
    void DectivateCarrotEyeDuringLaserShoot()
    {
        _carrotLaserAnimator.SetActive(false);
    }
    private bool IsBulletCollision(Collider2D collision)
    {
        return collision.CompareTag(TagNames.BULLET);
    }

  
   
    /// <summary>
    /// 레이저는 다른 투사체와 다르게,
    /// 플랫폼에 부딫혀야 사라지는 애니메이션이 재생됩니다.
    /// 
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>


}

