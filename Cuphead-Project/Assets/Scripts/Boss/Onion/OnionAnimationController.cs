using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionAnimationController : MonoBehaviour
{
    [SerializeField] 
    Animator _animatorLeft;
    [SerializeField]
    Animator _animatorRight;

    [SerializeField]
    GameObject _cryStreamLeft;

    [SerializeField]
    GameObject _cryStreamRight;
    public void SetBoolFalseOfWaterStream()
    {
        _animatorLeft.SetBool("IsOnionCrying", false);
        _animatorRight.SetBool("IsOnionCrying", false);
    }


    public void DeactivateWaterStream()
    {
        _cryStreamRight.SetActive(false);
        _cryStreamLeft.SetActive(false);
    }


    public void SetBoolTrueOfWaterStream()
    {
        _animatorLeft.SetBool("IsOnionCrying", true);
        _animatorRight.SetBool("IsOnionCrying", true);
    }



    public void ActivateWaterStream()
    {
        _cryStreamRight.SetActive(true);
        _cryStreamLeft.SetActive(true);
    }
}
