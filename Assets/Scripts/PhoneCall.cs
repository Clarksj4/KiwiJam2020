using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCall : CalamityIncreaser
{
    private CameraShake cameraShake;
    [SerializeField]
    private AudioSource vibrationSound;
    [SerializeField]
    private GameObject screen;
    private Text callerText;
    private Image callerImage;

    private Coroutine phoneCall;

    private void Awake()
    {
        cameraShake = GetComponentInChildren<CameraShake>();
        screen.SetActive(false);
        //phoneCall = StartCoroutine(DoCall());
    }

    public override void SetCalamity(float progress)
    {
        base.SetCalamity(progress);

        float volume = Mathf.Clamp(1 - progress, 0.1f, 1f);

        vibrationSound.volume = volume;

        if (progress > 0.125f && phoneCall == null)
            phoneCall = StartCoroutine(DoCall());
    }

    private IEnumerator DoCall()
    {
        screen.SetActive(true);
        while (true)
        {
            // Burst -> short pause -> burst -> long pause
            yield return StartCoroutine(Vibrate());
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(Vibrate());
            yield return new WaitForSeconds(2.5f);
        }
    }

    private IEnumerator Vibrate()
    {
        // sound is 1s long with 0.05 second delay at start
        vibrationSound.Play();
        yield return new WaitForSeconds(0.05f);
        cameraShake.ShakeCamera(2, 1f);
        yield return new WaitForSeconds(1f);
    }
}
