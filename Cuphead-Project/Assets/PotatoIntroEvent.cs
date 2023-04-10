using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoIntroEvent : MonoBehaviour
{
    [SerializeField]
    Animator potatoAnimator;
   
    
    void TurnOnPotatoAnimator()
    {
        potatoAnimator.enabled = true;
    }
}
