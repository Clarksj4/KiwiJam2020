using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bopper : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 2f;

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
        Vector2 input = GetKeyboardInput();
        Vector2 rotation = input * sensitivity * Time.deltaTime;

        transform.Rotate(rotation.y, -rotation.x, 0, Space.World);
    }
}
