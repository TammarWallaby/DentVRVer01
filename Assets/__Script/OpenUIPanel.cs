using UnityEngine;
using UnityEngine.InputSystem;

public class OpenUIPanel : MonoBehaviour
{
    public GameObject uiPanel; // UI 패널
    private InputAction aButtonAction; // A 버튼 액션

    private bool isPanelActive = false;

    void Awake()
    {
        // A 버튼 액션 생성
        aButtonAction = new InputAction(type: InputActionType.Button, binding: "<XRController>{RightHand}/primaryButton");
        aButtonAction.Enable(); // 액션 활성화
        if (uiPanel != null) {
            uiPanel.SetActive(false); // 시작 시 UI 패널 비활성화
        }
    }

    void Update()
    {
        // A 버튼 입력 감지
        if (aButtonAction.WasPerformedThisFrame())
        {
            ToggleUIPanel();
        }
    }

    private void ToggleUIPanel()
    {
        isPanelActive = !isPanelActive;
        uiPanel.SetActive(isPanelActive);
    }
}
