using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRController : MonoBehaviour
{
    [Header("Tools in Scene")] // Scene�� ��ġ�� ����� ������, Tag�� Layer ��� Tool�� ����
    public GameObject abutment;
    public GameObject drill2;
    public GameObject drill3;
    public GameObject drill4;
    public GameObject driver;
    public GameObject driver2;
    public GameObject driver3;
    public GameObject elevator;
    public GameObject fixture;
    public GameObject handpiece;
    public GameObject handpiece2;
    public GameObject healingAbutment;
    public GameObject scalpel;
    public GameObject syringe;
    public GameObject torqueRatchet;

    [Header("Tools Model")] // Prefab���� �������� �ǵ�, Tag�� ��������� ��. Layer�� �ϴ� ����
    public GameObject abutmentM;
    public GameObject drill2M;
    public GameObject drill3M;
    public GameObject drill4M;
    public GameObject driverM;
    public GameObject driver2M;
    public GameObject driver3M;
    public GameObject driverWithAbutmentM;
    public GameObject driverWithHealingAbutmentM;
    public GameObject elevatorM;
    public GameObject fixtureM;
    public GameObject handpieceM;
    public GameObject handpiece2M;
    public GameObject handpieceWithDrill2M;
    public GameObject handpieceWithDrill3M;
    public GameObject handpieceWithDrill4M;
    public GameObject handpieceWithFixtureM;
    public GameObject healingAbutmentM;
    public GameObject scalpelM;
    public GameObject syringeM;
    public GameObject torqueRatchetM;

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
        if (currentModel.tag == "Controller") // �̰� ���߿� �����ؾ� �� 
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
                    if (goName == "Abutment")
                    {
                        abutment.SetActive(false);
                        UpdateControllerModel(abutmentM);
                    }
                    else if (goName == "Drill2")
                    {
                        drill2.SetActive(false);
                        UpdateControllerModel(drill2M);
                    }
                    else if (goName == "Drill3")
                    {
                        drill3.SetActive(false);
                        UpdateControllerModel(drill3M);
                    }
                    else if (goName == "Drill4")
                    {
                        drill4.SetActive(false);
                        UpdateControllerModel(drill4M);
                    }
                    else if (goName == "Driver")
                    {
                        driver.SetActive(false);
                        UpdateControllerModel(driverM);
                    }
                    else if (goName == "Driver2")
                    {
                        driver2.SetActive(false);
                        UpdateControllerModel(driver2M);
                    }
                    else if (goName == "Driver3")
                    {
                        driver3.SetActive(false);
                        UpdateControllerModel(driver3M);
                    }
                    else if (goName == "Elevator")
                    {
                        elevator.SetActive(false);
                        UpdateControllerModel(elevatorM);
                    }
                    else if (goName == "Fixture")
                    {
                        fixture.SetActive(false);
                        UpdateControllerModel(fixtureM);
                    }
                    else if (goName == "Handpiece")
                    {
                        handpiece.SetActive(false);
                        UpdateControllerModel(handpieceM);
                    }
                    else if (goName == "Handpiece2")
                    {
                        handpiece2.SetActive(false);
                        UpdateControllerModel(handpiece2M);
                    }
                    else if (goName == "HealingAbutment")
                    {
                        healingAbutment.SetActive(false);
                        UpdateControllerModel (healingAbutmentM);
                    }
                    else if (goName == "Scalpel")
                    {
                        scalpel.SetActive(false);
                        UpdateControllerModel(scalpelM);
                    }
                    else if (goName == "Syringe")
                    {
                        syringe.SetActive(false);
                        UpdateControllerModel(syringeM);
                    }
                    else if (goName == "TorqueRatchet")
                    {
                        torqueRatchet.SetActive(false);
                        UpdateControllerModel(torqueRatchetM);
                    }
                }
            }
        }
        else
        {
            // ���� ���� ���� ����
            if (currentModel.CompareTag("HandpieceWithDrill2")) 
            {
                drill2.SetActive(true);
                UpdateControllerModel(handpieceM);
            }
            else if (currentModel.CompareTag("HandpieceWithDrill3"))
            {
                drill3.SetActive(true);
                UpdateControllerModel(handpieceM);
            }
            else if (currentModel.CompareTag("HandpieceWithDrill4"))
            {
                drill4.SetActive(true);
                UpdateControllerModel(handpieceM);
            }
            else if (currentModel.CompareTag("HandpieceWithFixture"))
            {
                fixture.SetActive(true);
                UpdateControllerModel(handpiece2M);
            }
            else if (currentModel.CompareTag("DriverWithHealingAbutment"))
            {
                healingAbutment.SetActive(true);
                UpdateControllerModel (driverM);
            }
            else if (currentModel.CompareTag("DriverWithAbutment"))
            {
                abutment.SetActive(true);
                UpdateControllerModel(driver3M);
            }
            else // ���� ���� �̿� ����
            {
                if (currentModel.CompareTag("Abutment"))
                {
                    abutment.SetActive(true);
                }
                else if (currentModel.CompareTag("Drill2"))
                {
                    drill2.SetActive(true);
                }
                else if (currentModel.CompareTag("Drill3"))
                {
                    drill3.SetActive(true);
                }
                else if (currentModel.CompareTag("Drill4"))
                {
                    drill4.SetActive(true);
                }
                else if (currentModel.CompareTag("Driver"))
                {
                    driver.SetActive(true);
                }
                else if (currentModel.CompareTag("Driver2"))
                {
                    driver2.SetActive(true);
                }
                else if (currentModel.CompareTag("Driver3"))
                {
                    driver3.SetActive(true);
                }
                else if (currentModel.CompareTag("Elevator"))
                {
                    elevator.SetActive(true);
                }
                else if (currentModel.CompareTag("Fixture"))
                {
                    fixture.SetActive(true);
                }
                else if (currentModel.CompareTag("Handpiece"))
                {
                    handpiece.SetActive(true);
                }
                else if (currentModel.CompareTag("Handpiece2"))
                {
                    handpiece2.SetActive(true);
                }
                else if (currentModel.CompareTag("HealingAbutment"))
                {
                    healingAbutment.SetActive(true);
                }
                else if (currentModel.CompareTag("Scalpel"))
                {
                    scalpel.SetActive(true);
                }
                else if (currentModel.CompareTag("Syringe"))
                {
                    syringe.SetActive(true);
                }
                else if (currentModel.CompareTag("TorqueRatchet"))
                {
                    torqueRatchet.SetActive(true);
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
            else if (currentModel.CompareTag("Drill3") && oppositeController.currentModel.CompareTag("Handpiece"))
            {
                UpdateControllerModel(controllerM);
                oppositeController.UpdateControllerModel(handpieceWithDrill3M);
            }
            else if (currentModel.CompareTag("Drill4") && oppositeController.currentModel.CompareTag("Handpiece"))
            {
                UpdateControllerModel(controllerM);
                oppositeController.UpdateControllerModel(handpieceWithDrill4M);
            }
            else if (currentModel.CompareTag("Fixture") && oppositeController.currentModel.CompareTag("Handpiece2"))
            {
                UpdateControllerModel(controllerM);
                oppositeController.UpdateControllerModel(handpieceWithFixtureM);
            }
            else if (currentModel.CompareTag("HealingAbutment") && oppositeController.currentModel.CompareTag("Driver"))
            {
                UpdateControllerModel(controllerM);
                oppositeController.UpdateControllerModel(driverWithHealingAbutmentM);
            }
            else if (currentModel.CompareTag("Abutment") && oppositeController.currentModel.CompareTag("Driver3"))
            {
                UpdateControllerModel(controllerM);
                oppositeController.UpdateControllerModel(driverWithAbutmentM);
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
