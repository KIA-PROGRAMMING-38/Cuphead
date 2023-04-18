using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnionTearSpawner : MonoBehaviour
{
 
    SpriteRenderer OnionSprtieRenderer;

    [SerializeField] Material _hitMaterial;
    [SerializeField] Material _defaultMaterial;
    [SerializeField]  float _waitingTime;
    [SerializeField] public Animator _animator;

    private static int OnionHP = 10;
    private void OnEnable()
    {
        OnionSprtieRenderer = GetComponent<SpriteRenderer>();
    }


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
    WaitForSeconds _waitTimeForMaterial = new WaitForSeconds(0.15f);
    IEnumerator TurnBackToOriginalMaterial()
    {
        yield return _waitTimeForMaterial;

        OnionSprtieRenderer.material = _defaultMaterial;
    }
}
