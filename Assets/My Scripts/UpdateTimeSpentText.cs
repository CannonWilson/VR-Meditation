using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTimeSpentText : MonoBehaviour
{
    public Text timeSpentText;
    float minutesCalc;

    void OnEnable()
    {
        minutesCalc = GlobalVariables.secondsSpentMeditatingThisSession/60;
        timeSpentText.text = "You have meditated for " + string.Format("{0:0}", minutesCalc) + " out of " + GlobalVariables.timerLength + " minutes.";
    }

    void Update() {
        if (minutesCalc != GlobalVariables.secondsSpentMeditatingThisSession/60) { // check to see if minutesCalc is no longer accurate. If so, update the text message.
            minutesCalc = GlobalVariables.secondsSpentMeditatingThisSession/60;
            timeSpentText.text = "You have meditated for " + string.Format("{0:0}", minutesCalc) + " out of " + GlobalVariables.timerLength + " minutes.";
        }
    }
}
