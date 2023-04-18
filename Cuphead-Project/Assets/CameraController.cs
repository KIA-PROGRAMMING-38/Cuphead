using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform _player;

    [SerializeField]
    Transform _cameraLeft;


    [SerializeField]
    Transform _cameraRight;


    private Vector3 cameraPosition;
    [SerializeField]
    Vector3 offset;
    [SerializeField] float followSpeed;
    // Update is called once per frame

    [SerializeField]
    float platformLength;


    void FixedUpdate()
    {
        Vector3 cameraPosition = new Vector3(_player.position.x / 15 + offset.x , transform.position.y, transform.position.z);
        transform.position =

           Vector3.Lerp(transform.position, cameraPosition, followSpeed * Time.deltaTime);

    }
}
