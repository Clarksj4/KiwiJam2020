using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BopIt : CalamityIncreaser
{
    public event Action OnBop;

    [SerializeField]
    private Transform rayOrigin;
    [SerializeField]
    private bool keyboardInput;
    [SerializeField]
    private int mouseButton = 0;
    [SerializeField]
    private KeyCode keyboardButton = KeyCode.Space;
    private RotationInput rotationInput;

    private Bop[] bops;

    private void Awake()
    {
        rotationInput = GetComponent<RotationInput>();
        bops = GetComponentsInChildren<Bop>();
    }

    private void Start()
    {
        foreach (Bop bop in bops)
            bop.Depress();

        RaiseRandomBop();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = GetInputRay();
        if (Physics.Raycast(ray, out var hit, 1000f))
        {
            Transform transform = hit.transform;
            Bop bop = transform.parent.GetComponent<Bop>();
            SelectBop(bop);

            // If receiving input - BOP IT!
            if (IsInputHappening())
                DoABopIt(bop);
        }
    }

    public void SetEnabled(bool enabled)
    {
        this.enabled = enabled;
        rotationInput.enabled = enabled;
    }

    private void RaiseRandomBop(Bop exceptForThisOne = null)
    {
        Bop randomBop = bops[UnityEngine. Random.Range(0, bops.Length)];
        while (randomBop == exceptForThisOne)
            randomBop = bops[UnityEngine.Random.Range(0, bops.Length)];

        randomBop.Raise();
    }

    private void DoABopIt(Bop bop)
    {
        if (bop.GetCurrentState() == ButtonState.Up)
        {
            // Depress the tapped bop
            bop.Depress();

            // Raise a different random one
            RaiseRandomBop(exceptForThisOne: bop);

            OnBop?.Invoke();
        }
    }

    private Ray GetInputRay()
    {
        return new Ray(rayOrigin.position, transform.position - rayOrigin.position);
    }

    private bool IsInputHappening()
    {
        return keyboardInput ? Input.GetKeyUp(keyboardButton) : Input.GetMouseButtonUp(mouseButton);
    }

    private void SelectBop(Bop bop)
    {
        foreach (Bop child in bops)
            child.Deselect();
        bop.Select();
    }
}
