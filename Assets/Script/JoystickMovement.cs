using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class JoystickMovement : MonoBehaviour
{
    // camera movement speed
    public float speedMultiplier = 1.0f;
    
    private Transform cameraRig;
    private Transform centerEyeAnchor;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraRig = GetComponent<Transform>();
        centerEyeAnchor = cameraRig.Find("TrackingSpace/CenterEyeAnchor");   
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 thumbstickInput = GetThumbstickInput();
        if (thumbstickInput.magnitude > 0.0f)
        {
            MoveCamera(thumbstickInput);
        }
    }

    private Vector2 GetThumbstickInput()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics characteristics = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.HeldInHand;
        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);

        foreach (InputDevice device in devices)
        {
            if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxis))
            {
                return primary2DAxis;
            }
        }

        return Vector2.zero;
    }

    private void MoveCamera(Vector2 thumbstickInput)
    {
        float speed = thumbstickInput.magnitude * speedMultiplier;
        Vector3 movementDirection = new Vector3(thumbstickInput.x, 0, thumbstickInput.y);
        Quaternion relativeRotation = Quaternion.Euler(0, centerEyeAnchor.eulerAngles.y, 0);
        Vector3 worldMovementDirection = relativeRotation * movementDirection;
        cameraRig.position += worldMovementDirection * speed * Time.deltaTime;
    }
}


