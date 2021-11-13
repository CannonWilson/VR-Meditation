using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static bool isMeditating = false;
    public static bool isMeditationFinished = false;
    public static int secondsSpentMeditatingThisSession = 0;
    public static string chosenScene = "Ocean";
    public static string[] availableScenes = {"Ocean", "Beach", "Forest", "Mountain"};
    public static string[] availableMusic = {"Default", "Rain", "Fire", "Forest"};
    public static string timeOfDay = "Day";
    public static float musicVolume = 0.5f;
    public static float ambientNoiseVolume = 0.5f;
    public static bool playBell = true;
    public static int timerLength = 5;
    public GameObject threeBells;

    float elapsed = 0f;

    void Update() { // update secondsSpentMeditating if player is meditating
        if (isMeditating) {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) {
            elapsed = elapsed % 1f;
            secondsSpentMeditatingThisSession += 1;
            Debug.Log("Updated Meditation Timer");
        }
        }

        if ((float)secondsSpentMeditatingThisSession == ((float)timerLength*60f)-13f) { // Play the bells 12 seconds before stopping if they are enabled
            if(playBell) {
                threeBells.GetComponent<AudioSource>().Play();
            }
        }

        if ((float)secondsSpentMeditatingThisSession/60f == (float)timerLength) {
            TriggerRecognizer.isListening = false;
            if (!isMeditationFinished) {
                isMeditationFinished = true;
                isMeditating = false;
            }
        }
    }
}
