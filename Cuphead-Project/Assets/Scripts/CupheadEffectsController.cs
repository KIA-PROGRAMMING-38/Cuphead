using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CupheadEffectsController : MonoBehaviour
{
    public delegate void CustomEventHandler();
    public static event CustomEventHandler ParryEffect;

    

    // 이벤트 발생 함수
    public void SendCustomEvent()
    {
            ParryEffect?.Invoke();
        
        // CustomEvent 이벤트 발생
    }

    public void SetParryEffectTrue()
    {
        SendCustomEvent();
    }


   
}
