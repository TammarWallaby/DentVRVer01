using System.Collections;
using UnityEngine;

public class TextSlider : MonoBehaviour
{
    public RectTransform[] textPanels;  // 텍스트 패널 배열
    public float slideDuration = 0.5f;  // 슬라이드 시간
    public Vector2 slideOffset = new Vector2(1000, 0);  // 슬라이드 방향 (왼쪽)

    private Vector2[] initialPositions;  // 텍스트 초기 위치 배열
    private int currentTextIndex = 0;    // 현재 표시 중인 텍스트 인덱스

    void Start()
    {
        // 초기 위치 배열 초기화
        initialPositions = new Vector2[textPanels.Length];

        for (int i = 0; i < textPanels.Length; i++)
        {
            initialPositions[i] = textPanels[i].anchoredPosition;
            textPanels[i].gameObject.SetActive(i == currentTextIndex); // 첫 번째 텍스트만 활성화
        }
    }

    public void SlideToNextText()
    {
        if (textPanels == null || textPanels.Length == 0)
        {
            Debug.LogWarning("TextPanels 배열이 비어 있습니다. 슬라이드를 수행할 수 없습니다.");
            return;
        }

        int nextTextIndex = (currentTextIndex + 1) % textPanels.Length;
        StartCoroutine(SlideText(textPanels[currentTextIndex], textPanels[nextTextIndex], initialPositions[currentTextIndex], initialPositions[nextTextIndex]));
    }


    private IEnumerator SlideText(RectTransform fromPanel, RectTransform toPanel, Vector2 fromStart, Vector2 toStart)
    {
        float elapsedTime = 0f;

        // 슬라이드 시작 시 대상 패널 활성화
        toPanel.gameObject.SetActive(true);

        while (elapsedTime < slideDuration)
        {
            // 슬라이드 애니메이션 실행
            fromPanel.anchoredPosition = Vector2.Lerp(fromStart, fromStart + slideOffset, elapsedTime / slideDuration);
            toPanel.anchoredPosition = Vector2.Lerp(toStart - slideOffset, toStart, elapsedTime / slideDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 슬라이드 종료 후 위치 보정
        fromPanel.anchoredPosition = fromStart + slideOffset;
        toPanel.anchoredPosition = toStart;

        // 슬라이드가 끝난 패널 비활성화
        fromPanel.gameObject.SetActive(false);

        // 표시 중인 텍스트 인덱스 업데이트
        currentTextIndex = System.Array.IndexOf(textPanels, toPanel);
    }
}
