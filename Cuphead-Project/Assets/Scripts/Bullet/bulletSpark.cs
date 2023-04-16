using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpark : MonoBehaviour
{
    SpriteRenderer _playerSpriteRenderer;

    [SerializeField]
    Transform _bulletSparkPositionRight;
    [SerializeField]
    Transform _bulletSparkPositionLeft;
    [SerializeField]
    Transform _bulletSparkPositionDuckingRight;
    [SerializeField]
    Transform _bulletSparkPositionDuckingLeft;


    private void OnEnable()
    {
       
    }

    public void Update()
    {
        
        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
        {
            if (CupheadController.IsDucking == false)
                transform.position = _bulletSparkPositionRight.position;
            else transform.position = _bulletSparkPositionDuckingRight.position;
        }

        else if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_LEFT)
        {
            if (CupheadController.IsDucking == false)
                transform.position = _bulletSparkPositionLeft.position;
            else transform.position = _bulletSparkPositionDuckingLeft.position;
        }
    }

}
