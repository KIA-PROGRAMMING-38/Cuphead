using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionContainerController : MonoBehaviour
{
    [SerializeField]
    GameObject _onionBackground;
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _onionBackground.SetActive(true);
    }

}
