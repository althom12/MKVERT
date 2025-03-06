using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetpackController : MonoBehaviour
{
    [Header("Jetpack Settings")]
    public float jetpackForce = 10f;          // 喷气背包推力
    public float maxJetpackFuel = 5f;         // 最大燃料
    public float fuelRegenRate = 1f;          // 燃料恢复速度
    public Transform cameraTransform;         // 头部方向参考（一般是VR Main Camera）

    [Header("Input Action")]
    public InputActionProperty jetpackButton;  // XR按钮映射（XRI里的Activate或自定义）

    private Rigidbody playerRigidbody;
    private float currentFuel;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.useGravity = false;   // 关闭默认重力
        currentFuel = maxJetpackFuel;

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void FixedUpdate()
    {
        bool isJetpackActive = jetpackButton.action.ReadValue<float>() > 0.5f;

        if (isJetpackActive && currentFuel > 0)
        {
            UseJetpack();
        }
        else
        {
            RegenerateFuel();
        }

        ApplyCustomGravity();
    }

    private void UseJetpack()
    {
        Vector3 upwardForce = Vector3.up;
        Vector3 forwardForce = cameraTransform.forward * 0.3f;  // 稍微向前推，增加真实感

        Vector3 finalForce = (upwardForce + forwardForce).normalized * jetpackForce;
        playerRigidbody.AddForce(finalForce, ForceMode.Acceleration);

        currentFuel -= Time.fixedDeltaTime;
    }

    private void RegenerateFuel()
    {
        if (currentFuel < maxJetpackFuel)
        {
            currentFuel += Time.fixedDeltaTime * fuelRegenRate;
        }
    }

    private void ApplyCustomGravity()
    {
        if (currentFuel <= 0)
        {
            playerRigidbody.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);
        }
    }

    public float GetFuelPercentage()
    {
        return currentFuel / maxJetpackFuel;
    }
}
