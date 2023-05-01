using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUIScript : MonoBehaviour
{
    [SerializeField]
    RectTransform ReadyUItransform;

    [SerializeField]
    Image _introImage;

    [SerializeField]
    Sprite _wallop;

    [SerializeField]
    Sprite _wallop2;

    [SerializeField]
    Animator _introAnimator;

    [SerializeField]
    float _increasingSpeed = 0.5f;
    float targetWidth;
    float currentWidth;

    WaitForSeconds waitTimeForWallop;
    WaitForSeconds blinkInterval;

    [SerializeField]
    float waitTimeForwallopFloat;
    [SerializeField]
    float _blinkInterval;

    private void Awake()
    {
        waitTimeForWallop = new WaitForSeconds(waitTimeForwallopFloat);
        blinkInterval = new WaitForSeconds(_blinkInterval);
        _introAnimator.enabled = false;
    }
    void FixedUpdate()
    {
        IncreaseWidth();
        ChangeImage();
    }

    private void IncreaseWidth()
    {
        targetWidth = 1000f;  // 증가할 최대 width 값
         currentWidth = ReadyUItransform.sizeDelta.x;  // 현재 width 값

        // Mathf.Lerp 함수를 사용하여 서서히 width 값을 증가시킵니다.
        float newWidth = Mathf.Lerp(currentWidth, targetWidth, _increasingSpeed * Time.fixedDeltaTime);

        // 새로 계산된 width 값을 RectTransform의 sizeDelta 속성에 대입합니다.
        ReadyUItransform.sizeDelta = new Vector2(newWidth, ReadyUItransform.sizeDelta.y);
        StartCoroutine(ChangeImage());
    }
    [SerializeField]
    int totalCount;
    int currentCount = 0;
    IEnumerator ChangeImage()
    {
            yield return waitTimeForWallop;
        
       while(currentCount < totalCount )
        {
            _introImage.sprite = _wallop;
            yield return blinkInterval;
            _introImage.sprite = _wallop2;
            yield return blinkInterval;
            currentCount++;
        }

        gameObject.SetActive(false);
            StopCoroutine(ChangeImage());

    }
}
