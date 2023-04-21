using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotProjectilesSpawner : MonoBehaviour
{

    //캐럿 보스 체력
    [SerializeField]
    private static int CarrotHP = 30;


    Animator _animator;

    //CarrotProjectile을 랜덤으로 생성 할 상단 플랫폼을 두가지 구간으로 나누어줍니다. 
    [SerializeField]
    Transform _LeftSpawnPositionOfCarrotProjectile;
    [SerializeField]
    Transform _RightSpawnPositionOfCarrotProjectile;


  
    private int spawnPosition = 0;
    private readonly int SPAWN_POSITION_LEFT = 0;
    private readonly int SPAWN_POSITION_RIGHT = 10;

    private int PositionDeciderOfCarrotProjectile = 0;
    float durationOfHitMaterial = 0.15f;


    SpriteRenderer CarrotSpriteRenderer;


    private void Start()
    {
        gameObject.SetActive(false); //Carrot_background 객체에 의해 애니메이션 이벤트를 통해 호출됩니다. 
    }


    private void OnEnable()
    {
        _waitTimeForMaterial = new WaitForSeconds(durationOfHitMaterial);
        _animator = GetComponent<Animator>();
        CarrotSpriteRenderer = GetComponent<SpriteRenderer>();
      
    }

    
    [SerializeField]
    GameObject CarrotEye;

    public void ActivateCarrotEye() => CarrotEye.SetActive(true);
    public void DeactivateCarrotEye() => CarrotEye.SetActive(false);


    Vector3 spawnPositionMove;


    /// <summary>
    /// 당근 투사체 애니메이션 이벤트 입니다.
    /// 위치값을 랜덤으로 하도록 구성하였습니다. 
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
    /// 캐롯보스가 데미지를 입었을때 매터리얼을 변경하여
    /// 플레이어가 보스를 성공적으로 공격하였음을 알 수 있습니다. 
    /// </summary>
    /// <returns></returns>

    [SerializeField] Material _MaterialDuringDamaged;
    [SerializeField] Material _defaultMaterial;

    // 매터리얼을 바꿀 시간을 정해줍니다. 
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
   

  
   
    /// <summary>
    /// 레이저는 다른 투사체와 다르게,
    /// 플랫폼에 부딫혀야 사라지는 애니메이션이 재생됩니다.
    /// 
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>


}

