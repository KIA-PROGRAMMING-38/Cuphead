using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoIntroEvent : MonoBehaviour
{
    [SerializeField]
    Animator potatoAnimator;

    [SerializeField]
    SpriteRenderer potatoSpriteRenderer;


    void TurnOnPotatoAnimator()
    {
        potatoAnimator.enabled = true;
        potatoSpriteRenderer.enabled = true;
    }

    public void Deactive()
    {
       gameObject.SetActive(false);
    }
}
