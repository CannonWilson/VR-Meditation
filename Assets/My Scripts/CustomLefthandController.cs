using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomLefthandController : MonoBehaviour
{
    private ActionBasedController controller; // get reference to left/right hand XR controller

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();

        bool selectIsPressed = controller.selectAction.action.ReadValue<bool>(); // use this command to tell if the select action is performed on the controller

        controller.selectAction.action.performed += Action_performed;
    }

    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        Debug.Log("Select was pressed");
    }

}
