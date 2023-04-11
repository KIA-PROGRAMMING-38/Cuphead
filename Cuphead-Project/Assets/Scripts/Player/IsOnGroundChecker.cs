using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOnGroundChecker : MonoBehaviour
{

    Animator _animator;

    [SerializeField]
    Animator _bulletAnimator;

    [SerializeField]
    Transform _transform;

    [SerializeField]
    float _radiusSize;

    [SerializeField]
    public LayerMask whatIsGround;


    void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }


    void Update()
    {
        TurnOffJumpState();
    }

    // 오버레이 서클을 씬 화면에서 볼 수 있도록 기즈모 함수를 작성했습니다.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_transform.position, _radiusSize);
    }

    //플레이어가 지면에 닿아있는지 지속적으로 검사합니다. 
    public bool CheckIfPlayerIsOnGround()
    {
        return Physics2D.OverlapCircle(_transform.position, _radiusSize, whatIsGround);
    }



    /*검사한 결과를 bool값으로 저장하고, 결과에 맞게 애니메이션
    파라미터 값을 조정해 줍니다.*/
    public static bool isOnGround;
    public void TurnOffJumpState()
    {
        isOnGround = CheckIfPlayerIsOnGround();

        if (isOnGround)
        {
            _animator.SetBool(CupheadAnimID.IS_JUMPING, false);
            _bulletAnimator.SetBool(CupheadAnimID.IS_ON_GROUND, true);
            _animator.SetBool(CupheadAnimID.IS_PARRYING, false);
            CupheadController.isJumping = false;
        }
        else
        {

            _animator.SetBool(CupheadAnimID.IS_JUMPING, true);
            _bulletAnimator.SetBool(CupheadAnimID.IS_ON_GROUND, false);
            CupheadController.isJumping = true;
        }
    }
}
