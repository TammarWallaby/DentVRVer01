using System.Collections;
using UnityEngine;

public class TextSlider : MonoBehaviour
{
    public RectTransform textPanel1;  // 첫 번째 텍스트 패널
    public RectTransform textPanel2;  // 두 번째 텍스트 패널
    public float slideDuration = 0.5f;  // 슬라이드 시간
    public Vector2 slideOffset = new Vector2(1000, 0);  // 슬라이드 방향 (왼쪽)

    private Vector2 initialPos1;  // 첫 번째 텍스트 초기 위치
    private Vector2 initialPos2;  // 두 번째 텍스트 초기 위치
    private Vector2 initialPos3;  // 두 번째 텍스트 이동 위치
    private bool showingFirstText = true;  // 첫 번째 텍스트 표시 여부

    void Start()
    {
        // 초기 위치 설정
        initialPos1 = textPanel1.anchoredPosition;
        initialPos2 = initialPos1 - slideOffset;
        initialPos3 = initialPos2 + slideOffset;



        textPanel2.gameObject.SetActive(false);     // 두 번째 텍스트 초기 비활성화
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

        // 표시 상태 전환
        showingFirstText = !showingFirstText;
    }
}
