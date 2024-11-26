using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OpenUIPanel : MonoBehaviour
{
    public GameObject uiPanel;  // ���� ���� UI �г�
    private InputDevice inputDevice;  // XR Input Device
    private InputFeatureUsage<bool> buttonOne = new InputFeatureUsage<bool>("Button.One"); // Button.One ����
    private bool buttonPressed = false;

    void Start()
    {
        // XRInputSubsystem���� InputDevice�� ã���ϴ�.
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, inputDevices); // ������ ��Ʈ�ѷ� �Է� ����̽� ��������

        if (inputDevices.Count > 0)
        {
            inputDevice = inputDevices[0];  // ù ��° ����̽��� ���
        }

        uiPanel.SetActive(false); // UI �г� �ʱ� ��Ȱ��ȭ
    }

    void Update()
    {
        if (inputDevice.isValid && inputDevice.TryGetFeatureValue(buttonOne, out bool buttonOneValue))
        {
            if (buttonOneValue && !buttonPressed)
            {
                ToggleUIPanel();
                buttonPressed = true; // ��ư�� �������� ǥ��
            }
            else if (!buttonOneValue)
            {
                buttonPressed = false; // ��ư�� �������� ���� �ʱ�ȭ
            }
        }
    }

    // UI �г��� Ȱ��ȭ�ϰų� ��Ȱ��ȭ�ϴ� �Լ�
    void ToggleUIPanel()
    {
        if (uiPanel != null)
        {
            bool isActive = uiPanel.activeSelf;
            uiPanel.SetActive(!isActive); // ���� ������ �ݴ�� UI �г��� ���
        }
    }
}
