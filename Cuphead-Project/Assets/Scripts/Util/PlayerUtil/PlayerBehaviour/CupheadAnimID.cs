using UnityEditor;
using UnityEngine;

public static class CupheadAnimID
{
    public static readonly int DIED = Animator.StringToHash("Died");

    public static readonly int RUN = Animator.StringToHash("Running");

    public static readonly int JUMP = Animator.StringToHash("Jumping");
    public static readonly int JUMP_AND_SHOOT = Animator.StringToHash("JumpingAndShooting");

    public static readonly int DUCK = Animator.StringToHash("Ducking");
    public static readonly int IS_DUCK_AND_SHOOOT = Animator.StringToHash("DuckingAndShooting");

    public static readonly int STOP_MOVING = Animator.StringToHash("StopMoving");


    public static readonly int SHOOT = Animator.StringToHash("Shooting");
    public static readonly int SHOOT_UP = Animator.StringToHash("ShootingUp");
    public static readonly int SHOOT_DOWN = Animator.StringToHash("ShootingDown");
    public static readonly int SHOOT_UPPER_SIDEWAYS = Animator.StringToHash("ShootingUpperSideways");
    public static readonly int SHOOT_BOTTOM_SIDEWAYS = Animator.StringToHash("ShootingBottomSideways");
  

    public static readonly int TRY_PARRYING = Animator.StringToHash("TryParrying");
    public static readonly int HAS_PARRIED = Animator.StringToHash("HasParried");

    public static readonly int EX_MOVE = Animator.StringToHash("ExMoving");

    public static readonly int ON_GROUND = Animator.StringToHash("IsOnGround");
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
    public static readonly string CARROT_BACKGROUND_PROJECTILE = "CarrotBackgroundProjectile";

    public static readonly string BULLET_SPARKER = "Bullet_Sparker";
    public static readonly string PLAYER_HIT_EFFECT = "PlayerHitEffect";
    public static readonly string EX_MOVE_EFFECTS = "ExMoveEffects";
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
    public static readonly string ENEMY= "Enemy";
}



