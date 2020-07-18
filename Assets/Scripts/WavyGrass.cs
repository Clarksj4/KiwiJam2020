using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavyGrass : MonoBehaviour
{
    Vector3 startRotation;
    float phase;

    void Start()
    {
        startRotation = transform.localRotation.eulerAngles;
        phase = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        float angle = -10 + Mathf.Sin((phase + Time.time) * 30f) * 5;
        transform.localRotation = Quaternion.Euler(startRotation.x + angle, startRotation.y, startRotation.z);
    }
}
