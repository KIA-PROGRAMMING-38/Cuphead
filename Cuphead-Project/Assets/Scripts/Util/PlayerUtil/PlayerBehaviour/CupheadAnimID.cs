using UnityEditor;
using UnityEngine;

public static class CupheadAnimID
{
    
    public static readonly int IS_RUNNING = Animator.StringToHash("Running");
    public static readonly int IS_JUMPING = Animator.StringToHash("Jumping");
    public static readonly int IS_JUMPING_AND_SHOOTING = Animator.StringToHash("JumpingAndShooting");
    public static readonly int IS_DUCKING = Animator.StringToHash("Ducking");
    public static readonly int IS_DUCKING_AND_SHOOOTING = Animator.StringToHash("DuckingAndShooting");
    public static readonly int IS_STANDSHOOTING = Animator.StringToHash("StandShooting");
    public static readonly int IS_PARRYING = Animator.StringToHash("Parrying"); // shooting is not possible 
    public static readonly int IS_EX_MOVING = Animator.StringToHash("ExMoving");
    public static readonly int IS_ON_GROUND = Animator.StringToHash("IsOnGround");
}

public static class BulletAnimID
{
  
    public static readonly int IS_LAUNCHED = Animator.StringToHash("IsLaunched");
    public static readonly int IS_Hit = Animator.StringToHash("IsHit");
}

public static class ObjectPoolNameID
{

    public static readonly string IS_LAUNCHED;
    public static readonly string IS_Hit;
}