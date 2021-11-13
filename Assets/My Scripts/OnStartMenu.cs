using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class OnStartMenu : MonoBehaviour
{
    public GameObject blackBackground;
    public Image sceneImage;
    public Text sceneText;
    public GameObject pauseMenu;
    public GameObject sceneSelectorMenu;
    public GameObject soundSelectorMenu;
    public GameObject timerSettingsMenu;
    public GameObject musicChoicesParent;
    public GameObject ambientNoise;
    public Text musicChosenText;
    public Slider musicSlider;
    public Slider ambientNoiseSlider;
    public GameObject bellStrikes;
    public Text timeChosenText;
    public GameObject customTimerSliderAndText;
    public Slider customTimeSlider;
    public Text customTimeInMinutes;
    int currentSceneIndex = 0;
    int currentMusicIndex = 0;
    int numberOfScenes = GlobalVariables.availableScenes.Length;
    int numberOfMusicChoices = GlobalVariables.availableMusic.Length;
    bool customTimerEnabled = false;
    public GameObject Ocean;
   
    public static bool comingFromPauseMenu = false;

    //TODO:
    //Combine all of these left/right arrow pressed functions into just one/two functions
    //Combine PopulateMenu functions
    // Add time of day selector on sceneselection screen
    // Make back button for each screen
    // Make arms disappear when not moving

    public void rightSceneSettingsArrowPressed() {
        currentSceneIndex = (currentSceneIndex+1)%numberOfScenes; // wrap around if scene index becomes >= number of scenes
        UpdateSceneImageAndText();
    }

    public void leftSceneSettingsArrowPressed() {
       currentSceneIndex = (currentSceneIndex-1)%numberOfScenes; // wrap around if scene index becomes < 0
       if (currentSceneIndex < 0) {
           currentSceneIndex = numberOfScenes-1;
       }
       UpdateSceneImageAndText();
    }

    void UpdateSceneImageAndText() {
        // TODO: 
        // Update for real images intead of colors
        // Use for loop instead of if statements
        if (currentSceneIndex == 0) { // Ocean is currently selected
            sceneImage.GetComponent<Image>().color = new Color(0f, 0f, 1f); // blue
            GlobalVariables.chosenScene = GlobalVariables.availableScenes[currentSceneIndex];
            sceneText.GetComponent<Text>().text = GlobalVariables.chosenScene;
        }
        if (currentSceneIndex == 1) { // Beach is currently selected
            sceneImage.GetComponent<Image>().color = new Color(1f, 1f, 0f); // yellow
            GlobalVariables.chosenScene = GlobalVariables.availableScenes[currentSceneIndex];
            sceneText.GetComponent<Text>().text = GlobalVariables.chosenScene;
        }
        if (currentSceneIndex == 2) { // Forest is currently selected
            sceneImage.GetComponent<Image>().color = new Color(0f, 1f, 0f); // green
            GlobalVariables.chosenScene = GlobalVariables.availableScenes[currentSceneIndex];
            sceneText.GetComponent<Text>().text = GlobalVariables.chosenScene;
        }
        if (currentSceneIndex == 3) { // Mountain is currently selected
            sceneImage.GetComponent<Image>().color = new Color(0f, 1f, 1f); // blue-green
            GlobalVariables.chosenScene = GlobalVariables.availableScenes[currentSceneIndex];
            sceneText.GetComponent<Text>().text = GlobalVariables.chosenScene;
        }
    }


    public void rightSoundSettingsArrowPressed() {
        currentMusicIndex = (currentMusicIndex+1)%numberOfMusicChoices; // Wrap around if the index becomes >= number of music choices
        UpdateMusic();
    }

    public void leftSoundSettingsArrowPressed() {
        currentMusicIndex = (currentMusicIndex-1)%numberOfMusicChoices;
        if (currentMusicIndex < 0) {
            currentMusicIndex = numberOfMusicChoices - 1; // Wrap around if the index becomes < number of music choices
        }
        UpdateMusic();
    }

    public void startingBellCheckboxPressed() { 
        if (GlobalVariables.playBell == true) {
            GlobalVariables.playBell = false;
        }
        else {
            GlobalVariables.playBell = true;
        }
    }

    public void leftTimerSettingsArrowPressed() {
        if(!customTimerEnabled) {
            GlobalVariables.timerLength -= 5;
            if (GlobalVariables.timerLength == 0) {
                GlobalVariables.timerLength = 120;
            }
        timeChosenText.text = "" + GlobalVariables.timerLength;
        }
    }

    public void rightTimerSettingsArrowPressed() {
        if(!customTimerEnabled) {
            GlobalVariables.timerLength += 5;
            if (GlobalVariables.timerLength == 125) {
                GlobalVariables.timerLength = 5;
            }
            timeChosenText.text = "" + GlobalVariables.timerLength;
        }
    }

    public void customTimerCheckboxPressed() {

        if (customTimerEnabled == false) { //custom timer wasn't previously enabled
            customTimerEnabled = true;
            customTimerSliderAndText.SetActive(true);
            timeChosenText.text = "Custom";
        }
        else { //custom timer was previously enabled
            customTimerEnabled = false;
            customTimerSliderAndText.SetActive(false);
            //Reset the normal timer back to default values
            timeChosenText.text = "5";
            GlobalVariables.timerLength = 5;
        }
    }

    public void SceneSelectorContinueButtonPressed() {
        int thisMenuChildrenCount = sceneSelectorMenu.transform.childCount; // Get the number of scene menu Children
        for(int i=0; i<thisMenuChildrenCount; i++) { // loop over all children
            sceneSelectorMenu.transform.GetChild(i).gameObject.SetActive(false);
        }
        if (comingFromPauseMenu) {
            StartCoroutine(fromPauseMenu());
        }
        else {
            StartCoroutine(PopulateSoundSettingsMenu());
        } 
    }

    public void SoundSettingsContinueButtonPressed() {
        int thisMenuChildrenCount = soundSelectorMenu.transform.childCount; // Get the number of sound menu Children
        for (int i=0; i<thisMenuChildrenCount; i++) { // loop over all children
            soundSelectorMenu.transform.GetChild(i).gameObject.SetActive(false);
        };
        // TODO:
        // Fix so that is goes back to pause menu
        if (comingFromPauseMenu) {
            StartCoroutine(fromPauseMenu());
        }
        else {
            StartCoroutine(PopulateTimerSettingsMenu());
        }
    }

    public void TimerSettingsContinueButtonPressed() {
        int thisMenuChildrenCount = timerSettingsMenu.transform.childCount; // Get the number of timer menu Children
        for(int i=0; i<thisMenuChildrenCount; i++) { // loop over all children
            timerSettingsMenu.transform.GetChild(i).gameObject.SetActive(false);
        }
        blackBackground.SetActive(false);
        if (GlobalVariables.playBell) {
            bellStrikes.GetComponent<AudioSource>().Play();
        }
        CreateScene();
        StartCoroutine(StartedMeditating());
    }

    IEnumerator fromPauseMenu() {
        comingFromPauseMenu = false;
        yield return new WaitForSeconds(1); // Wait for 1 second

        int thisMenuChildrenCount = pauseMenu.transform.childCount; // Get the number of pauseMenu Children
        for(int i=0; i<thisMenuChildrenCount; i++) { // loop over all children
            pauseMenu.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
        };
    }

    void UpdateMusic() {
        // TODO: 
        // Update to use for loop instead of so many if statements
        // Use names of muiscChoicesParent children gameObjects instead of global variable storing names
        if (currentMusicIndex == 0) { // Default is currently selected
            musicChosenText.GetComponent<Text>().text = GlobalVariables.availableMusic[0]; //Default
            UpdateMusicPlaying(0);
        }
        if (currentMusicIndex == 1) { // Rain is currently selected
            musicChosenText.GetComponent<Text>().text = GlobalVariables.availableMusic[1]; //Rain
            UpdateMusicPlaying(1);
        }
        if (currentMusicIndex == 2) { // Jazz is currently selected
            musicChosenText.GetComponent<Text>().text = GlobalVariables.availableMusic[2]; //Fire
            UpdateMusicPlaying(2);
        }
        if (currentMusicIndex == 3) { // Vocal is currently selected
            musicChosenText.GetComponent<Text>().text = GlobalVariables.availableMusic[3];
            UpdateMusicPlaying(3);
        } 
    }

    void UpdateMusicPlaying(int index) {
        var allChildren = musicChoicesParent.GetComponentsInChildren<Transform>(true);
        // Have to do some funky stuff here with the indices because GetComponentsInChildren includes the parent object -_-
        for (int i=1; i<=numberOfMusicChoices; i++) {
            if (i != index+1) {
                allChildren[i].gameObject.SetActive(false);
            }
            else {
                allChildren[i].gameObject.SetActive(true);
            }
        }
    }


    void Update() {
        if (musicSlider.value != GlobalVariables.musicVolume) { //If the music volume has changed
            GlobalVariables.musicVolume = musicSlider.value;
            // Change the volume for ALL of the music 
            var allMusicChoices = musicChoicesParent.GetComponentsInChildren<Transform>(true);
            for (int i=1; i<=numberOfMusicChoices; i++) {
                allMusicChoices[i].gameObject.GetComponent<AudioSource>().volume = GlobalVariables.musicVolume;
            }
        }
        if (ambientNoiseSlider.value != GlobalVariables.ambientNoiseVolume) { //If ambient noise volume has changed
            GlobalVariables.ambientNoiseVolume = ambientNoiseSlider.value;
            ambientNoise.GetComponent<AudioSource>().volume = GlobalVariables.ambientNoiseVolume;
        }
        if (customTimerEnabled) { //If customTime is enabled, set Global Time variable if it has changed and update text
            if (customTimeSlider.value != GlobalVariables.timerLength) { 
                GlobalVariables.timerLength = (int)customTimeSlider.value;
            }
            customTimeInMinutes.text = "" + GlobalVariables.timerLength;
        }
    }

    void CreateScene() {
        //TODO: 
        // Enable scenes based on the user's choices.
        // Make sure to play ding dong bell

        // Debug.Log("chosenScene" + GlobalVariables.chosenScene);
        // sceneToLoad = GameObject.Find("/Locations/" + GlobalVariables.chosenScene);
        // sceneToLoad.SetActive(true);
        Ocean.SetActive(true);
    }

    void Start() {
        numberOfScenes = GlobalVariables.availableScenes.Length;
        StartCoroutine(PopulateSceneSelectorMenu());
    }

    IEnumerator PopulateSceneSelectorMenu() {
        yield return new WaitForSeconds(1); // Wait for the black background to load

        int thisMenuChildrenCount = sceneSelectorMenu.transform.childCount; // Get the number of sceneSelectorMenu Children
        for(int i=0; i<thisMenuChildrenCount; i++) { // loop over all children
            sceneSelectorMenu.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator PopulateSoundSettingsMenu() {
        yield return new WaitForSeconds(1); // Wait for 1 second

        int thisMenuChildrenCount = soundSelectorMenu.transform.childCount; // Get the number of soundSettingsMenu Children
        for(int i=0; i<thisMenuChildrenCount; i++) { // loop over all children
            soundSelectorMenu.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator PopulateTimerSettingsMenu() {
        yield return new WaitForSeconds(1); // Wait for 1 second

        int thisMenuChildrenCount = timerSettingsMenu.transform.childCount;
        for (int i=0; i<thisMenuChildrenCount; i++) {
            timerSettingsMenu.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator StartedMeditating() {
        yield return new WaitForSeconds(0.1f);
        GlobalVariables.isMeditating = true;
        GlobalVariables.secondsSpentMeditatingThisSession = 0;
        TriggerRecognizer.isListening = true;
    }

}
