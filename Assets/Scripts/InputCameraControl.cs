using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCameraControl : MonoBehaviour
{
    [SerializeField]
    private float maxHorizontalRotation = 45;
    [SerializeField]
    private float maxVerticalRotation = 25;
    [SerializeField]
    private float sensitivity = 50;

    private bool isEnabled;
    private Vector3 _previousMousePosition;

    private float x;
    private float y;

    public void SetEnabled(bool enabled)
    {
        if (isEnabled != enabled)
        {
            isEnabled = enabled;

            StopAllCoroutines();
            if (!isEnabled)
                StartCoroutine(DoResetTransform(0.5f));
        }
    }

    private void Update()
    {
        if (isEnabled)
        {
            Vector2 rawInputDelta = GetMouseInput();
            Vector2 smoothedInputDelta = rawInputDelta * sensitivity * Time.deltaTime;

            x = Mathf.Clamp(x + -smoothedInputDelta.y, -maxVerticalRotation, maxVerticalRotation);
            y = Mathf.Clamp(y + smoothedInputDelta.x, -maxHorizontalRotation, maxHorizontalRotation);

            transform.localRotation = Quaternion.Euler(x, y, 0);
        }

        _previousMousePosition = Input.mousePosition;
    }

    private Vector3 GetMouseInput()
    {
        return Input.mousePosition - _previousMousePosition;
    }

    private IEnumerator DoResetTransform(float duration)
    {
        float startX = x;
        float startY = y;

        float time = 0;
        while (time < duration)
        {
            float t = time / duration;
            float currentX = Mathf.Lerp(startX, 0, t);
            float currentY = Mathf.Lerp(startY, 0, t);

            transform.localRotation = Quaternion.Euler(currentX, currentY, 0);

            time += Time.deltaTime;
            yield return null;
        }

        x = 0;
        y = 0;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
