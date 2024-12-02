/* XR Origin의 Left Controller와 Right Controller에 들어갈 스크립트
 * 플레이어가 각 방에서 수행 할 수술 구현
 */


using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class SurgeryManager : MonoBehaviour
{
    [Header("Main Setting")]
    public InputActionReference triggerRef;
    public XRController controller;
    public Player player;


    //[Header("Start")]

    [Header("Anesthesia")]
    public GameObject donutAnesthesia1; // 도넛은 모두 씬에 있는 오브젝트를 넣음
    public GameObject syringe; // 프리팹 모델 넣어야 함!
    public GameObject syringeHandle; // 프리팹 모델 넣어야 함!

    [Header("Incision")]
    public GameObject donutIncision1;
    public GameObject donutIncision2;
    public GameObject donutIncision3;
    public GameObject gumIncision1;
    public GameObject gumIncision2;
    public GameObject gumIncision3;

    [Header("Elevation")]
    public GameObject donutElevation1;
    public GameObject donutElevation2;
    public GameObject gumElevation1;
    public GameObject gunElevation2;

    [Header("Drill")]
    public GameObject donutDrill1;
    public GameObject donutDrill2;
    public GameObject donutDrill3;
    public GameObject drill2; // 프리팹 모델
    public GameObject drill3; // 프리팹
    public GameObject drill4; // 프
    public GameObject gumDrill1;
    public GameObject gumDrill2;
    public GameObject gumDrill3;

    [Header("FixturePlace")]
    public GameObject donutFixturePlace1;
    public GameObject donutFixturePlace2;
    public GameObject fixture1; // 씬에 미리 배치해 둘 것
    public GameObject gumFixture1;

    [Header("HAPlace")]
    public GameObject donutHAPlace1;
    public GameObject donutHAPlace2;
    public GameObject healingAbutment1; //씬에 미리 배치

    //[Header("Suture")] 모델링 실 개수 정해지면

    //[Header("Wait")] Wait방 조건 정해지면

    [Header("HARemove")]
    public GameObject donutHARemove1;
    public GameObject healingAbutment2;

    [Header("AbutmentPlace")]
    public GameObject donutAbutmentPlace1;
    public GameObject donutAbutmentPlace2;
    public GameObject abutment1;

    [Header("CrownPlace")]
    public GameObject donutCrownPlace1;
    public GameObject donutCrownPlace2;
    public GameObject crown1;


    //-------------------------------------------
    private Sequence gaugeSequence;
    private Sequence moveSequence;
    private bool isSequenceRunning = false;
    private bool isSequenceAssigned = false;
    private RaycastHit hit;
    private string donutName;

    private void Awake()
    {
        if (triggerRef != null)
        {
            triggerRef.action.started += HandleActionStarted;
            triggerRef.action.canceled += HandleActionCanceled;
        }
    }

    private void Update()
    {
        if (controller.interactor.TryGetCurrent3DRaycastHit(out hit))
        {
            if (!hit.collider.CompareTag("Donut"))
            {
                PauseSequence();
            }
        }
        else
        {
            PauseSequence();
        }
    }

    private void OnDestroy()
    {
        if(triggerRef != null)
        {
            triggerRef.action.started -= HandleActionStarted;
            triggerRef.action.canceled -= HandleActionCanceled;
        }
    }

    void HandleActionStarted(InputAction.CallbackContext context)
    {
        if (controller.interactor.TryGetCurrent3DRaycastHit(out hit))
        {
            donutName = hit.collider.gameObject.name;

            if (controller.currentModel.CompareTag("Scalpel"))
            {
                if (donutName == "DonutIncision1")
                {
                    if (isSequenceAssigned == false)
                    {
                        float gaugeInterval = 12f / 12f;

                        moveSequence = DOTween.Sequence()
                            .Append(donutIncision1.transform.DOMove(donutIncision1.GetComponent<Donut>().targetPos, 12f).SetEase(Ease.Linear));

                        gaugeSequence = DOTween.Sequence()
                            .AppendInterval(gaugeInterval / 2f);
                        for (int i = 0; i < 12; i++)
                        {
                            int index = i;
                            gaugeSequence
                                .AppendCallback(() =>
                                {
                                    donutIncision1.GetComponent<Donut>().gauge[index].SetActive(true);
                                });
                            if (index < 11)
                            {
                                gaugeSequence
                                    .AppendInterval(gaugeInterval);
                            }
                        }
                        gaugeSequence
                            .AppendInterval(gaugeInterval / 2f)
                            .OnComplete(() =>
                            {
                                donutIncision1.SetActive(false);
                                donutIncision2.SetActive(true);
                                isSequenceAssigned = false;
                            });

                        isSequenceAssigned = true;
                        isSequenceRunning = true;
                    }
                    else
                    {
                        PlaySequence();
                    }
                }
                else if (donutName == "DonutIncision2")
                {

                }
                else if (donutName == "DonutIncision3")
                {

                }
            }
        }
    }

    void HandleActionCanceled(InputAction.CallbackContext context)
    {
        PauseSequence();
    }


    private void PlaySequence()
    {
        if (isSequenceAssigned==true)
        {
            if (isSequenceRunning == false)
            {
                moveSequence?.Play();
                gaugeSequence?.Play();
                isSequenceRunning = true;
            }
        }
    }

    private void PauseSequence()
    {
        if (isSequenceAssigned == true)
        {
            if (isSequenceRunning == true)
            {
                moveSequence?.Pause();
                gaugeSequence?.Pause();
                isSequenceRunning = false;
            }
        }
    }
}
