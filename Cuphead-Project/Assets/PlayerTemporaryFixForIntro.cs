using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemporaryFixForIntro : MonoBehaviour
{
    
    [SerializeField] float _playwaitingTime;
  

    /*
    플레이어 인트로 애니메이션 재생중에는 
     플레이어 컨트롤을 제한하기 위해서 코루틴을 사용해
     스크립트를 일정시간 정지 시켜 놓고, 활성화 합니다. 
    */
    void Start()
    {
        gameObject.GetComponent<CupheadController>().enabled = false;
        StartCoroutine(AnimatorDelayer());

    }


    //_playwatingTime 만큼 스크립트를 비활성화 시킵니다. 
    IEnumerator AnimatorDelayer()
    {
        yield return new WaitForSeconds(_playwaitingTime);
        gameObject.GetComponent<CupheadController>().enabled = true;
    }
}
