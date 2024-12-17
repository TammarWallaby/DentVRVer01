using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] uiPanels; // UI 패널 배열
    private int currentIndex = 0; // 현재 활성화된 UI 패널의 인덱스

    void Start()
    {
        // 모든 패널 비활성화 후 첫 번째 패널만 활성화
        InitializeUIPanels();
    }

    // 특정 UI로 전환
    public void ChangeUI(int newIndex)
    {
        if (newIndex < 0 || newIndex >= uiPanels.Length)
        {
            Debug.LogWarning("잘못된 UI 인덱스입니다.");
            return;
        }

        // 현재 UI 비활성화
        uiPanels[currentIndex].SetActive(false);

        // 새 UI 활성화
        uiPanels[newIndex].SetActive(true);

        // 현재 인덱스 갱신
        currentIndex = newIndex;
    }

    // UI 초기화 함수
    private void InitializeUIPanels()
    {
        for (int i = 0; i < uiPanels.Length; i++)
        {
            uiPanels[i].SetActive(i == currentIndex); // 첫 번째 패널만 활성화
        }
    }

    // UI를 다음으로 전환
    public void NextUI()
    {
        int nextIndex = (currentIndex + 1) % uiPanels.Length;
        ChangeUI(nextIndex);
    }

}
