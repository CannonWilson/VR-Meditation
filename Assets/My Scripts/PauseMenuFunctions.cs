using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

// Next up to keep track of a timer and ending the meditation, then keep track of player data, then create visual dashboard

public class PauseMenuFunctions : MonoBehaviour
{
    public GameObject pauseMenu;
    public Text pauseMenuDescription;
    public GameObject blackBackground;
    public GameObject sceneSettingsMenu;
    public GameObject soundSettingsMenu;
    public GameObject timerSettingsMenu;
    public GameObject continueButton;
    public GameObject Ocean;
    
    // The pause menu is being populated by the start button script. 
    // This script handles the functionality of all the buttons on the pause menu.
    
    // TODO:
    // Fix the actual objects so they dont look like shiz
    // Pause menu glitches when the meditation has finished

    void Update() {
        if (GlobalVariables.isMeditationFinished) {
            StartCoroutine(PopulatePauseMenu());
            disableLocation();
        }
    }

    public void sceneButtonPressed() {
        Debug.Log("SceneButtonPressed");
        if (continueButton.activeSelf) { // check to make sure that the continue button is already present to avoid any visual bugs
            depopulatePauseMenu();
            OnStartMenu.comingFromPauseMenu = true;
            StartCoroutine(PopulateSceneMenu());
        }
    }

    public void soundButtonPressed() {
        if (continueButton.activeSelf) {
            depopulatePauseMenu();
            OnStartMenu.comingFromPauseMenu = true;
            StartCoroutine(PopulateSoundMenu());
        }
    }

    public void timerButtonPressed() {
        if (continueButton.activeSelf) {
            depopulatePauseMenu();
            OnStartMenu.comingFromPauseMenu = true;
            StartCoroutine(PopulateTimerMenu());
        }
    }

    public void continueButtonPressed() {
        GlobalVariables.isMeditating = true;
        blackBackground.SetActive(false);
        depopulatePauseMenu();
        enableLocation();
    }

    void depopulatePauseMenu() {
        Debug.Log("depopulatePauseMenu reached");
        int thisMenuChildrenCount = pauseMenu.transform.childCount; // Get the number of pauseMenu Children
        Debug.Log("Childcount" + thisMenuChildrenCount);
        for(int i=0; i<thisMenuChildrenCount; i++) { // loop over all children
            pauseMenu.transform.GetChild(i).gameObject.SetActive(false);
            Debug.Log("disabled" + pauseMenu.transform.GetChild(i).gameObject.name);
        }
    }

    void enableLocation() {
        // TODO:
        // Update this function to work with other locations
        Ocean.SetActive(true);
    }

    void disableLocation() {
        // TODO:
        // Update this function to work with other locations
        Ocean.SetActive(false);
    }

    IEnumerator PopulatePauseMenu() {
        blackBackground.SetActive(true);
        pauseMenuDescription.text = "Congrats! You finished your meditation.";
        yield return new WaitForSeconds(1); // Wait for the black background to load

        int thisMenuChildrenCount = pauseMenu.transform.childCount; // Get the number of sceneSelectorMenu Children
        for(int i=0; i<thisMenuChildrenCount; i++) { // loop over all children
            pauseMenu.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
        }   
    }

    IEnumerator PopulateSceneMenu() {
        yield return new WaitForSeconds(1); // Wait for the black background to load

        int thisMenuChildrenCount = sceneSettingsMenu.transform.childCount; // Get the number of sceneSelectorMenu Children
        for(int i=0; i<thisMenuChildrenCount; i++) { // loop over all children
            sceneSettingsMenu.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator PopulateSoundMenu() {
        yield return new WaitForSeconds(1); // Wait for 1 second

        int thisMenuChildrenCount = soundSettingsMenu.transform.childCount; // Get the number of soundSettingsMenu Children
        for(int i=0; i<thisMenuChildrenCount; i++) { // loop over all children
            soundSettingsMenu.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator PopulateTimerMenu() {
        yield return new WaitForSeconds(1); // Wait for 1 second

        int thisMenuChildrenCount = timerSettingsMenu.transform.childCount;
        for (int i=0; i<thisMenuChildrenCount; i++) {
            timerSettingsMenu.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
        }
    }
}
