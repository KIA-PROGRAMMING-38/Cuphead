using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordBoardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
