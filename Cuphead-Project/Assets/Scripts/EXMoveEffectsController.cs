using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EXMoveEffectsController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField] PeashotSpawner peashotSpawner;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
      

        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        if (peashotSpawner.isUp)
        {   
            transform.rotation = quaternion.Euler(0, 0, 90f);
        }
    }

    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();
    }

    void Deactivate() => gameObject.SetActive(false);
   
  
}
