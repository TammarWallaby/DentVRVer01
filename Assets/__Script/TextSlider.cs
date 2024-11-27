using System.Collections;
using UnityEngine;

public class TextSlider : MonoBehaviour
{
    public RectTransform textPanel1;  // 첫 번째 Text UI 패널
    public RectTransform textPanel2;  // 두 번째 Text UI 패널
    public float slideDuration = 0.5f;  // 슬라이드 지속 시간
    public Vector2 slideOffset = new Vector2(-1000, 0);  // 슬라이드 오프셋 (왼쪽으로 이동)

    private Vector2 initialPos1;  // 첫 번째 Text의 초기 위치
    private Vector2 initialPos2;  // 두 번째 Text의 초기 위치
    private bool showingFirstText = true;  // 현재 첫 번째 Text가 보이는지 여부

    void Start()
    {
        initialPos1 = textPanel1.anchoredPosition;
        initialPos2 = textPanel2.anchoredPosition;

        textPanel2.gameObject.SetActive(false);  // 두 번째 Text 비활성화
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
        // 현재 텍스트 비활성화
        fromPanel.gameObject.SetActive(false);
        // 다음 텍스트 활성화
        toPanel.gameObject.SetActive(true);

        // 텍스트의 위치 업데이트
        fromPanel.anchoredPosition = fromStart + offset;  // 첫 번째 텍스트 위치
        toPanel.anchoredPosition = toStart;  // 두 번째 텍스트 위치

        // 상태 반전: 텍스트 전환
        showingFirstText = !showingFirstText;

        // 다음 텍스트가 활성화된 후 상태 유지
        yield return null;
    }
}
