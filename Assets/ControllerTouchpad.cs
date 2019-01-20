/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ControllerTouchpad : MonoBehaviour {

    public Rigidbody ring;
    public Rigidbody rod;

    public float ringforceMultiplier = 1f;
    public float rodforceMultiplier = 1f;
    Vector3 ringRotationAxis = new Vector3(0, 0, 1);

    //Vector3 rodRotationAxis = new Vector3(0, 1, 0);
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
    }

    private void OnDestroy()
    {
        MLInput.OnControllerTouchpadGestureContinue -= HandleControllerTouchpadGestureContinue;
    }
    #endregion
    private void HandleControllerTouchpadGestureContinue(byte controllerId, MLInputControllerTouchpadGesture gesture)
    {
        MLInputController controller = _controllerConnectionHandler.ConnectedController;
        Vector3? pos = gesture.PosAndForce;
        Vector3 vpos = pos.Value;
        if(vpos.x > 0)
        {
            ring.AddTorque(ringRotationAxis * -ringforceMultiplier);
        }
        else if (vpos.x < 0)
        {
            ring.AddTorque(ringRotationAxis * ringforceMultiplier);
        }
        ring.AddTorque(ringRotationAxis * ringforceMultiplier);
    }
}
*/