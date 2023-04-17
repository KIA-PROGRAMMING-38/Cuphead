using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potatoProjectileSpawner : MonoBehaviour
{


    [SerializeField] float _waitingTime;
    public Animator _animator;

    [SerializeField]
    PotatoIntroEvent potatoIntroEvent;

    [SerializeField]
    float _spawnCoolTime;

    private static int PotatoHp = 10;

    private void Awake()
    {

    }
    [SerializeField]
    GameObject _spawnposition;

    [SerializeField]
    OnionBackgroundController onionBackgroundController;


    int count = 0;
    readonly int bossProjectileCounts = 3;
    GameObject throwProjectile()
    {
        if (count < bossProjectileCounts)
        {
            count++;
            return ObjectPooler.SpawnFromPool
                ("PotatoProjectile", _spawnposition.transform.position);

        }
        else
        {
            count = 0;
            return ObjectPooler.SpawnFromPool
                ("Parryable", _spawnposition.transform.position);

        }
    }

    /// <summary>
    /// 애니메이션 이벤트로 중복재생 할 함수 재생
    /// </summary>
    /// <returns></returns>
    protected GameObject throwParryable()
    {
        return ObjectPooler.SpawnFromPool
                ("Parryable", _spawnposition.transform.position);
    }



    private static void DecreaseHP() => PotatoHp -= 1;
    private void CheckPlayerAlive()
    {
        if (PotatoHp == 0)
        {
            _animator.SetBool(CupheadAnimID.DIED, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (IsPlatformCollision(collision))
        {
            DecreaseHP();
            CheckPlayerAlive();
        }

    }
    private bool IsPlatformCollision(Collider2D collision)
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

    /// <summary>
    /// 포테이터 사망 애니메이션 후, 양파 호출 함수 
    /// </summary>
    /// 
    [SerializeField]
    Renderer onionRenderer;
    [SerializeField]
    Animator onionAnimator;
    [SerializeField]
    GameObject onionBackground;

    public void SetActiveOnionBackground()
    {
        StartCoroutine(SetActiveOnion());
    }

    WaitForSeconds waitTime = new WaitForSeconds(1.0f);
    IEnumerator SetActiveOnion()
    {
        yield return waitTime;
        ActivateOnionAndBackground();
    }

    public void ActivateOnionAndBackground()
    {
        onionBackground.SetActive(true);
        onionAnimator.enabled = true;
        onionRenderer.enabled = true;
    }
}


