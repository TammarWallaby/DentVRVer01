/* 좌우 컨트롤러에 들어갈 스크립트
 * Ray의 색 변경
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayColorChange : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private XRInteractorLineVisual lineVisual;

    [Header("Colors")]
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color highlightColor;

    private void Start()
    {
        // 컴포넌트들이 할당되지 않았다면 자동으로 가져오기
        if (rayInteractor == null)
            rayInteractor = GetComponent<XRRayInteractor>();
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
        if (lineVisual == null)
            lineVisual = GetComponent<XRInteractorLineVisual>();

        // 기본 색상 설정
        SetRayColor(defaultColor);
    }

    private void Update()
    {
        // Ray가 물체와 충돌했는지 확인
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            // 충돌한 물체가 Donut 태그를 가지고 있는지 확인
            if (hit.collider.CompareTag("Donut")||hit.collider.CompareTag("Tool")||hit.collider.CompareTag("ObjectUI"))
            {
                SetRayColor(highlightColor);
                return;
            }
        }

        // 충돌하지 않았거나 Donut이 아닌 경우 기본 색상으로 되돌리기
        SetRayColor(defaultColor);
    }

    private void SetRayColor(Color color)
    {
        // Line Renderer 색상 변경
        if (lineRenderer != null)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }

        // XR Interactor Line Visual 색상 변경
        if (lineVisual != null)
        {
            lineVisual.validColorGradient = new Gradient()
            {
                colorKeys = new GradientColorKey[]
                {
                    new GradientColorKey(color, 0f),
                    new GradientColorKey(color, 1f)
                }
            };
        }
    }
}
