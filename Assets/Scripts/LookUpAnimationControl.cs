using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUpAnimationControl : MonoBehaviour
{
    public event Action OnLookUp;
    public event Action OnLookDown;

    private Animator lookUpAnimator;
    private InputCameraControl inputCameraControl;
    private bool up;

    private void Awake()
    {
        lookUpAnimator = GetComponent<Animator>();
        inputCameraControl = GetComponentInChildren<InputCameraControl>();
        inputCameraControl.SetEnabled(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            lookUpAnimator.SetTrigger("Up");
            
        else if (Input.GetKeyUp(KeyCode.Space))
            lookUpAnimator.SetTrigger("Down");
    }

    private void OnLookedUp()
    {
        up = true;
        inputCameraControl.SetEnabled(true);
        OnLookUp?.Invoke();
    }

    private void OnLookingDown()
    {
        up = false;
        inputCameraControl.SetEnabled(false);
        OnLookDown?.Invoke();
    }
}
