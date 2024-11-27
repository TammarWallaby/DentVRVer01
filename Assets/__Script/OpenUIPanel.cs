using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OpenUIPanel : MonoBehaviour
{
    public GameObject uiPanel;
    private InputDevice inputDevice;
    private bool buttonPressed = false;

    void Start()
    {
        InitializeInputDevice();
        uiPanel.SetActive(false); // UI 패널 초기 비활성화
    }

    void Update()
    {
        // InputDevice가 유효하지 않다면 재탐색
        if (!inputDevice.isValid)
        {
            InitializeInputDevice();
        }

        // primaryButton 상태를 체크하여 UI 토글
        if (inputDevice.isValid && inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonOneValue))
        {
            if (buttonOneValue && !buttonPressed)
            {
                ToggleUIPanel();
                buttonPressed = true;
            }
            else if (!buttonOneValue)
            {
                buttonPressed = false;
            }
        }
    }

    // InputDevice 초기화 함수
    private void InitializeInputDevice()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, inputDevices);

        if (inputDevices.Count > 0)
        {
            inputDevice = inputDevices[0];
            Debug.Log($"Input Device Found: {inputDevice.name}");
        }
        else
        {
            Debug.LogWarning("No Input Device found for Right Hand.");
        }
    }

    // UI 패널 활성화/비활성화 토글 함수
    private void ToggleUIPanel()
    {
        if (uiPanel != null)
        {
            bool isActive = uiPanel.activeSelf;
            uiPanel.SetActive(!isActive);
        }
    }
}
