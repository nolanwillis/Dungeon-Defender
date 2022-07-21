using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateSound : StateMachineBehaviour
{
    // References
    private AudioManager audioManager;

    // Name of audio clip to be played
    public string clipName;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play(clipName);
    }
}
