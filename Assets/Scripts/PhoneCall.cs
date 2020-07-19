using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCall : CalamityIncreaser
{
    private CameraShake cameraShake;
    private GameObject screen;
    private Text callerText;
    private Image callerImage;

    private Coroutine phoneCall;

    private void Awake()
    {
        cameraShake = GetComponentInChildren<CameraShake>();
        phoneCall = StartCoroutine(DoCall());
    }

    public override void SetCalamity(float progress)
    {
        base.SetCalamity(progress);

        //if (progress > 0.125f && phoneCall == null)
        //    phoneCall = StartCoroutine(DoCall());
    }

    private IEnumerator DoCall()
    {
        while (true)
        {
            cameraShake.ShakeCamera(2, 0.5f);
            yield return new WaitForSeconds(1f);
            cameraShake.ShakeCamera(2, 0.5f);

            yield return new WaitForSeconds(2.5f);
        }
    }
}
