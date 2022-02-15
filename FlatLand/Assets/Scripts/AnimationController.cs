using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationState {
    Idle,
    Walking,
    Running,
    Sprinting
}
public class AnimationController : MonoBehaviour {
    private const string PLAYER_ANIMATIONS_LAYER = "PlayerAnimations";
    
    private AnimationState _currentState = AnimationState.Idle;
    [SerializeField] private Animator playerAnimator;
    
    
    public void SetAnimationState(AnimationState newState) {
        if (_currentState == newState) return;

        playerAnimator.Play(newState.ToString(), playerAnimator.GetLayerIndex(PLAYER_ANIMATIONS_LAYER));

        _currentState = newState;
    }

}
