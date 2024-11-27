using System.Collections;
using UnityEngine;

public class TextSlider : MonoBehaviour
{
    public RectTransform textPanel1;  // ù ��° Text UI �г�
    public RectTransform textPanel2;  // �� ��° Text UI �г�
    public float slideDuration = 0.5f;  // �����̵� ���� �ð�
    public Vector2 slideOffset = new Vector2(-1000, 0);  // �����̵� ������ (�������� �̵�)

    private Vector2 initialPos1;  // ù ��° Text�� �ʱ� ��ġ
    private Vector2 initialPos2;  // �� ��° Text�� �ʱ� ��ġ
    private bool showingFirstText = true;  // ���� ù ��° Text�� ���̴��� ����

    void Start()
    {
        initialPos1 = textPanel1.anchoredPosition;
        initialPos2 = textPanel2.anchoredPosition;

        textPanel2.gameObject.SetActive(false);  // �� ��° Text ��Ȱ��ȭ
    }

    public void SlideToNextText()
    {
        if (showingFirstText)
        {
            StartCoroutine(SlideText(textPanel1, textPanel2, initialPos1, initialPos2, slideOffset));
        }
        else
        {
            StartCoroutine(SlideText(textPanel2, textPanel1, initialPos2, initialPos1, -slideOffset));
        }
    }

    private IEnumerator SlideText(RectTransform fromPanel, RectTransform toPanel, Vector2 fromStart, Vector2 toStart, Vector2 offset)
    {
        // ���� �ؽ�Ʈ ��Ȱ��ȭ
        fromPanel.gameObject.SetActive(false);
        // ���� �ؽ�Ʈ Ȱ��ȭ
        toPanel.gameObject.SetActive(true);

        // �ؽ�Ʈ�� ��ġ ������Ʈ
        fromPanel.anchoredPosition = fromStart + offset;  // ù ��° �ؽ�Ʈ ��ġ
        toPanel.anchoredPosition = toStart;  // �� ��° �ؽ�Ʈ ��ġ

        // ���� ����: �ؽ�Ʈ ��ȯ
        showingFirstText = !showingFirstText;

        // ���� �ؽ�Ʈ�� Ȱ��ȭ�� �� ���� ����
        yield return null;
    }
}
