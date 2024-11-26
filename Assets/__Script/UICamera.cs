using UnityEngine;

public class UICamera : MonoBehaviour
{
    public Camera xrCamera;  // XR ī�޶�
    public GameObject uiPanel;  // UI �г�

    public float distanceFromCamera = 0.5f;  // ī�޶�� UI �г� ���� �Ÿ�

    // Update is called once per frame
    void Update()
    {
        if (uiPanel.activeSelf)  // UI �г��� Ȱ��ȭ�� ��
        {
            Vector3 forwardDirection = xrCamera.transform.forward;  // ī�޶��� �� ����
            uiPanel.transform.position = xrCamera.transform.position + forwardDirection * distanceFromCamera;  // ī�޶� �տ� UI �г� ��ġ ����
            uiPanel.transform.rotation = Quaternion.LookRotation(forwardDirection);  // UI �г��� ī�޶� ���ϰ� ȸ��
        }
    }
}
