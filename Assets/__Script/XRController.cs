﻿/* XR Origin의 Left Controller와 Right Controller에 들어갈 스크립트
 * 컨트롤러의 현재 도구 교체 및 도구 결합
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRController : MonoBehaviour
{
    [Header("Main Setting")]
    public XRController oppositeController;
    public GameObject controllerM;
    public ActionBasedController controller;
    public XRRayInteractor interactor;
    public float combineDistance = 10f; // 결합 도구 결합 시, 좌우 컨트롤러 거리
    public InputActionReference selectRef; // 그립 버튼 전용
    public InputActionReference triggerRef; // 트리거 버튼 전용
    public GameObject currentModel; // 현재 모델 추적용 오브젝트

    [Header("Interaction LayerMask")]
    public InteractionLayerMask defaultMask; // 기본 레이어마스크
    public InteractionLayerMask maskWithTool; // 도구 사용 중인 경우 상호작용 레이어마스크

    [Header("Tools in Scene")] // Scene에 배치할 관상용 도구들, Tag와 Layer 모두 Tool로 설정
    public GameObject abutment;
    public GameObject drill2;
    public GameObject drill3;
    public GameObject drill4;
    public GameObject driver;
    public GameObject driver2;
    public GameObject elevator;
    public GameObject fixture;
    public GameObject handpiece;
    public GameObject healingAbutment;
    public GameObject scalpel;
    public GameObject syringe;
    public GameObject syringeWithFluid;
    public GameObject torqueRatchet;
    public GameObject needleWrapping;
    public GameObject crown;

    [Header("Tools Model")] // Prefab 폴더에서 가져오는 건데, Tag를 설정해줘야 함. Layer는 일단 없음
    public GameObject abutmentM;
    public GameObject drill2M;
    public GameObject drill3M;
    public GameObject drill4M;
    public GameObject driverM;
    public GameObject driver2M;
    public GameObject elevatorM;
    public GameObject fixtureM;
    public GameObject handpieceM;
    public GameObject handpieceWithDrill2M;
    public GameObject handpieceWithDrill3M;
    public GameObject handpieceWithDrill4M;
    public GameObject healingAbutmentM;
    public GameObject scalpelM;
    public GameObject syringeM;
    public GameObject syringeWithFluidM;
    public GameObject torqueRatchetM;
    public GameObject needleM;
    public GameObject crownM;


    public RaycastHit hit;

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
            if(interactor.interactionLayers!=defaultMask)
            {
                interactor.interactionLayers = defaultMask;
            }
        }
        else
        {
            if (interactor.interactionLayers != maskWithTool)
            {
                interactor.interactionLayers = maskWithTool;
            }
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
            if (interactor.TryGetCurrent3DRaycastHit(out hit))
            {
                string goName = hit.collider.gameObject.name;
                if (hit.collider.CompareTag("Tool"))
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
                    else if (goName == "SyringeWithFluid")
                    {
                        syringeWithFluid.SetActive(false);
                        UpdateControllerModel(syringeWithFluidM);
                    }
                    else if (goName == "TorqueRatchet")
                    {
                        torqueRatchet.SetActive(false);
                        UpdateControllerModel(torqueRatchetM);
                    }
                    else if (goName == "NeedleWrapping")
                    {
                        needleWrapping.SetActive(false);
                        UpdateControllerModel(needleM);
                    }
                    else if (goName == "Crown")
                    {
                        crown.SetActive(false);
                        UpdateControllerModel(crownM);
                    }
                    
                }
            }
        }
        else
        {
            // 결합 도구 먼저 판정
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
            else // 결합 도구 이외 판정
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
                else if (currentModel.CompareTag("SyringeWithFluid"))
                {
                    syringeWithFluid.SetActive(true);
                }
                else if (currentModel.CompareTag("TorqueRatchet"))
                {
                    torqueRatchet.SetActive(true);
                }
                else if (currentModel.CompareTag("Needle"))
                {
                    needleWrapping.SetActive(true);
                }
                else if (currentModel.CompareTag("Crown"))
                {
                    crown.SetActive(true);
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
            else if (currentModel.CompareTag("Handpiece") && oppositeController.currentModel.CompareTag("Drill2"))
            {
                UpdateControllerModel(handpieceWithDrill2M);
                oppositeController.UpdateControllerModel(controllerM);
            }
            else if (currentModel.CompareTag("Handpiece") && oppositeController.currentModel.CompareTag("Drill3"))
            {
                UpdateControllerModel(handpieceWithDrill3M);
                oppositeController.UpdateControllerModel(controllerM);
            }
            else if (currentModel.CompareTag("Handpiece") && oppositeController.currentModel.CompareTag("Drill4"))
            {
                UpdateControllerModel(handpieceWithDrill4M);
                oppositeController.UpdateControllerModel(controllerM);
            }
        }
    }

    public void UpdateControllerModel(GameObject newModelPrefab)
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

        if (controller != null)
        {
            controller.SendHapticImpulse(0.2f, 0.1f);
        }
    }
}
