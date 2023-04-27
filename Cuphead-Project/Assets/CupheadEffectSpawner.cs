using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class CupheadEffectSpawner : MonoBehaviour
{
    [SerializeField]
    PeashotSpawner peashotSpawner;


    [SerializeField]
    Transform _playerTransform;


    private void Start()
    {
        
    }

    public GameObject ActivateHitEffect()
    {
        return ObjectPooler.SpawnFromPool
            (ObjectPoolNameID.PLAYER_HIT_EFFECT, _playerTransform.position);

    }


    [SerializeField]
    Transform ExMovePositionLeft;
    [SerializeField]
    Transform ExMovePositionRight;
    [SerializeField]
    Transform ExMovePositionUp;
    public GameObject ActivateExMoveEffect()
    {
        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT 
            && !peashotSpawner.isUp)
        {
            return ObjectPooler.SpawnFromPool
            (ObjectPoolNameID.EX_MOVE_EFFECTS, ExMovePositionRight.position);
        }
        else if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT
             && !peashotSpawner.isUp)
            return ObjectPooler.SpawnFromPool
           (ObjectPoolNameID.EX_MOVE_EFFECTS, ExMovePositionLeft.position);
        else
            return ObjectPooler.SpawnFromPool
         (ObjectPoolNameID.EX_MOVE_EFFECTS, ExMovePositionUp.position);

    }
}
