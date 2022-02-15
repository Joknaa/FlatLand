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
    private const string PLAYER_ANIMATIONS_LAYER = "PlayerAnimations";
    
    private PlayerState _currentState = PlayerState.Idle;
    [SerializeField] private Animator playerAnimator;
    
    
    public void SetAnimationState(PlayerState newState) {
        if (_currentState == newState) return;

        playerAnimator.Play(newState.ToString(), playerAnimator.GetLayerIndex(PLAYER_ANIMATIONS_LAYER));

        _currentState = newState;
    }

}
