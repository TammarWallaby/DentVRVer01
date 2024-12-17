/*
 * 빈 오브젝트에다가 넣으면 됨(아직 이름 지정안함)
 * 설정창 오른손 컨트롤러 A버튼 누르면 나오고 사라짐
 * 설정창이 카메라 기준으로 따라감
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OpenUIPanel : MonoBehaviour
{
    private InputDevice inputDevice;
    private bool buttonPressed = false;
    public Camera xrCamera;  // XR 카메라
    public GameObject uiPanel;  // UI 패널
    public GameObject christmas; 
    public float distanceFromCamera = 0.5f;  // 카메라와 UI 패널 간의 거리
    void Start()
    {
        InitializeInputDevice();
        uiPanel.SetActive(false); // UI 패널 초기 비활성화
        christmas.SetActive(false);
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

        if (uiPanel.activeSelf)  // UI 패널이 활성화될 때
        {
            Vector3 forwardDirection = xrCamera.transform.forward;  // 카메라의 앞 방향
            uiPanel.transform.position = xrCamera.transform.position + forwardDirection * distanceFromCamera;  // 카메라 앞에 UI 패널 위치 설정
            uiPanel.transform.rotation = Quaternion.LookRotation(forwardDirection);  // UI 패널을 카메라를 향하게 회전
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

    public void Christmas()
    {
        if (christmas != null)
        {
            bool isActive = christmas.activeSelf;
            christmas.SetActive(!isActive);
        }
    }
}
