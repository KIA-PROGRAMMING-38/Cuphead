using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpark : MonoBehaviour
{



    [SerializeField]
    Animator _bulletSparkAnimator;

    [SerializeField]
    GameObject _bulletsparkGameObject;

    [SerializeField]
    Transform _bulletSparkPositionUp;
    [SerializeField]
    Transform _bulletSparkPositionDown;
    [SerializeField]
    Transform _bulletSparkPositionRight;
    [SerializeField]
    Transform _bulletSparkPositionLeft;

    [SerializeField]
    Transform _bulletSparkPositionTopLeft;
    [SerializeField]
    Transform _bulletSparkPositionTopRight;
    [SerializeField]
    Transform _bulletSparkPositionBottomLeft;
    [SerializeField]
    Transform _bulletSparkPositionBottomRight;

    [SerializeField]
    Transform _bulletSparkPositionTopLeftRunning;
    [SerializeField]
    Transform _bulletSparkPositionTopRightRunning;
    [SerializeField]
    Transform _bulletSparkPositionRightRunning;
    [SerializeField]
    Transform _bulletSparkPositionLeftRunning;

    [SerializeField]
    Transform _bulletSparkPositionDuckingRight;
    [SerializeField]
    Transform _bulletSparkPositionDuckingLeft;


    WaitForSeconds _waitSeconds;
    float waitTime = 0.4f;


    private void Start()
    {
        _bulletsparkGameObject.SetActive(false);
    }



    [SerializeField]
    Transform _bulletSparkPositionUpRight;
    [SerializeField]
    Transform _bulletSparkPositionUpLeft;

    public GameObject ActivatePeashotSparkUp()
    {
        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
            _bulletSparkPositionUpRight.position);
        }
        else if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_LEFT)
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
           _bulletSparkPositionUpLeft.position);
        }
        else return null;
     
    }

    [SerializeField]
    Transform _bulletSparkPositionDownRight;
    [SerializeField]
    Transform _bulletSparkPositionDownLeft;
    public GameObject ActivatePeashotSparkDown()
    {
        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
            _bulletSparkPositionDownRight.position);
        }
        else if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_LEFT)
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
           _bulletSparkPositionDownLeft.position);
        }
        else return null;
    }


    public GameObject ActivatePeashotSparkSideways()
    {

        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
            _bulletSparkPositionRight.position);
        }
        else if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_LEFT)
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
           _bulletSparkPositionLeft.position);
        }

        else
        {
            return null;
        }


    }



    public GameObject ActivatePeashotSparkBottomSideways()
    {

        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
            _bulletSparkPositionBottomRight.position);
        }
        else
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
         _bulletSparkPositionBottomLeft.position);
        }


    }

    public GameObject ActivatePeashotSparkTopSideways()
    {

        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
       _bulletSparkPositionTopRight.position);


        }
        else
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
       _bulletSparkPositionTopLeft.position);

        }


    }



    public GameObject ActivatePeashotSparkSidewaysRunning()
    {

        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
      _bulletSparkPositionRightRunning.position);

        }
        else
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
      _bulletSparkPositionLeftRunning.position);

        }


    }


    public GameObject ActivatePeashotSparkDuckingSideways()
    {

        if (CupheadController.playerDirection == CupheadController.PLAYER_DIRECTION_RIGHT)
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
     _bulletSparkPositionDuckingRight.position);

        }
        else
        {
            return ObjectPooler.SpawnFromPool(ObjectPoolNameID.BULLET_SPARKER,
    _bulletSparkPositionDuckingLeft.position);
        }


    }



    [SerializeField]
    PeashotSpawner peashotSpawner;
    public void ActivatePeashotSparkJumping()
    {

        if (CupheadController.IsShooting)
        {
            if (peashotSpawner.isUp)
            {
                ActivatePeashotSparkUp();
            }

            else if (peashotSpawner.isDown)
            {
                ActivatePeashotSparkDown();
            }

            else if (peashotSpawner.isUpperRight)
            {
                ActivatePeashotSparkTopSideways();
            }
            else if (peashotSpawner.isUpperLeft)
            {
                ActivatePeashotSparkTopSideways();
            }

            else if (peashotSpawner.isRight)
            {
                ActivatePeashotSparkBottomSideways();
            }
            else if (peashotSpawner.isLeft)
            {
                ActivatePeashotSparkBottomSideways();
            }

            else if (peashotSpawner.isBottomRight)
            {
                ActivatePeashotSparkBottomSideways();
            }
            else if (peashotSpawner.isBottomLeft)
            {
                ActivatePeashotSparkBottomSideways();
            }



        }


    }
}