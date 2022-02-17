using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    Idle,
    Walking,
    Running,
    Sprinting
}
public class AnimationController : MonoBehaviour {
    
    private PlayerState _currentState = PlayerState.Idle;
    [SerializeField] private Animator playerAnimator;
    private static readonly int Velocity = Animator.StringToHash("Velocity");


    public void SetAnimationState(PlayerState newState) {
        if (_currentState == newState) return;
        
        //test.duration = 0.8;
        
        //playerAnimator.Play(newState.ToString());

        _currentState = newState;
    }

    public void SetVelocity(float velocity) {
        playerAnimator.SetFloat(Velocity, velocity);
    }

}
