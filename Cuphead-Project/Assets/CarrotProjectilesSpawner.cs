using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotProjectilesSpawner : MonoBehaviour
{
    Animator _animator;

    [SerializeField]
    PotatoIntroEvent potatoIntroEvent;


    [SerializeField]
    Transform _LeftSpawnPositionOfCarrotProjectile;
    [SerializeField]
    Transform _RightSpawnPositionOfCarrotProjectile;

    [SerializeField]
    Transform _LaserSpawnPosition;

    private static int CarrotHP = 30;

    private int spawnPosition = 0;
    private readonly int SPAWN_POSITION_LEFT = 0;
    private readonly int SPAWN_POSITION_RIGHT = 10;

    private int tearDecider = 0;
    private readonly float PARRYABLE_TEAR = 90;
    private readonly float NORMAL_TEAR = 0;
    private static float hitMaterialDurationTime = 0.15f;
    WaitForSeconds _waitTimeForMaterial;
    SpriteRenderer PotatoSpriteRenderer;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        PotatoSpriteRenderer = GetComponent<SpriteRenderer>();
        _waitTimeForMaterial = new WaitForSeconds(hitMaterialDurationTime);
    }

    Vector3 _decidedSpawnpositionLeft;
    Vector3 _decidedSpawnpositionRight;


    [SerializeField]
    OnionBackgroundController onionBackgroundController;


    int count = 0;
    readonly int bossProjectileCounts = 3;
    Vector3 spawnPositionMove;

    /// <summary>
    /// 당근 투사체 애니메이션 이벤트 
    /// </summary>
    /// <returns></returns>
    GameObject throwCarrotProjectile()
    {
        spawnPosition = Random.Range(SPAWN_POSITION_LEFT, SPAWN_POSITION_RIGHT);
        tearDecider = Random.Range(0, 100); // 백분위로 랜덤함수 판별.

        //기준값 초기화.
        _RightSpawnPositionOfCarrotProjectile.position = Vector3.zero;
        _LeftSpawnPositionOfCarrotProjectile.position = Vector3.zero;

        Debug.Log(spawnPosition);

        if (spawnPosition < 5)
        {
            float rangeToMovespawnPosition = Random.Range(0, 7);
            spawnPositionMove = new Vector3(rangeToMovespawnPosition, 0, 0);

            // 기준점(왼쪽,오른쪽 총 두개) 에서 랜덤값을 더한값을 최종값으로 입력.
            _decidedSpawnpositionLeft =
            _LeftSpawnPositionOfCarrotProjectile.position + spawnPositionMove;

            return ObjectPooler.SpawnFromPool
            (ObjectPoolNameID.CARROT_PROJECTILE, _decidedSpawnpositionLeft);
        }

        else
        {
            float rangeToMovespawnPosition = Random.Range(0, 7);
            spawnPositionMove = new Vector3(rangeToMovespawnPosition, 0, 0);

            // 기준점(왼쪽,오른쪽 총 두개) 에서 랜덤값을 더한값을 최종값으로 입력.
            _decidedSpawnpositionRight =
            _RightSpawnPositionOfCarrotProjectile.transform.position + spawnPositionMove;


            return ObjectPooler.SpawnFromPool
            (ObjectPoolNameID.CARROT_PROJECTILE, _decidedSpawnpositionRight);
        }
    }

    /// <summary>
    /// 애니메이션 이벤트로 중복재생 할 함수 재생
    /// </summary>
    /// <returns></returns>
    GameObject throwLaserProjectile()
    {
        return ObjectPooler.SpawnFromPool
                (ObjectPoolNameID.CARROT_LASER, _LaserSpawnPosition.transform.position);
    }



    private static void DecreaseHP() => CarrotHP -= 1;
    private void CheckPotatoAlive()
    {
        if (CarrotHP < 0)
        {
            _animator.SetBool(CupheadAnimID.DIED, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (IsBulletCollision(collision))
        {
            DecreaseHP();
            CheckPotatoAlive();
            changeMaterial();
        }

    }
    private bool IsBulletCollision(Collider2D collision)
    {
        return collision.CompareTag(TagNames.BULLET);
    }

    void TurnOffBackround()
    {
        potatoIntroEvent.Deactive();
        Deactive();
    }
    public void Deactive()
    {
        gameObject.SetActive(false);
    }


    [SerializeField] Material _MaterialDuringDamaged;
    [SerializeField] Material _defaultMaterial;
    public void changeMaterial()
    {
        PotatoSpriteRenderer.material = _MaterialDuringDamaged;
        StartCoroutine(TurnBackToOriginalMaterial());
    }

    IEnumerator TurnBackToOriginalMaterial()
    {
        yield return _waitTimeForMaterial;
        PotatoSpriteRenderer.material = _defaultMaterial;
    }


    /// <summary>
    /// 캐롯 사망 씬 후 게임매니져 호출
    /// </summary>
    /// 


    public void SetActiveOnionBackground()
    {
    }




    public void ActivateOnionAndBackground()
    {

    }
}

