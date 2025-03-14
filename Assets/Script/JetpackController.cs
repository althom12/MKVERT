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
    public Transform cameraTransform;         // 头部方向参考（VR Main Camera）
    public float fuelConsumptionRate = 2f;  // 燃料消耗系数，值越大消耗速度越快


    [Header("Input Action")]
    public InputActionProperty jetpackButton;  // XR按钮映射

    private Rigidbody playerRigidbody;
    private float currentFuel;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.useGravity = true;   // 让玩家默认受重力影响
        currentFuel = maxJetpackFuel;

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void FixedUpdate()
    {
        bool isJetpackActive = jetpackButton.action.ReadValue<float>() > 0.5f;
        // 打印日志查看按钮状态
        Debug.Log("Jetpack Button Pressed: " + isJetpackActive);
        Debug.Log("Fuel: " + currentFuel);

        if (isJetpackActive && currentFuel > 0)
        {
            UseJetpack();
        }
        else
        {
            RegenerateFuel();
            playerRigidbody.useGravity = true; // 松开键后重力生效
        }
    }

    private void UseJetpack()
    {
        playerRigidbody.useGravity = false; // 关闭重力
        Vector3 upwardForce = Vector3.up;
        Vector3 forwardForce = cameraTransform.forward * 0.3f;  // 让飞行更自然

        Vector3 finalForce = (upwardForce + forwardForce).normalized * jetpackForce;
        Debug.Log("Jetpack Force: " + finalForce);
        playerRigidbody.AddForce(finalForce, ForceMode.Acceleration);

        currentFuel -= Time.fixedDeltaTime* 4f;
    }

    private void RegenerateFuel()
    {
        if (currentFuel < maxJetpackFuel)
        {
            currentFuel += Time.fixedDeltaTime * fuelRegenRate;
        }
    }

    public float GetFuelPercentage()
    {
        return currentFuel / maxJetpackFuel;
    }
}
