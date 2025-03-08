using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class RocketController : MonoBehaviour
{
    public GameObject rocketL; // Rocket-L GameObject
    public GameObject rocketR; // Rocket-R GameObject

    public float forceAmount = 10f; // The force to be applied

    private XRNode leftControllerNode = XRNode.LeftHand;
    private XRNode rightControllerNode = XRNode.RightHand;

    public InputDevice leftController;
    public InputDevice rightController;

    void Start()
    {
        // Get the input devices for the left and right controllers
        leftController = InputDevices.GetDeviceAtXRNode(leftControllerNode);
        rightController = InputDevices.GetDeviceAtXRNode(rightControllerNode);

        // Debugging: Check if the controllers are properly detected
        if (!leftController.isValid)
            Debug.LogError("Left controller is not detected.");
        if (!rightController.isValid)
            Debug.LogError("Right controller is not detected.");
    }

    void Update()
    {
        // Check if the left controller button is pressed
        if (leftController.isValid && leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftButtonPressed) && leftButtonPressed)
        {
            ApplyForceToRocket(rocketL);
        }

        // Check if the right controller button is pressed
        if (rightController.isValid && rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightButtonPressed) && rightButtonPressed)
        {
            ApplyForceToRocket(rocketR);
        }
    }

    private void ApplyForceToRocket(GameObject rocket)
    {
        if (rocket != null)
        {
            Rigidbody rb = rocket.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply an upwards force on the rocket
                rb.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);
            }
        }
    }
}
