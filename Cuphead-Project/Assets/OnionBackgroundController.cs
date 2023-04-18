using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionBackgroundController : MonoBehaviour
{
    [SerializeField] GameObject onionBody;
   
    void Start()
    {
        
    }


    void Update()
    {
        
    }
   
    public void ActivateOnion()
    {
        onionBody.SetActive(true);
    }
}
