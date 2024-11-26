using UnityEngine;

public class UICamera : MonoBehaviour
{
    public Camera xrCamera;  // XR 카메라
    public GameObject uiPanel;  // UI 패널

    public float distanceFromCamera = 0.5f;  // 카메라와 UI 패널 간의 거리

    // Update is called once per frame
    void Update()
    {
        if (uiPanel.activeSelf)  // UI 패널이 활성화될 때
        {
            Vector3 forwardDirection = xrCamera.transform.forward;  // 카메라의 앞 방향
            uiPanel.transform.position = xrCamera.transform.position + forwardDirection * distanceFromCamera;  // 카메라 앞에 UI 패널 위치 설정
            uiPanel.transform.rotation = Quaternion.LookRotation(forwardDirection);  // UI 패널을 카메라를 향하게 회전
        }
    }
}
