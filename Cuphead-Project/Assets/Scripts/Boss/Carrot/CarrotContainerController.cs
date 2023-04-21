using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotContainerController : MonoBehaviour
{
    [SerializeField] 
    GameObject _carrotBackground;
    void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        _carrotBackground.SetActive(true);
    }


}
