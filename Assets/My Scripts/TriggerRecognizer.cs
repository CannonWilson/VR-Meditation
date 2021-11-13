using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TriggerRecognizer : MonoBehaviour
{
    public XRNode inputSource;
    public UnityEngine.XR.Interaction.Toolkit.InputHelpers.Button inputButton;
    public float inputThreshold = 0.1f;
    public GameObject blackBackground;
    public GameObject pauseMenu;
    public GameObject locationFolder;
    public static bool isListening = false;

    void Update()
    {
        if (isListening) {
            UnityEngine.XR.Interaction.Toolkit.InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);

            if (isPressed) { // Check if a trigger is pressed while meditating
                // for testing
                StartCoroutine(PopulatePauseMenuIfAppropriate());
            }

            if (Input.GetMouseButtonDown(0)) { // Temporary use of keyboard input for debugging, set to normal left click.
                StartCoroutine(PopulatePauseMenuIfAppropriate());
            }
        }
    }

    IEnumerator PopulatePauseMenuIfAppropriate() {
        if (GlobalVariables.isMeditating == true) {
            isListening = false;
            GlobalVariables.isMeditating = false;
            disableLocation();
            blackBackground.SetActive(true);
            yield return new WaitForSeconds(1); // Wait for the black background to load

            int thisMenuChildrenCount = pauseMenu.transform.childCount; // Get the number of pauseMenu Children
            for(int i=0; i<thisMenuChildrenCount; i++) { // loop over all children
                pauseMenu.transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.4f);
            }
        }
    }

    void disableLocation() {
        int locations = locationFolder.transform.childCount;
        for(int i=0; i<locations; i++) {
            locationFolder.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
