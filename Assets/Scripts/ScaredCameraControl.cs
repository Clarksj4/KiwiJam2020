using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredCameraControl : MonoBehaviour
{
    public event Action OnBeingAScaredyGuy;

    private AudioSource[] scaredAudio;
    private Animator scaredAnimator;

    private LookUpAnimationControl lookUpAnimationControl;
    
    private void Awake()
    {
        scaredAudio = GetComponents<AudioSource>();
        scaredAnimator = GetComponent<Animator>();
        lookUpAnimationControl = GetComponentInChildren<LookUpAnimationControl>();

        lookUpAnimationControl.OnLookUp += HandleOnLookUp;
    }

    private void HandleOnLookUp()
    {
        scaredAnimator.SetTrigger("Scared");
    }

    private void OnVocalizeScaredness()
    {
        scaredAudio[UnityEngine.Random.Range(0, scaredAudio.Length)].Play();
        OnBeingAScaredyGuy?.Invoke();
    }
}
