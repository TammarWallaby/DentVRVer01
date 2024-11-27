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
        uiPanel.SetActive(false); // UI �г� �ʱ� ��Ȱ��ȭ
    }

    void Update()
    {
        // InputDevice�� ��ȿ���� �ʴٸ� ��Ž��
        if (!inputDevice.isValid)
        {
            InitializeInputDevice();
        }

        // primaryButton ���¸� üũ�Ͽ� UI ���
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

    // InputDevice �ʱ�ȭ �Լ�
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

    // UI �г� Ȱ��ȭ/��Ȱ��ȭ ��� �Լ�
    private void ToggleUIPanel()
    {
        if (uiPanel != null)
        {
            bool isActive = uiPanel.activeSelf;
            uiPanel.SetActive(!isActive);
        }
    }
}
