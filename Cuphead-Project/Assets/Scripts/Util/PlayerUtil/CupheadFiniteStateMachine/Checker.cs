//using UnityEngine;


//public abstract class Checker : IChecker
//{
//    //오버라이드 할 수 있게끔 가상함수로 만들어줍니다. 
//    [SerializeField]
//    Transform _transform;

//    [SerializeField]

//    Gizmos gizmo;

//    [SerializeField]
//    public LayerMask LayerToCheck;
//    float _radiusSize;


//    public virtual void ControlAnimator()
//    {

//    }
//    public virtual bool CheckOverlaying()
//    {
//        if (CupheadController.HasParried == true)
//        {

//            return Physics2D.OverlapCircle(_transform.position, _radiusSize, LayerToCheck);

//        }
//        return false;
//    }
//    public virtual void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(_transform.position, _radiusSize);
//    }

//}
