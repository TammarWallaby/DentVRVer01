using UnityEngine;
using UnityEngine.InputSystem;

public class OpenUIPanel : MonoBehaviour
{
    public GameObject uiPanel; // UI �г�
    private InputAction aButtonAction; // A ��ư �׼�

    private bool isPanelActive = false;

    void Awake()
    {
        // A ��ư �׼� ����
        aButtonAction = new InputAction(type: InputActionType.Button, binding: "<XRController>{RightHand}/primaryButton");
        aButtonAction.Enable(); // �׼� Ȱ��ȭ
        if (uiPanel != null) {
            uiPanel.SetActive(false); // ���� �� UI �г� ��Ȱ��ȭ
        }
    }

    void Update()
    {
        // A ��ư �Է� ����
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
