using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionRightTearController : MonoBehaviour
{
   
    /// <summary>
    /// Start단계 에서는 호출되지않으며, 어니언의 Cry 애니메이션 이벤트에 의해 활성화됩니다. 
    /// </summary>
    void Start()
    {
        gameObject.SetActive(false);
    }

   
}
