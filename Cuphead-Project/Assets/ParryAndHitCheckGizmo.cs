using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryAndHitCheckGizmo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static readonly float PLAYER_COLLIDER_GIZMO_SIZE = 0.6f;
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, PLAYER_COLLIDER_GIZMO_SIZE);
    }
}
