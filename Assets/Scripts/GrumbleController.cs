using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrumbleController : MonoBehaviour
{
    [SerializeField]
    private float minTimeBetweenGrumbles = 10f;
    [SerializeField]
    private float maxTimeBetweenGrumbles = 60f;
    [SerializeField]
    private ScaredCameraControl scaredCameraController;
    [SerializeField]
    private LookUpAnimationControl lookUpAnimationController;
    [SerializeField]
    private BopIt bopIt;
    [SerializeField]
    private AudioSource[] grumbles;
    [SerializeField]
    private AudioSource[] thatOnes;
    private AudioSource currentPlayer;
    private Coroutine audioCoroutine;
    private bool lookingDown = true;

    private void Awake()
    {
        scaredCameraController.OnBeingAScaredyGuy += HandleOnBeingAScaredGuy;
        lookUpAnimationController.OnLookDown += HandleOnLookDown;
        bopIt.OnBop += HandleOnBop;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, minTimeBetweenGrumbles));

        while (true)
        {
            if (lookingDown)
                Grumble();

            yield return new WaitForSeconds(UnityEngine.Random.Range(minTimeBetweenGrumbles, maxTimeBetweenGrumbles));
        }
    }

    private void ShutTheFuckUp()
    {
        if (audioCoroutine != null)
            StopCoroutine(audioCoroutine);

        if (currentPlayer != null)
            currentPlayer.Stop();

        currentPlayer = null;
    }

    // One scoop of 'That One'
    private void ExclaimThatOne()
    {
        if (currentPlayer == null)
            audioCoroutine = StartCoroutine(PlaySound(thatOnes[UnityEngine.Random.Range(0, thatOnes.Length)]));
    }

    // A single unit of grumble
    private void Grumble()
    {
        if (currentPlayer == null)
            audioCoroutine = StartCoroutine(PlaySound(grumbles[UnityEngine.Random.Range(0, grumbles.Length)]));
    }

    private IEnumerator PlaySound(AudioSource source)
    {
        currentPlayer = source;
        source.Play();

        while (currentPlayer != null &&
               currentPlayer.isPlaying)
            yield return null;

        currentPlayer = null;
    }


    //
    // Event handling
    //

    private void HandleOnLookDown()
    {
        lookingDown = true;
    }

    private void HandleOnBeingAScaredGuy()
    {
        // Too scared for grumbles!
        ShutTheFuckUp();
        lookingDown = false;
    }

    private void HandleOnBop()
    {
        if (currentPlayer == null && UnityEngine.Random.Range(0f, 1f) > 0.4f)
            ExclaimThatOne();
    }
}
