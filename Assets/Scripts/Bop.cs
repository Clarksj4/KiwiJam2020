using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonState
{
    Up, Down, Raise, Depress
}

public class Bop : MonoBehaviour
{
    [SerializeField]
    private Material litMaterial;
    [SerializeField]
    private Material unlitMaterial;
    [SerializeField]
    private Material outlineMaterial;
    [SerializeField]
    private Material nonOutlineMaterial;
    [SerializeField]
    private AudioSource clickAudio;
    [SerializeField]
    private AudioSource popAudio;
    [SerializeField]
    private MeshRenderer mechanismRenderer;
    [SerializeField]
    private MeshRenderer borderRenderer;
    private Animator animator;

    private int buttonUpHash = Animator.StringToHash("Base Layer.ButtonUp");
    private int buttonDownHash = Animator.StringToHash("Base Layer.ButtonDown");
    private int buttonRaiseHash = Animator.StringToHash("Base Layer.ButtonRaise");
    private int buttonDepressHash = Animator.StringToHash("Base Layer.ButtonDepress");

    private Dictionary<int, ButtonState> buttonStateCatalogue;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        buttonStateCatalogue = new Dictionary<int, ButtonState>()
        {
            { buttonUpHash, ButtonState.Up },
            { buttonDownHash, ButtonState.Down },
            { buttonRaiseHash, ButtonState.Raise },
            { buttonDepressHash, ButtonState.Depress }
        };
    }

    public void Select()
    {
        borderRenderer.material = outlineMaterial;
    }

    public void Deselect()
    {
        borderRenderer.material = nonOutlineMaterial;
    }

    public void Raise()
    {
        animator.SetTrigger("Raise");

        mechanismRenderer.material = unlitMaterial;
    }

    public void Depress()
    {
        animator.SetTrigger("Depress");

        mechanismRenderer.material = litMaterial;
    }

    public ButtonState GetCurrentState()
    {
        foreach (var kvp in buttonStateCatalogue)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == kvp.Key)
                return kvp.Value;
        }

        throw new InvalidOperationException("None of these states work you idiot!");
    }

    public void SetVolume(float volume)
    {
        popAudio.volume = volume;
        clickAudio.volume = volume;
    }

    private void Pop()
    {
        popAudio.Play();
    }

    private void Click()
    {
        clickAudio.Play();
    }
}
