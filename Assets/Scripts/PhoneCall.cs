using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCall : CalamityIncreaser
{
    [SerializeField]
    private float vibrateSensitivity = 0.1f;
    [SerializeField]
    private AudioSource vibrationSound;
    [SerializeField]
    private GameObject screen;
    private Text callerText;
    private Image callerImage;
    private new Rigidbody rigidbody;

    private Coroutine phoneCall;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        screen.SetActive(false);
        //phoneCall = StartCoroutine(DoCall());
    }

    public override void SetCalamity(float progress)
    {
        base.SetCalamity(progress);

        float volume = Mathf.Clamp(1 - progress, 0.1f, 1f);

        vibrationSound.volume = volume;

        if (progress > 0f && phoneCall == null)
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
        //cameraShake.ShakeCamera(2, 1f);
        float time = 0;
        while (time < 1f)
        {
            rigidbody.AddTorque(Random.insideUnitSphere * vibrateSensitivity, ForceMode.Impulse);

            time += Time.deltaTime;
            yield return null;
        }
    }
}
