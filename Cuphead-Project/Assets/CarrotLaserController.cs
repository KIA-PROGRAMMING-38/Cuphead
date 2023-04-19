using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotLaserController : MonoBehaviour
{
    Rigidbody2D carrotRigidbody;

    SpriteRenderer projectileSpriteRenderer;
    Animator animator;
    [SerializeField]
    float _initialProjectileSpeed;


    new Collider2D collider;
    float _spawnMoveDistance;
    [SerializeField]
    Transform _playerTransform;

    [SerializeField]
    float _laserSpeed;






    Vector2 _lastlyDetectedPlayerPosition;
    private void OnEnable()
    {
        projectileSpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        carrotRigidbody = GetComponent<Rigidbody2D>();
        Invoke(nameof(DeactivateDelay), 5f);

        _lastlyDetectedPlayerPosition = _playerTransform.position;
    }

    private void FixedUpdate()
    {
        carrotRigidbody.position = Vector2.Lerp
        (transform.position, _lastlyDetectedPlayerPosition, _laserSpeed * Time.deltaTime);
    }

  
    void DeactivateDelay() => gameObject.SetActive(false)
;
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);

        CancelInvoke(); //코루틴과 다르게 반드시 해제해주어야 합니다. 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (HasBeenHitCollision(collision))
        {
            animator.SetBool(ProjectileAnimID.HIT_PLAYER, true);
        }
    }

    private bool HasBeenHitCollision(Collider2D collision)
    {
        return collision.CompareTag(TagNames.PLATFROM);
    }


    public void ShowLaser() => gameObject?.SetActive(true);
    public void HideLaser() => gameObject?.SetActive(false);
    public void DeactivatieLaserCollider() => collider.enabled = false;
}
