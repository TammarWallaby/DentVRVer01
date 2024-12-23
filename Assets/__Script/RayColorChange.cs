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
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color highlightColor = Color.yellow;

    private void Start()
    {
        // ������Ʈ���� �Ҵ���� �ʾҴٸ� �ڵ����� ��������
        if (rayInteractor == null)
            rayInteractor = GetComponent<XRRayInteractor>();
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
        if (lineVisual == null)
            lineVisual = GetComponent<XRInteractorLineVisual>();

        // �⺻ ���� ����
        SetRayColor(defaultColor);
    }

    private void Update()
    {
        // Ray�� ��ü�� �浹�ߴ��� Ȯ��
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            // �浹�� ��ü�� Donut �±׸� ������ �ִ��� Ȯ��
            if (hit.collider.CompareTag("Donut")||hit.collider.CompareTag("Tool"))
            {
                SetRayColor(highlightColor);
                return;
            }
        }

        // �浹���� �ʾҰų� Donut�� �ƴ� ��� �⺻ �������� �ǵ�����
        SetRayColor(defaultColor);
    }

    private void SetRayColor(Color color)
    {
        // Line Renderer ���� ����
        if (lineRenderer != null)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }

        // XR Interactor Line Visual ���� ����
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
