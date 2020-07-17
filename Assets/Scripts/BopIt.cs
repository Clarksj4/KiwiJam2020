using System.Collections;
using System.Collections.Generic;
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

    // All child colliders
    private Collider[] colliders;

    private void Awake()
    {
        colliders = GetComponentsInChildren<Collider>();
        Cursor.visible = false;
    }

    private void Start()
    {
        //ExclusivelyShowColldier(null);
    }

    private void OnDrawGizmos()
    {
        if (rayOrigin != null)
        {
            Ray ray = new Ray(rayOrigin.position, transform.position - rayOrigin.position);
            Gizmos.DrawRay(ray.origin, ray.direction * 1000f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If receiving input - BOP IT!
        if (IsInputHappening())
        {
            Ray ray = new Ray(rayOrigin.position, transform.position - rayOrigin.position);
            if (Physics.Raycast(ray, out var hit, 1000f))
            {
                Transform transform = hit.transform;
                Animator buttonAnimator = transform.parent.GetComponentInChildren<Animator>();
                buttonAnimator.SetTrigger("Depress");
            }
        }
    }

    private bool IsInputHappening()
    {
        return keyboardInput ? Input.GetKeyUp(keyboardButton) : Input.GetMouseButtonUp(mouseButton);
    }

    private void ExclusivelyShowColldier(Collider collider)
    {
        // Turn off all colliders except the given one
        foreach(Collider child in colliders)
        {
            bool active = child == collider;
            child.GetComponent<Renderer>().enabled = active;
        }
    }
}
