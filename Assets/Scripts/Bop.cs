using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonState
{
    Up, Down, Raise, Depress
}

public class Bop : MonoBehaviour
{
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

    public void Raise()
    {
        animator.SetTrigger("Raise");
    }

    public void Depress()
    {
        animator.SetTrigger("Depress");
    }

    public ButtonState GetCurrentState()
    {
        foreach (var kvp in buttonStateCatalogue)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == kvp.Key)
                return kvp.Value;
        }

        throw new System.InvalidOperationException("None of these states work you idiot!");
    }
}
