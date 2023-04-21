using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionBackgroundController : MonoBehaviour
{
    [SerializeField] GameObject _onionBody;

 
    public void ActivateOnion()
    {
        _onionBody.SetActive(true);
    }
}
