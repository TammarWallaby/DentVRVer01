using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OpenUIPanel : MonoBehaviour
{
    public GameObject uiPanel;  // 열고 싶은 UI 패널
    private InputDevice inputDevice;  // XR Input Device
    private InputFeatureUsage<bool> buttonOne = new InputFeatureUsage<bool>("Button.One"); // Button.One 정의
    private bool buttonPressed = false;

    void Start()
    {
        // XRInputSubsystem에서 InputDevice를 찾습니다.
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, inputDevices); // 오른손 컨트롤러 입력 디바이스 가져오기

        if (inputDevices.Count > 0)
        {
            inputDevice = inputDevices[0];  // 첫 번째 디바이스를 사용
        }

        uiPanel.SetActive(false); // UI 패널 초기 비활성화
    }

    void Update()
    {
        if (inputDevice.isValid && inputDevice.TryGetFeatureValue(buttonOne, out bool buttonOneValue))
        {
            if (buttonOneValue && !buttonPressed)
            {
                ToggleUIPanel();
                buttonPressed = true; // 버튼이 눌렸음을 표시
            }
            else if (!buttonOneValue)
            {
                buttonPressed = false; // 버튼이 떼어지면 상태 초기화
            }
        }
    }

    // UI 패널을 활성화하거나 비활성화하는 함수
    void ToggleUIPanel()
    {
        if (uiPanel != null)
        {
            bool isActive = uiPanel.activeSelf;
            uiPanel.SetActive(!isActive); // 현재 상태의 반대로 UI 패널을 토글
        }
    }
}
