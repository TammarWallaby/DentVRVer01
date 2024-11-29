/* XR Origin의 Left Controller와 Right Controller에 들어갈 스크립트
 * 플레이어가 각 방에서 수행 할 수술 구현
 */


using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SurgeryManager : MonoBehaviour
{
    [Header("Main Setting")]
    public InputActionReference triggerRef;
    public XRController controller;
    public Player player;

    public RaycastHit hit;
    public string donutName;


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


    private Sequence gaugeSequence;
    private Sequence moveSequence;
    private bool isSequenceRunning = false;

    private void Awake()
    {
        if (triggerRef != null)
        {
            triggerRef.action.started += HandleActionStarted;
            triggerRef.action.performed += HandleActionPerformed;
            triggerRef.action.canceled += HandleActionCanceled;
        }
    }

    private void OnDestroy()
    {
        if(triggerRef != null)
        {
            triggerRef.action.started -= HandleActionStarted;
            triggerRef.action.performed -= HandleActionPerformed;
            triggerRef.action.canceled -= HandleActionCanceled;
        }
    }

    void HandleActionStarted(InputAction.CallbackContext context)
    {
        if(controller.interactor.TryGetCurrent3DRaycastHit(out hit))
        {
            donutName = hit.collider.gameObject.name;

            if (controller.currentModel.CompareTag("Scalpel"))
            {
                if (donutName == "DonutIncision1")
                {
                    if (isSequenceRunning == false)
                    {
                        float gaugeInterval= 4f/12f;
                        moveSequence = DOTween.Sequence()
                            .Append(donutIncision1.transform.DOMove(donutIncision1.GetComponent<Donut>().targetPos, 4f).SetEase(Ease.Linear));

                        gaugeSequence = DOTween.Sequence()
                            .AppendInterval(gaugeInterval/2f)                          
                            .AppendCallback(() =>
                            {
                                donutIncision1.GetComponent<Donut>().gauge[0].SetActive(true);
                            })
                            .AppendInterval(gaugeInterval)
                            .AppendCallback(() =>
                            {
                                donutIncision1.GetComponent<Donut>().gauge[1].SetActive(true);
                            })
                            .AppendInterval(gaugeInterval)
                            .AppendCallback(() =>
                            {
                                donutIncision1.GetComponent<Donut>().gauge[2].SetActive(true);
                            })
                            .AppendInterval(gaugeInterval)
                            .AppendCallback(() =>
                            {
                                donutIncision1.GetComponent<Donut>().gauge[3].SetActive(true);
                            })
                            .AppendInterval(gaugeInterval)
                            .AppendCallback(() =>
                            {
                                donutIncision1.GetComponent<Donut>().gauge[4].SetActive(true);
                            })
                            .AppendInterval(gaugeInterval)
                            .AppendCallback(() =>
                            {
                                donutIncision1.GetComponent<Donut>().gauge[5].SetActive(true);
                            })
                            .AppendInterval(gaugeInterval)
                            .AppendCallback(() =>
                            {
                                donutIncision1.GetComponent<Donut>().gauge[6].SetActive(true);
                            })
                            .AppendInterval(gaugeInterval)
                            .AppendCallback(() =>
                            {
                                donutIncision1.GetComponent<Donut>().gauge[7].SetActive(true);
                            })
                            .AppendInterval(gaugeInterval)
                            .AppendCallback(() =>
                            {
                                donutIncision1.GetComponent<Donut>().gauge[8].SetActive(true);
                            })
                            .AppendInterval(gaugeInterval)
                            .AppendCallback(() =>
                            {
                                donutIncision1.GetComponent<Donut>().gauge[9].SetActive(true);
                            })
                            .AppendInterval(gaugeInterval)
                            .AppendCallback(() =>
                            {
                                donutIncision1.GetComponent<Donut>().gauge[10].SetActive(true);
                            })
                            .AppendInterval(gaugeInterval)
                            .AppendCallback(() =>
                            {
                                donutIncision1.GetComponent<Donut>().gauge[11].SetActive(true);
                            })
                            .AppendInterval(gaugeInterval/2f)
                            .OnComplete(() =>
                            {
                                donutIncision1.SetActive(false);
                                donutIncision2.SetActive(true);
                                isSequenceRunning = false;
                            });

                        isSequenceRunning = true;
                    }
                    else
                    {
                        moveSequence.Play();
                        gaugeSequence.Play();
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

    void HandleActionPerformed(InputAction.CallbackContext context)
    {

    }

    void HandleActionCanceled(InputAction.CallbackContext context)
    {
        if (isSequenceRunning == true)
        {
            moveSequence.Pause();
            gaugeSequence.Pause();
        }
    }
}
