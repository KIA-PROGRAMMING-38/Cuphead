using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potatoBackgroundController : MonoBehaviour
{
    [SerializeField]
    GameObject _potato;


    void ActivatePotato()
    {
        _potato.SetActive(true);
    }
}
