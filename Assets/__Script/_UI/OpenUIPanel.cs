/*
 * SettingPanel 안에 넣음
 * 설정창 오른손 컨트롤러 A버튼 누르면 나오고 사라짐
 * 설정창이 카메라 기준으로 따라감
 * 설정창 키면 Ray가 늘어나서 닿음
 * 카메라 회전 자유도 on off
 * 눈송이 누르면 크리스마스 모드 on off
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class OpenUIPanel : MonoBehaviour
{
    private InputDevice inputDevice;
    private bool buttonPressed = false;
    public Camera xrCamera;  // XR 카메라
    public GameObject uiPanel;  // UI 패널
    public GameObject christmas; // 눈송이
    public float distanceFromCamera = 0.5f;  // 카메라와 UI 패널 간의 거리
    public XRRayInteractor leftRayInteractor; // XR의 왼쪽 컨트롤러의 RAY 참조
    public XRRayInteractor rightRayInteractor; // XR의 오른쪽 컨트롤러의 RAY 참조

    public Button FreeButton;  // 버튼 색
    public Button FixedButton;  // 버튼 색

    private Color FreeButtonColor; 
    private Color FixedButtonColor; 
    private Color whiteColor = Color.white; // 흰색

    public ContinuousTurnProviderBase FreeTurn; // XR의 Locomotion System 오브젝트 내에 있는 스크립트 참조
    public SnapTurnProviderBase FixedTurn; // XR의 Locomotion System 오브젝트 내에 있는 스크립트 참조
    void Start()
    {
        InitializeInputDevice();
        uiPanel.SetActive(false); // UI 패널 초기 비활성화
        christmas.SetActive(true);

        FreeButtonColor = FreeButton.GetComponent<Image>().color; 
        FixedButtonColor = FixedButton.GetComponent<Image>().color; 
        FreeButtonClicked(); // 초기 버튼 상태 설정
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
    private void InitializeInputDevice() // XR 컨트롤러 관련 스크립트
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
            if (uiPanel.activeSelf) // 설정창이 활성화 일 때, Ray의 길이가 길어짐
            {
                leftRayInteractor.maxRaycastDistance = 1f;
                rightRayInteractor.maxRaycastDistance = 1f;
            }
            else // 설정창이 비활성화 일 때, Ray의 길이 초기값
            {
                leftRayInteractor.maxRaycastDistance = 0.25f;
                rightRayInteractor.maxRaycastDistance = 0.25f;
            }
        }
    }

    public void Christmas() // 크리스마스 모드
    {
        if (christmas != null)
        {
            bool isActive = christmas.activeSelf;
            christmas.SetActive(!isActive);
        }
    }

    // 버튼 색상 변경 함수
    private void SetButtonColors(Button button, Color targetColor) // 이미지 색상 변경
    {
        Image buttonImage = button.GetComponent<Image>(); // 버튼의 이미지 컴포넌트를 가져옴
        if (buttonImage != null)
        {
            buttonImage.color = targetColor; // 이미지 색상을 변경
        }
    }
    public void FreeButtonClicked() // 자유 회전 버전일 때
    {
        SetButtonColors(FreeButton, FreeButtonColor); // Free 버튼의 색상을 기본 색상으로 설정
        SetButtonColors(FixedButton, whiteColor); // Fixed 버튼의 색상을 흰색으로 설정
        FreeTurn.enabled = true; // ContinuousTurnProviderBase 스크립트 활성화
        FixedTurn.enabled = false; // SnapTurnProviderBase 스크립트 비활성화
    }
    public void FixedButtonClicked() // 고정 회전 버튼일 때
    {
        SetButtonColors(FreeButton, whiteColor); // Free 버튼의 색상을 흰색으로 설정
        SetButtonColors(FixedButton, FixedButtonColor); // Fixed 버튼의 색상을 기본 색상으로 설정
        FreeTurn.enabled = false; // ContinuousTurnProviderBase 스크립트 비활성화
        FixedTurn.enabled = true; // SnapTurnProviderBase 스크립트 활성화
    }
}
