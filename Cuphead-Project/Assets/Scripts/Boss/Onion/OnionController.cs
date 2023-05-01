using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class OnionController : MonoBehaviour
{
    [SerializeField]
    GameManager GameManager;

    SpriteRenderer OnionSprtieRenderer;

    [SerializeField]
    Material _hitMaterial;

    [SerializeField]
    Material _defaultMaterial;

    [SerializeField]
    WaitForSeconds _waitTimeForMaterial;


    [SerializeField]
    Animator _animator;

    [SerializeField]
    Animator _cryAnimatorLeft;

    [SerializeField]
    Animator _cryAnimatorRight;

    [SerializeField]
    public GameObject _carrot;
    [SerializeField]
    public Animator _carrotanimator;

    [SerializeField]
    GameObject Onion;

    Collider2D onionCollider;

    private static int OnionHP = 30;
    float durationOfHitMaterial = 0.15f;

    /// <summary>
    /// Start단계 에서는 호출되지않으며, 어니언의 Background 애니메이션 이벤트에 의해 활성화됩니다. 
    /// </summary>
    private void Start()
    {
        _fadeCoroutine = FadeCoroutine();
        gameObject.SetActive(false);
        onionCollider = GetComponent<Collider2D>();
    }


    private void OnEnable()
    {
        OnionSprtieRenderer = GetComponent<SpriteRenderer>();
        _waitTimeForMaterial = new WaitForSeconds(durationOfHitMaterial);
    }



    public void ActivateCarrot()
    {
        _carrot.SetActive(true);
        _carrotanimator.enabled = true;
    }

    public void DeactivateOnion() => Onion.SetActive(false);
    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (IsBulletCollision(collision))
        {
            DecreaseHP();
            CheckOnionAlive();
            changeMaterial();
        }

    }
    
   
    /// <summary>
    /// HP가 0이하가 된 경우 Death 애니메이션을 재생합니다. 
    /// </summary>
    private void CheckPotatoAlive()
    {
        if (OnionHP < 0) //죽는 순간 눈물 물줄기를 먼저 끄고 DIE실행
        {

            _animator.SetBool(CupheadAnimID.DIED, true);

        }
    }
    private void DeactivateSpriteRenderer()
    {
        OnionSprtieRenderer.enabled = false;
    }

    private void OnOnionDeath()
    {
      
        GameManager.OnOnionDead();
    }

    private void OnOnionDeadStart()
    {
        OnionCrySource.Stop();
        onionCollider.enabled = false;
        GameManager.OnOnionDeadStart();
    }



    /// <summary>
    /// OnTrigger와 함께 총알이 맞은 경우 체력을 -1 감소 시킵니다. 
    /// </summary>
    private static void DecreaseHP() => OnionHP -= 1;
    private void CheckOnionAlive()
    {
        if (OnionHP < 0)
        {
            _animator.SetBool(CupheadAnimID.DIED, true);
        }
    }

    private bool IsBulletCollision(Collider2D collision)
    {
        return collision.CompareTag(TagNames.BULLET);
    }
    /// <summary>
    /// 플레이어의 총알에 맞은 경우 쉐이더를 적용시켰다가, 해제합니다.
    /// </summary>
    public void changeMaterial()
    {
        OnionSprtieRenderer.material = _hitMaterial;
        StartCoroutine(TurnBackToOriginalMaterial());
    }
    IEnumerator TurnBackToOriginalMaterial()
    {
        yield return _waitTimeForMaterial;

        OnionSprtieRenderer.material = _defaultMaterial;
    }

    [SerializeField] SpriteRenderer _onionBackgroundRenderer;
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
                _onionBackgroundRenderer.color = Color.Lerp(START_COLOR, END_COLOR, _fadeTime / 0.3f);

                _fadeTime += Time.deltaTime;

                yield return null;
            }

            StopCoroutine(_fadeCoroutine);

            yield return null;
        }
    }

    [SerializeField] AudioSource OnionCrySource;
    [SerializeField] AudioClip OnionCryClip;

    public void PlayOnionCry()
    {
        OnionCrySource.clip= OnionCryClip;
        OnionCrySource.Play();
    }
    public void StopOnionCry()
    {
        OnionCrySource.Stop();
    }
}





