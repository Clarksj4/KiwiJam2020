using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationInput : MonoBehaviour
{
    [SerializeField]
    private bool keyboardInput = true;
    [SerializeField]
    private Space relativeTo = Space.World;
    [SerializeField]
    private float sensitivity = 2f;

    private bool isFirstFrame = true;


    private Vector2 GetMouseInput()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    private Vector2 GetKeyboardInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    // Update is called once per frame
    void Update()
    {
        // Don't do SHIT on the first frame because it results in some yucky jumps
        if (!isFirstFrame)
        {
            Vector2 input = keyboardInput ? GetKeyboardInput() : GetMouseInput();
            Vector2 rotation = input * sensitivity * Time.deltaTime;

            transform.Rotate(rotation.y, -rotation.x, 0, relativeTo);
        }

        isFirstFrame = false;
    }
}
