using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateSound : StateMachineBehaviour
{
    private AudioManager audioManager;
    public string clipName;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play(clipName);
    }
}
