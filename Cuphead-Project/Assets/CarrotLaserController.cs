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
    Rigidbody2D _carrotEye;

    [SerializeField]
    float _laserSpeed;

    //플레이어 움직임 추적을 한번만 하도록 하기위한 bool 변수 설정 
    private bool isDead;

    
    
    

    private void OnEnable()
    {
       
        isDead = false;
        projectileSpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        carrotRigidbody = GetComponent<Rigidbody2D>();
        Invoke(nameof(DeactivateDelay), 5f);
    }


    private void FixedUpdate()
    {
        if (!isDead)
        {
            _lastlyDetectedPlayerPosition.y = -4f;
            Vector2 extrapolation = (_lastlyDetectedPlayerPosition - _carrotEye.position).normalized;
        carrotRigidbody.position = Vector2.Lerp
        (transform.position, _lastlyDetectedPlayerPosition +(10 * extrapolation), _laserSpeed * Time.deltaTime);
        }
        
    }
   
    
    public static Vector2 _lastlyDetectedPlayerPosition;

    [SerializeField]
    LaserSpawner laserSpawner;
    
  
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
            animator.SetBool(ProjectileAnimID.DEAD, true);
            isDead = true;
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
