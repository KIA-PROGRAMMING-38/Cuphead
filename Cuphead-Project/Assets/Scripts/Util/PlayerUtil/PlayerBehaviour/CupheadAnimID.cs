using UnityEditor;
using UnityEngine;

public static class CupheadAnimID
{
    public static readonly int DIED = Animator.StringToHash("Died");
    public static readonly int IS_RUNNING = Animator.StringToHash("Running");
    public static readonly int IS_JUMPING = Animator.StringToHash("Jumping");
    public static readonly int IS_JUMPING_AND_SHOOTING = Animator.StringToHash("JumpingAndShooting");
    public static readonly int IS_DUCKING = Animator.StringToHash("Ducking");
    public static readonly int IS_DUCKING_AND_SHOOOTING = Animator.StringToHash("DuckingAndShooting");
    public static readonly int IS_STANDSHOOTING = Animator.StringToHash("StandShooting");
    public static readonly int TRY_PARRYING = Animator.StringToHash("TryParrying");
    public static readonly int HAS_PARRIED = Animator.StringToHash("HasParried");
    public static readonly int IS_EX_MOVING = Animator.StringToHash("ExMoving");
    public static readonly int IS_ON_GROUND = Animator.StringToHash("IsOnGround");
    public static readonly int HAS_BEEN_HIT = Animator.StringToHash("HasBeenHit");
}

public static class BulletAnimID
{
    
    public static readonly int PARRIED = Animator.StringToHash("Parried"); 
    public static readonly int IS_LAUNCHED = Animator.StringToHash("IsLaunched");
    public static readonly int HIT_ENEMY_OR_ITS_PROJECTILES = Animator.StringToHash("IsHit");
}

public static class ProjectileAnimID
{
    public static readonly int DEAD = Animator.StringToHash("Dead");
    public static readonly int PARRIED = Animator.StringToHash("Parried");
    public static readonly int HIT_PLAYER = Animator.StringToHash("HitPlayer");
}
public static class ObjectPoolNameID
{

    public static readonly string POTATO_PROJECTILE_PARRYABLE = "PotatoProjectileParryable";
    public static readonly string POTATO_PROJECTILE = "PotatoProjectile";
    public static readonly string ONION_TEARS = "NormalTear";
    public static readonly string ONION_TEARS_PARRYABLE = "ParryableTear";
    public static readonly string EX_MOVE = "ExMove";
    public static readonly string CARROT_PROJECTILE = "CarrotProjectile";
    public static readonly string CARROT_LASER = "CarrotLaser";
}

public static class EffectNames
{
    public static readonly string PLAYER_DUST_ON = "PlayerJumpDustOn";
}
public static class LayerNames
{
    public static readonly string PLATFORM = "Platform";
}

public static class TagNames
{
    public static readonly string BULLET = "Bullet";
    public static readonly string PROJECTILE = "Projectile";
    public static readonly string PARRYABLE = "Parryable";
    public static readonly string PLATFROM = "Platform";
    public static readonly string PLAYER = "Player";
}



