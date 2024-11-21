using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRController : MonoBehaviour
{
    [Header("Tools in Scene")] // Scene�� ��ġ�� ����� ������, Tag�� Layer ��� Tool�� ����
    public GameObject scalpel;
    public GameObject driver;
    public GameObject torqueRatchet;
    public GameObject handpiece;
    public GameObject drill2;

    [Header("Tools Model")] // Prefab���� �������� �ǵ�, Tag�� ��������� ��. Layer�� �ϴ� ����
    public GameObject scalpelM;
    public GameObject driverM;
    public GameObject torqueRatchetM;
    public GameObject handpieceM;
    public GameObject drill2M;
    public GameObject handpieceWithDrill2M;

    [Header("Interaction LayerMask")]
    public InteractionLayerMask defaultMask; // �⺻ ���̾��ũ
    public InteractionLayerMask maskWithoutTool; // ���� ��� ���� ��� ��ȣ�ۿ� ���̾��ũ

    [Header("Main Setting")]
    public XRController oppositeController;
    public GameObject controllerM;
    public ActionBasedController controller;
    public XRRayInteractor interactor;
    public float combineDistance = 10f;
    [SerializeField] private InputActionReference selectRef; // �׸� ��ư ����
    [SerializeField] private InputActionReference triggerRef; // Ʈ���� ��ư ����
    [SerializeField] private GameObject currentModel; // ���� �� ������ ������Ʈ

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
        // ���� ���� �ִٸ� ����
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        // �� �� ���� �� ����
        if (newModelPrefab != null)
        {
            currentModel = Instantiate(newModelPrefab.gameObject, controller.transform);
        }
    }
}
