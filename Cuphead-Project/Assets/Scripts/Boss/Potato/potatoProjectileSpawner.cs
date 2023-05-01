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

    Collider2D potatoCollider;
    private static int PotatoHp = 50;


    private void Start()
    {
        _fadeCoroutine = FadeCoroutine();

        potatoCollider = GetComponent<Collider2D>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        PotatoSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Potato 사망시 게임매니저에서 발생하는 이벤트 
    public void OnpPotatoDeathStart()
    {//여기에 추후에 흐려지는 쉐이더 적용시키면 될듯?
        potatoCollider.enabled = false;
        //감자가 죽으면 Background보다 뒤에 그려져고 사라져야 자연스럽기에 1로 바꿔줍니다. 
        PotatoSpriteRenderer.sortingOrder = 1;
        GameManager.OnPotatoDeadStart();
    }
    public void OnpPotatoDeath()
    {//여기에 추후에 흐려지는 쉐이더 적용시키면 될듯?
        GameManager.OnPotatoDead();

    }
    private void DeactivatePotatoSpriteRenderer()
    {
        PotatoSpriteRenderer.enabled = false;
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


    [SerializeField] GameObject Potato_shoot_animator;

    public void ActivatePotatoShootEffect()
    {
        Potato_shoot_animator.SetActive(true);
    }

    public void DectivatePotatoShootEffect()
    {
        Potato_shoot_animator.SetActive(false);
    }


    [SerializeField] SpriteRenderer _potatoBackgroundRenderer;
    [SerializeField] float _dcreasingSpeed;

    private float _fadeTime;


    public void StartFadeCoroutine()
    {
        _fadeTime = 0;
        StartCoroutine(_fadeCoroutine);
    }

    private readonly Color START_COLOR = new(1, 1, 1, 1);
    private readonly Color END_COLOR = new(1, 1, 1, 0);

    private IEnumerator _fadeCoroutine;
    private IEnumerator FadeCoroutine()
    {
        while (true)
        {
            while (_fadeTime < 0.3f)
            {
                _potatoBackgroundRenderer.color = Color.Lerp(START_COLOR, END_COLOR, _fadeTime / 0.3f);

                _fadeTime += Time.deltaTime;

                yield return null;
            }

            StopCoroutine(_fadeCoroutine);

            yield return null;
        }
    }

    [SerializeField]
    AudioSource _bossAudioSource;
    [SerializeField]
    AudioClip _potatoProjectileClip;

    public void StartPotatoProjectileSound()
    {
        _bossAudioSource.clip = _potatoProjectileClip;
        _bossAudioSource.PlayOneShot(_potatoProjectileClip);
    }

}


