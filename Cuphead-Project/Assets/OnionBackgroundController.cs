using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionBackgroundController : MonoBehaviour
{
    [SerializeField]
    Renderer onionRenderer;
    [SerializeField]
    Animator onionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetActive()
    {
        onionAnimator.enabled = true;
        onionRenderer.enabled = true;
    }
}
