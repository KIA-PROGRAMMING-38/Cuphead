using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockOutUIScript : MonoBehaviour
{
    [SerializeField]
    RectTransform ReadyUItransform;

 

    [SerializeField]
    float _increasingSpeed = 0.5f;
    float targetWidth;
    float currentWidth;
  

    private void Awake()
    {
       
    }
    void FixedUpdate()
    {
        IncreaseWidth();
    }

    private void IncreaseWidth()
    {
        targetWidth = 1000f;  // 증가할 최대 width 값
        currentWidth = ReadyUItransform.sizeDelta.x;  // 현재 width 값

        // Mathf.Lerp 함수를 사용하여 서서히 width 값을 증가시킵니다.
        float newWidth = Mathf.Lerp(currentWidth, targetWidth, _increasingSpeed * Time.fixedDeltaTime);

        // 새로 계산된 width 값을 RectTransform의 sizeDelta 속성에 대입합니다.
        ReadyUItransform.sizeDelta = new Vector2(newWidth, ReadyUItransform.sizeDelta.y);
       
    }
   
   
   
}
