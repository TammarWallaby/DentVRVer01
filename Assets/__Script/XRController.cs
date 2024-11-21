using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRController : MonoBehaviour
{
    [Header("Tools in Scene")] // Scene에 배치할 관상용 도구들, Tag와 Layer 모두 Tool로 설정
    public GameObject scalpel;
    public GameObject driver;
    public GameObject torqueRatchet;
    public GameObject handpiece;
    public GameObject drill2;

    [Header("Tools Model")] // Prefab에서 가져오는 건데, Tag를 설정해줘야 함. Layer는 일단 없음
    public GameObject scalpelM;
    public GameObject driverM;
    public GameObject torqueRatchetM;
    public GameObject handpieceM;
    public GameObject drill2M;
    public GameObject handpieceWithDrill2M;

    [Header("Interaction LayerMask")]
    public InteractionLayerMask defaultMask; // 기본 레이어마스크
    public InteractionLayerMask maskWithoutTool; // 도구 사용 중인 경우 상호작용 레이어마스크

    [Header("Main Setting")]
    public XRController oppositeController;
    public GameObject controllerM;
    public ActionBasedController controller;
    public XRRayInteractor interactor;
    public float combineDistance = 10f;
    [SerializeField] private InputActionReference selectRef; // 그립 버튼 전용
    [SerializeField] private InputActionReference triggerRef; // 트리거 버튼 전용
    [SerializeField] private GameObject currentModel; // 현재 모델 추적용 오브젝트

    private RaycastHit raycastHit;

    private void Awake()
    {
        if (selectRef != null)
        {
            selectRef.action.started += SelectTool;
        }
        if (triggerRef != null)
        {
            triggerRef.action.started += CombineTool;
        }
    }

    private void Start()
    {
        UpdateControllerModel(controllerM);
    }

    private void Update()
    {
        if (currentModel.tag == "Controller")
        {
            interactor.interactionLayers = defaultMask;
        }
        else
        {
            interactor.interactionLayers = maskWithoutTool;
        }
    }

    private void OnDestroy()
    {
        if (selectRef != null)
        {
            selectRef.action.started -= SelectTool;
        }
        if (triggerRef != null)
        {
            triggerRef.action.started -= CombineTool;
        }
    }

    private void SelectTool(InputAction.CallbackContext obj)
    {
        if (currentModel.tag == "Controller")
        {
            if (interactor.TryGetCurrent3DRaycastHit(out raycastHit))
            {
                string goName = raycastHit.collider.gameObject.name;
                if (raycastHit.collider.CompareTag("Tool"))
                {
                    if (goName == "ToolScalpel")
                    {
                        scalpel.SetActive(false);
                        UpdateControllerModel(scalpelM);
                    }
                    else if (goName == "ToolTorqueRatchet")
                    {
                        torqueRatchet.SetActive(false);
                        UpdateControllerModel(torqueRatchetM);
                    }
                    else if (goName == "ToolDriver")
                    {
                        driver.SetActive(false);
                        UpdateControllerModel(driverM);
                    }
                    else if (goName == "ToolHandpiece")
                    {
                        handpiece.SetActive(false);
                        UpdateControllerModel(handpieceM);
                    }
                    else if (goName == "ToolDrill2")
                    {
                        drill2.SetActive(false);
                        UpdateControllerModel(drill2M);
                    }
                }
            }
        }
        else
        {
            if (currentModel.CompareTag("HandpieceWithDrill2"))
            {
                drill2.SetActive(true);
                UpdateControllerModel(handpieceM);
            }
            else
            {
                if (currentModel.CompareTag("Scalpel"))
                {
                    scalpel.SetActive(true);
                }
                else if (currentModel.CompareTag("TorqueRatchet"))
                {
                    torqueRatchet.SetActive(true);
                }
                else if (currentModel.CompareTag("Driver"))
                {
                    driver.SetActive(true);
                }
                else if (currentModel.CompareTag("Handpiece"))
                {
                    handpiece.SetActive(true);
                }
                else if (currentModel.CompareTag("Drill2"))
                {
                    drill2.SetActive(true);
                }

                UpdateControllerModel(controllerM);
            }
        }
    }

    private void CombineTool(InputAction.CallbackContext obj)
    {
        if (Vector3.Distance(controller.transform.position, oppositeController.controller.transform.position) < combineDistance)
        {
            if (currentModel.CompareTag("Drill2") && oppositeController.currentModel.CompareTag("Handpiece"))
            {
                UpdateControllerModel(controllerM);
                oppositeController.UpdateControllerModel(handpieceWithDrill2M);
            }
        }
    }

    private void UpdateControllerModel(GameObject newModelPrefab)
    {
        // 기존 모델이 있다면 제거
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        // 새 모델 생성 및 설정
        if (newModelPrefab != null)
        {
            currentModel = Instantiate(newModelPrefab.gameObject, controller.transform);
        }
    }
}
