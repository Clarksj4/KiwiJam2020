using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bop : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Raise()
    {
        animator.SetTrigger("Raise");
    }

    public void Depress()
    {
        animator.SetTrigger("Depress");
    }
}
