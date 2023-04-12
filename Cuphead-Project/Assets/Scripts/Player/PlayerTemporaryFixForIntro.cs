using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemporaryFixForIntro : MonoBehaviour
{
    
  
  

    /*
    플레이어 인트로 애니메이션 재생중에는 
     플레이어 컨트롤을 제한하기 위해서 코루틴을 사용해
     스크립트를 일정시간 정지 시켜 놓고, 활성화 합니다. 
    */
    void Start()
    {
        gameObject.GetComponent<CupheadController>().enabled = false;
        gameObject.GetComponent<PeashotSpawner>().enabled = false;
    }


    //이벤트 함수를 호출합니다. 
    void EnableScript() 
    {
        gameObject.GetComponent<CupheadController>().enabled = true;
        gameObject.GetComponent<PeashotSpawner>().enabled = true;
    }
}

