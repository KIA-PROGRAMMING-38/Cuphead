using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.VolumeComponent;

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

    private float homePositionY;
    void Start()
    {
        homePositionY = transform.position.y;
    }

    void FixedUpdate()
    {
       
        Vector3 cameraPosition = new Vector3(_player.position.x / 15 + offset.x, homePositionY, transform.position.z);
        transform.position =

        Vector3.Lerp(transform.position, cameraPosition, followSpeed * Time.deltaTime);

     

    }
    private void Update()
    {
        jitterCamera();
    }   


    /// <summary>
    /// 플레이어 피격시 카메라를 진동시키기 위한 함수입니다. 
    /// 플레이어 피격시 cuphead.controller에서 호출합니다. 
    /// </summary>


    [SerializeField]
    private float jitterPower;

  
    public void jitterCamera()
    {
       
        if (CupheadController.HasBeenHit)
        {
           
            float power = Random.Range(-jitterPower, jitterPower);
            transform.position += Vector3.one * power;
          
        }
    

    }


}
