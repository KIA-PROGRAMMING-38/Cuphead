﻿using UnityEngine;

public static class CupheadAnimID
{
    public static readonly int IS_RUNNING = Animator.StringToHash("Running");
    public static readonly int IS_JUMPING = Animator.StringToHash("Jumping");
    public static readonly int IS_JUMPING_AND_SHOOTING = Animator.StringToHash("JumpingAndShooting");
    public static readonly int IS_DUCKING = Animator.StringToHash("Ducking");
    public static readonly int IS_DUCKING_AND_SHOOOTING = Animator.StringToHash("DuckingAndShooting");
    public static readonly int IS_SHOOTING = Animator.StringToHash("Shooting");
    public static readonly int IS_PARRYING = Animator.StringToHash("Parrying"); // shooting is not possible 
    public static readonly int IS_EX_MOVING = Animator.StringToHash("ExMoving");

}