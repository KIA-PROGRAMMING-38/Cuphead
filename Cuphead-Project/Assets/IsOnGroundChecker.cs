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

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TurnOffJumpState();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_transform.position, _radiusSize);
    }
    public bool CheckIfPlayerIsOnGround()
    {
        return Physics2D.OverlapCircle(_transform.position, _radiusSize, whatIsGround);
    }


    public static bool isOnGround;
    public void TurnOffJumpState()
    {
        isOnGround = CheckIfPlayerIsOnGround();
        
        if (isOnGround)
        {
           
            _animator.SetBool(CupheadAnimID.IS_JUMPING, false);
            _bulletAnimator.SetBool(CupheadAnimID.IS_ON_GROUND, true);


        }
        else
        {

            _animator.SetBool(CupheadAnimID.IS_JUMPING, true);
            _bulletAnimator.SetBool(CupheadAnimID.IS_ON_GROUND, false);
        }
    }
}
