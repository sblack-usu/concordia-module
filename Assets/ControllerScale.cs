using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ControllerScale : MonoBehaviour {

    #region Private Variables
    private ControllerConnectionHandler _controllerConnectionHandler;

    #endregion

    public SolarSystem solarSystem;

    #region Unity Methods
    /// <summary>
    /// Initialize variables, callbacks and check null references.
    /// </summary>
    void Start()
    {
        _controllerConnectionHandler = GetComponent<ControllerConnectionHandler>();
        MLInput.OnControllerTouchpadGestureContinue += HandleControllerTouchpadGestureContinue;
        MLInput.OnControllerButtonDown += ScaleUp;
        MLInput.OnControllerButtonUp += ScaleDown;
    }

    private void OnDestroy()
    {
        MLInput.OnControllerTouchpadGestureContinue -= HandleControllerTouchpadGestureContinue;
        MLInput.OnControllerButtonDown -= ScaleUp;
        MLInput.OnControllerButtonUp -= ScaleDown;
    }
    #endregion

    private void ScaleUp(byte controllerId, MLInputControllerButton button)
    {
        solarSystem.scale = .05f;
    }

    private void ScaleDown(byte controllerId, MLInputControllerButton button)
    {
        solarSystem.scale = .001f;
    }
    private void HandleControllerTouchpadGestureContinue(byte controllerId, MLInputControllerTouchpadGesture gesture)
    {
        MLInputController controller = _controllerConnectionHandler.ConnectedController;
        if (gesture.Direction == MLInputControllerTouchpadGestureDirection.Up)
        {
            solarSystem.scale += gesture.Speed * 1000f;
        }
        else if(gesture.Direction == MLInputControllerTouchpadGestureDirection.Up)
        {
            solarSystem.scale -= gesture.Speed * 1000f;
        }
    }
}
