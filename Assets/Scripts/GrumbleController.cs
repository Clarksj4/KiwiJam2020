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

    private void Awake()
    {
        scaredCameraController.OnBeingAScaredyGuy += HandleOnBeingAScaredGuy;
        lookUpAnimationController.OnLookDown += HandleOnLookDown;
        bopIt.OnBop += HandleOnBop;
    }



    private IEnumerator Start()
    {
        // Wait a little bit before grumbling
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, minTimeBetweenGrumbles));

        StartCoroutine(LoopyGrumbles());
    }

    private void StopGrumbles()
    {
        StopAllCoroutines();

        if (currentPlayer != null)
            currentPlayer.Stop();
        currentPlayer = null;
    }

    private void ExclaimThatOne()
    {
        currentPlayer = thatOnes[UnityEngine.Random.Range(0, thatOnes.Length)];
        currentPlayer.Play();
    }

    private void Grumble()
    {
        currentPlayer = grumbles[UnityEngine.Random.Range(0, grumbles.Length)];
        currentPlayer.Play();
    }

    private IEnumerator DoExclaimThatOne()
    {
        ExclaimThatOne();
        yield return StartCoroutine(WaitForPlayerToFinish());
        StartCoroutine(LoopyGrumbles());
    }

    private IEnumerator WaitForPlayerToFinish()
    {
        while (currentPlayer != null && currentPlayer.isPlaying)
            yield return null;

        currentPlayer = null;
    }

    private IEnumerator LoopyGrumbles()
    {
        while (true)
        {
            Grumble();

            yield return StartCoroutine(WaitForPlayerToFinish());

            yield return new WaitForSeconds(UnityEngine.Random.Range(minTimeBetweenGrumbles, maxTimeBetweenGrumbles));
        }
    }

    private void HandleOnLookDown()
    {
        // Start grumbling again
        StopGrumbles();
        StartCoroutine(LoopyGrumbles());
    }

    private void HandleOnBeingAScaredGuy()
    {
        // Too scared for grumbles!
        StopGrumbles();
    }

    private void HandleOnBop()
    {
        if (currentPlayer == null && UnityEngine.Random.Range(0f, 1f) > 0.5f)
        {
            StopGrumbles();
            StartCoroutine(DoExclaimThatOne());
        }
    }
}
