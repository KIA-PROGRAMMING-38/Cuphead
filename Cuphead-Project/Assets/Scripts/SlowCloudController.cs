using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCloudController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float startPosition;
    [SerializeField] private float endPosition;

   

    private void Start()
    {
     
    }

    private void Update()
    {
        // 구름을 왼쪽으로 이동
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // 구름이 endPosition에 도달하면 startPosition으로 되돌아감
        if (transform.position.x <= endPosition)
        {
            transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
        }
    }
}
