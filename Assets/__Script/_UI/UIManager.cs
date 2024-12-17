using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] uiPanels; // UI �г� �迭
    private int currentIndex = 0; // ���� Ȱ��ȭ�� UI �г��� �ε���

    void Start()
    {
        // ��� �г� ��Ȱ��ȭ �� ù ��° �гθ� Ȱ��ȭ
        InitializeUIPanels();
    }

    // Ư�� UI�� ��ȯ
    public void ChangeUI(int newIndex)
    {
        if (newIndex < 0 || newIndex >= uiPanels.Length)
        {
            Debug.LogWarning("�߸��� UI �ε����Դϴ�.");
            return;
        }

        // ���� UI ��Ȱ��ȭ
        uiPanels[currentIndex].SetActive(false);

        // �� UI Ȱ��ȭ
        uiPanels[newIndex].SetActive(true);

        // ���� �ε��� ����
        currentIndex = newIndex;
    }

    // UI �ʱ�ȭ �Լ�
    private void InitializeUIPanels()
    {
        for (int i = 0; i < uiPanels.Length; i++)
        {
            uiPanels[i].SetActive(i == currentIndex); // ù ��° �гθ� Ȱ��ȭ
        }
    }

    // UI�� �������� ��ȯ
    public void NextUI()
    {
        int nextIndex = (currentIndex + 1) % uiPanels.Length;
        ChangeUI(nextIndex);
    }

}
