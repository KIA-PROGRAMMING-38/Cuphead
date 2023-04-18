using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpDust : MonoBehaviour
{
    SpriteRenderer projectile;   
    Animator animator;


    private void OnEnable()
    {
       
        //이후 패링객체 플레이어 상호작용 시 사용할 함수를 미리 작성했습니다.

        Invoke(nameof(DeactiveDelay), 1.5f);
        animator = GetComponent<Animator>();
        animator.enabled = true;
    }


    private void OnDisable()
    {
        // 패리어블 오브젝트 풀에 다시 리턴합니다. 
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();
    }

   
    void DeactiveDelay() => gameObject.SetActive(false);
   
  
}
