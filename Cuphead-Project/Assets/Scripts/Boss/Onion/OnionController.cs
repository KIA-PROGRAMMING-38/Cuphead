using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionController : MonoBehaviour
{


    SpriteRenderer OnionSprtieRenderer;

    [SerializeField] Material _hitMaterial;
    [SerializeField] Material _defaultMaterial;
    [SerializeField] float _waitingTime;
    [SerializeField] Animator _animator;

    [SerializeField] Animator _cryAnimatorLeft;
    [SerializeField] Animator _cryAnimatorRight;

    private static int OnionHP = 30;
    float durationOfHitMaterial = 0.15f;
    [SerializeField]
    WaitForSeconds _waitTimeForMaterial;
    private void OnEnable()
    {
        OnionSprtieRenderer = GetComponent<SpriteRenderer>();
        _waitTimeForMaterial = new WaitForSeconds(durationOfHitMaterial);
    }

    [SerializeField]
    public GameObject _carrot;
    [SerializeField]
    public Animator _carrotanimator;

    [SerializeField]
    GameObject Onion;

    public void ActivateCarrot()
    {
        _carrot.SetActive(true);
        _carrotanimator.enabled= true;
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
    private void CheckPotatoAlive()
    {
        if (OnionHP < 0)
        {
            _animator.SetBool(CupheadAnimID.DIED, true);
        }
    }

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


    public void SetLeftCryAnimActive() => _cryAnimatorLeft.enabled= true;
    public void SetRightCryAnimActive() => _cryAnimatorRight.enabled = true;
    public void SetLeftCryAnimDeactivate() => _cryAnimatorLeft.enabled = false;
    public void SetRightCryAnimDeactivate() => _cryAnimatorRight.enabled = false;
}
