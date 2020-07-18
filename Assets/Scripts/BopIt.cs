using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BopIt : MonoBehaviour
{
    [SerializeField]
    private Transform rayOrigin;
    [SerializeField]
    private bool keyboardInput;
    [SerializeField]
    private int mouseButton = 0;
    [SerializeField]
    private KeyCode keyboardButton = KeyCode.Space;

    private Bop[] bops;

    private void Awake()
    {
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
        // If receiving input - BOP IT!
        if (IsInputHappening())
        {
            Ray ray = GetInputRay();
            if (Physics.Raycast(ray, out var hit, 1000f))
            {
                Transform transform = hit.transform;
                Bop bop = transform.parent.GetComponent<Bop>();
                DoABopIt(bop);
            }
        }
    }

    private void RaiseRandomBop(Bop exceptForThisOne = null)
    {
        Bop randomBop = bops[Random.Range(0, bops.Length)];
        while (randomBop == exceptForThisOne)
            randomBop = bops[Random.Range(0, bops.Length)];

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

    // TODO: ALL bops except for a random one start down
    // TODO: When one bop is pressed - a different random one is raised
}
