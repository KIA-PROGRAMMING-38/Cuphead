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
        _playerSpriteRenderer = CupheadController._playerSpriteRenderer;

        if (_playerSpriteRenderer.flipX == false)
        {
            if (CupheadController.isDucking == false)
                transform.position = _bulletSparkPositionRight.position;
            else transform.position = _bulletSparkPositionDuckingRight.position;
        }

        else if (_playerSpriteRenderer.flipX == true)
        {
            if (CupheadController.isDucking == false)
                transform.position = _bulletSparkPositionLeft.position;
            else transform.position = _bulletSparkPositionDuckingLeft.position;
        }
    }

}
