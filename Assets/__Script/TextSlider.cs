using System.Collections;
using UnityEngine;

public class TextSlider : MonoBehaviour
{
    public RectTransform textPanel1;  // ù ��° �ؽ�Ʈ �г�
    public RectTransform textPanel2;  // �� ��° �ؽ�Ʈ �г�
    public float slideDuration = 0.5f;  // �����̵� �ð�
    public Vector2 slideOffset = new Vector2(1000, 0);  // �����̵� ���� (����)

    private Vector2 initialPos1;  // ù ��° �ؽ�Ʈ �ʱ� ��ġ
    private Vector2 initialPos2;  // �� ��° �ؽ�Ʈ �ʱ� ��ġ
    private Vector2 initialPos3;  // �� ��° �ؽ�Ʈ �̵� ��ġ
    private bool showingFirstText = true;  // ù ��° �ؽ�Ʈ ǥ�� ����

    void Start()
    {
        // �ʱ� ��ġ ����
        initialPos1 = textPanel1.anchoredPosition;
        initialPos2 = initialPos1 - slideOffset;
        initialPos3 = initialPos2 + slideOffset;



        textPanel2.gameObject.SetActive(false);     // �� ��° �ؽ�Ʈ �ʱ� ��Ȱ��ȭ
    }

    public void SlideToNextText()
    {
        if (showingFirstText)
        {
            StartCoroutine(SlideText(textPanel1, textPanel2, initialPos1, initialPos3));
        }
        else
        {
            StartCoroutine(SlideText(textPanel2, textPanel1, initialPos3, initialPos1));
        }
    }

    private IEnumerator SlideText(RectTransform fromPanel, RectTransform toPanel, Vector2 fromStart, Vector2 toStart)
    {
        float elapsedTime = 0f;

        // �����̵� ���� �� ��� �г� Ȱ��ȭ
        toPanel.gameObject.SetActive(true);

        while (elapsedTime < slideDuration)
        {
            // �����̵� �ִϸ��̼� ����
            fromPanel.anchoredPosition = Vector2.Lerp(fromStart, fromStart + slideOffset, elapsedTime / slideDuration);
            toPanel.anchoredPosition = Vector2.Lerp(toStart - slideOffset, toStart, elapsedTime / slideDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �����̵� ���� �� ��ġ ����
        fromPanel.anchoredPosition = fromStart + slideOffset;
        toPanel.anchoredPosition = toStart;

        // �����̵尡 ���� �г� ��Ȱ��ȭ
        fromPanel.gameObject.SetActive(false);

        // ǥ�� ���� ��ȯ
        showingFirstText = !showingFirstText;
    }
}
