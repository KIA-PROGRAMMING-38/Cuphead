using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotBackgroundController : MonoBehaviour
{

    [SerializeField]
    GameObject Carrot;

    //애니메이션 이벤트로 활성화
    public void ActivateCarrot()
    {
        Carrot.SetActive(true);
    }

   



}
