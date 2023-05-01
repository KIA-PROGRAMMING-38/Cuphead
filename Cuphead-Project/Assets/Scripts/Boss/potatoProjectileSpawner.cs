using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potatoProjectileSpawner : MonoBehaviour
{
    public GameManager GameManager;

    [SerializeField]
    GameObject potatoObject;

    [SerializeField]
    GameObject _potatoBackground;

    [SerializeField]
    GameObject _spawnposition;

    [SerializeField] Material _MaterialDuringDamaged;
    [SerializeField] Material _defaultMaterial;

    Animator _animator;

    SpriteRenderer PotatoSpriteRenderer;

    private static int PotatoHp = 30;


    private void Start()
    {
      gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        PotatoSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnpPotatoDeath()
    {//여기에 추후에 흐려지는 쉐이더 적용시키면 될듯?
        GameManager.OnPotatoDead();
    }

    public void DeactivatePotato()
    {
        
    }
    /// <summary>
    /// 투사체를 던지는 함수입니다.
    /// 4번째는 패리객체를 던질 수 있게끔 작성했습니다. 
    /// </summary>
    /// <returns></returns>
    /// 
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





    private static void DecreaseHP() => PotatoHp -= 1;
    private void CheckPotatoAlive()
    {
        if (PotatoHp < 0)
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

 
  

    /// <summary>
    /// 보스가 총알에 맞을때 데미지를 입는다는 인터페이스를 위해 
    /// 매티리얼을 바꿔주는 함수입니다. 
    /// </summary>
   
    public void changeMaterial()
    {
        PotatoSpriteRenderer.material = _MaterialDuringDamaged;
        StartCoroutine(TurnBackToOriginalMaterial());
    }
    WaitForSeconds _waitTimeForMaterial = new WaitForSeconds(0.15f);
    IEnumerator TurnBackToOriginalMaterial()
    {
        yield return _waitTimeForMaterial;

        PotatoSpriteRenderer.material = _defaultMaterial; 
    }


    /// <summary>
    /// 포테이터 사망 애니메이션 후, 양파 호출 함수입니다.
    /// 게임매니져로 변경해주어야합니다. 
    /// </summary>

    //[SerializeField]
    //GameObject _onion;


    //public void SetActiveOnionBackground()
    //{
    //    StartCoroutine(SetActiveOnion());
    //}

    //WaitForSeconds waitTime = new WaitForSeconds(1.0f);
    //IEnumerator SetActiveOnion()
    //{
    //    yield return waitTime;
    //    ActivateOnion();
    //}



    ///// <summary>
    ///// 포테이토가 죽은경우 발동하는 애니메이션 이벤트 함수 입니다.
    ///// 포테이토가 먼저 Deactivate되지 않도록 합니다. 
    ///// </summary>
    //public void ActivateOnion()
    //{
    //    Debug.Log("양파호출!");
    //    _onion.SetActive(true);
    //}



}


