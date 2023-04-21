using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireAnimationController : MonoBehaviour
{
    // 애니메이션 클립 가져오기


    [SerializeField]
    Animator animator;



    /// <summary>
    /// 에니메이션 역재생 함수입니다.
    /// </summary>
    void RewindAnim()
    {
        animator.SetFloat("Reverse", -1f);
    }

    /// <summary>
    /// 에니메이션 역재생이 끝난 후 다시 시작하게끔 해주는 함수입니다.
    /// </summary>
    void StartAnim()
    {
        animator.SetFloat("Reverse", 1f);
    }
}


