/* XR Origin의 Left Controller와 Right Controller에 들어갈 스크립트
 * 플레이어가 각 방에서 수행 할 수술 구현
 */


using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SurgeryManager : MonoBehaviour
{
    [Header("Main Setting")]
    public InputActionReference triggerRef;
    public XRController controller;


    [Header("Start")]
    public GameObject donutStart1;

    [Header("Anesthesia")]
    public GameObject donutAnesthesia1; // 도넛은 모두 씬에 있는 오브젝트를 넣음
    public GameObject syringe; // 프리팹 모델 넣어야 함!
    public GameObject syringeHandle; // 프리팹 모델 넣어야 함!

    [Header("Incision")]
    public GameObject donutIncision1;
    public GameObject donutIncision2;
    public GameObject donutIncision3;
    public GameObject gumIncisionOrigin;
    public GameObject gumIncision1;
    public GameObject gumIncision2;
    public GameObject gumIncision3;

    [Header("Elevation")]
    public GameObject donutElevation1;
    public GameObject donutElevation2;
    public GameObject gumElevationOrigin;
    public GameObject gumElevation1;
    public GameObject gumElevation2;

    [Header("Drill")]
    public GameObject donutDrill1;
    public GameObject donutDrill2;
    public GameObject donutDrill3;
    public GameObject drill2; // 프리팹 모델
    public GameObject drill3; // 프리팹
    public GameObject drill4; // 프
    public GameObject gumDrillOrigin;
    public GameObject gumDrill1;
    public GameObject gumDrill2;
    public GameObject gumDrill3;

    [Header("FixturePlace")]
    public GameObject donutFixturePlace1;
    public GameObject donutFixturePlace2;
    public GameObject fixture1; // 씬에 미리 배치해 둘 것

    [Header("HAPlace")]
    public GameObject donutHAPlace1;
    public GameObject donutHAPlace2;
    public GameObject healingAbutment1; //씬에 미리 배치

    [Header("Suture")] //모델링 실 개수 정해지면
    public GameObject donutSuture1;
    public GameObject donutSuture2;
    public GameObject donutSuture3;
    public GameObject donutSuture4;
    public GameObject donutSuture5;
    public GameObject donutSuture6;
    public GameObject gumSutureOrigin;
    public GameObject gumSuture1;
    public GameObject gumSuture2;
    public GameObject gumSuture3;
    public GameObject gumSuture4;
    public GameObject gumSuture5;
    public GameObject gumSuture6;

    [Header("Wait")] //Wait방 조건 정해지면
    public GameObject donutWait1;

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
        if (isSequenceRunning)
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

            //RoomStart
            if (Player.Instance.CurrentState == Player.PlayerState.Start)
            {
                if (donutName == "DonutStart1")
                {
                    donutStart1.SetActive(false);
                    Player.Instance.ChangeState(Player.PlayerState.StartComplete);
                }
            }

            //RoomAnesthesia
            if (Player.Instance.CurrentState == Player.PlayerState.Anesthesia)
            {
                if (controller.currentModel.CompareTag("Syringe"))
                {
                    if (donutName == "DonutAnesthesia1")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 5f / 12f;

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutAnesthesia1.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutAnesthesia1.SetActive(false);
                                    Player.Instance.ChangeState(Player.PlayerState.Incision);

                                    donutIncision1.SetActive(true);

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
                }
            }

            //RoomIncision
            if (Player.Instance.CurrentState == Player.PlayerState.Incision)
            {
                if (controller.currentModel.CompareTag("Scalpel"))
                {
                    if (donutName == "DonutIncision1")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 4f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutIncision1.transform.DOMove(donutIncision1.GetComponent<Donut>().targetPos, 4f).SetEase(Ease.Linear));

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
                                    gumIncisionOrigin.SetActive(false);
                                    gumIncision1.SetActive(true);

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
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 4f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutIncision2.transform.DOMove(donutIncision2.GetComponent<Donut>().targetPos, 4f).SetEase(Ease.Linear));

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutIncision2.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutIncision2.SetActive(false);
                                    donutIncision3.SetActive(true);
                                    gumIncision1.SetActive(false);
                                    gumIncision2.SetActive(true);

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
                    else if (donutName == "DonutIncision3")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 4f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutIncision3.transform.DOMove(donutIncision3.GetComponent<Donut>().targetPos, 4f).SetEase(Ease.Linear));

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutIncision3.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutIncision3.SetActive(false);
                                    gumIncision2.SetActive(false);
                                    gumIncision3.SetActive(true);
                                    Player.Instance.ChangeState(Player.PlayerState.Elevation);

                                    donutElevation1.SetActive(true);

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
                }
            }

            //RoomElevation
            if(Player.Instance.CurrentState==Player.PlayerState.Elevation)
            {
                if (controller.currentModel.CompareTag("Elevator"))
                {
                    if (donutName == "DonutElevation1")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 5f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutElevation1.transform.DOMove(donutElevation1.GetComponent<Donut>().targetPos, 5f).SetEase(Ease.Linear));

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutElevation1.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutElevation1.SetActive(false);
                                    donutElevation2.SetActive(true);
                                    gumElevationOrigin.SetActive(false);
                                    gumElevation1.SetActive(true);                                   

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
                    else if (donutName == "DonutElevation2")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 5f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutElevation2.transform.DOMove(donutElevation2.GetComponent<Donut>().targetPos, 5f).SetEase(Ease.Linear));

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutElevation2.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutElevation2.SetActive(false);
                                    gumElevation1.SetActive(false);
                                    gumElevation2.SetActive(true);
                                    Player.Instance.ChangeState(Player.PlayerState.Drill);

                                    donutDrill1.SetActive(true);

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
                }
            }

            //RoomDrill
            if(Player.Instance.CurrentState == Player.PlayerState.Drill)
            {
                if (controller.currentModel.CompareTag("HandpieceWithDrill2"))
                {
                    if(donutName=="DonutDrill1")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 5f / 12f;

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutDrill1.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutDrill1.SetActive(false);
                                    donutDrill2.SetActive(true);
                                    gumDrillOrigin.SetActive(false);
                                    gumDrill1.SetActive(true);

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
                }
                else if (controller.currentModel.CompareTag("HandpieceWithDrill3"))
                {
                    if (donutName == "DonutDrill2")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 5f / 12f;

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutDrill2.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutDrill2.SetActive(false);
                                    donutDrill3.SetActive(true);
                                    gumDrill1.SetActive(false);
                                    gumDrill2.SetActive(true);

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
                }
                else if (controller.currentModel.CompareTag("HandpieceWithDrill4"))
                {
                    if (donutName == "DonutDrill3")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 5f / 12f;

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutDrill3.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutDrill3.SetActive(false);                      
                                    gumDrill2.SetActive(false);
                                    gumDrill3.SetActive(true);
                                    Player.Instance.ChangeState(Player.PlayerState.FixturePlace);

                                    donutFixturePlace1.SetActive(true);

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
                }
            }

            //RoomFixturePlace
            if (Player.Instance.CurrentState == Player.PlayerState.FixturePlace)
            {
                if (controller.currentModel.CompareTag("Fixture"))
                {
                    if (donutName == "DonutFixturePlace1")
                    {
                        controller.UpdateControllerModel(controller.controllerM);
                        donutFixturePlace1.SetActive(false);
                        donutFixturePlace2.SetActive(true);
                        fixture1.SetActive(true);
                    }
                }
                else if (controller.currentModel.CompareTag("Handpiece2"))
                {
                    if (donutName == "DonutFixturePlace2")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 6f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutFixturePlace2.transform.DOMove(donutFixturePlace2.GetComponent<Donut>().targetPos, 6f).SetEase(Ease.Linear))
                                .Join(fixture1.transform.DOMove(new Vector3(-28.39375f, 1.012892f, -6.606135f), 6f).SetEase(Ease.Linear));

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutFixturePlace2.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutFixturePlace2.SetActive(false);
                                    Player.Instance.ChangeState(Player.PlayerState.HAPlace);

                                    donutHAPlace1.SetActive(true);

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
                }
            }

            //RoomHAPlace
            if (Player.Instance.CurrentState == Player.PlayerState.HAPlace)
            {
                if (controller.currentModel.CompareTag("HealingAbutment"))
                {
                    if (donutName == "DonutHAPlace1")
                    {
                        controller.UpdateControllerModel(controller.controllerM);
                        donutHAPlace1.SetActive(false);
                        donutHAPlace2.SetActive(true);
                        healingAbutment1.SetActive(true);
                    }
                }
                else if (controller.currentModel.CompareTag("Driver"))
                {
                    if (donutName == "DonutHAPlace2")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 6f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutHAPlace2.transform.DOMove(donutHAPlace2.GetComponent<Donut>().targetPos, 6f).SetEase(Ease.Linear))
                                .Join(healingAbutment1.transform.DOMove(new Vector3(-28.39375f, 1.012892f, -6.606135f), 6f).SetEase(Ease.Linear));

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutHAPlace2.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutHAPlace2.SetActive(false);
                                    Player.Instance.ChangeState(Player.PlayerState.HAPlaceComplete);

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
                }
            }

            //RoomSuture
            if (Player.Instance.CurrentState == Player.PlayerState.Suture)
            {
                if (controller.currentModel.CompareTag("Needle"))
                {
                    if (donutName == "DonutSuture1")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 2f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutSuture1.transform.DOMove(donutSuture1.GetComponent<Donut>().targetPos, 2f).SetEase(Ease.Linear));

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutSuture1.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutSuture1.SetActive(false);
                                    donutSuture2.SetActive(true);
                                    gumSutureOrigin.SetActive(false);
                                    gumSuture1.SetActive(true);

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
                    else if (donutName == "DonutSuture2")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 2f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutSuture2.transform.DOMove(donutSuture2.GetComponent<Donut>().targetPos, 2f).SetEase(Ease.Linear));

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutSuture2.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutSuture2.SetActive(false);
                                    donutSuture3.SetActive(true);
                                    gumSuture1.SetActive(false);
                                    gumSuture2.SetActive(true);

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
                    else if (donutName == "DonutSuture3")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 2f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutSuture3.transform.DOMove(donutSuture3.GetComponent<Donut>().targetPos, 2f).SetEase(Ease.Linear));

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutSuture3.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutSuture3.SetActive(false);
                                    donutSuture4.SetActive(true);
                                    gumSuture2.SetActive(false);
                                    gumSuture3.SetActive(true);

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
                    else if (donutName == "DonutSuture4")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 2f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutSuture4.transform.DOMove(donutSuture4.GetComponent<Donut>().targetPos, 2f).SetEase(Ease.Linear));

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutSuture4.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutSuture4.SetActive(false);
                                    donutSuture5.SetActive(true);
                                    gumSuture3.SetActive(false);
                                    gumSuture4.SetActive(true);

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
                    else if (donutName == "DonutSuture5")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 2f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutSuture5.transform.DOMove(donutSuture5.GetComponent<Donut>().targetPos, 2f).SetEase(Ease.Linear));

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutSuture5.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutSuture5.SetActive(false);
                                    donutSuture6.SetActive(true);
                                    gumSuture4.SetActive(false);
                                    gumSuture5.SetActive(true);

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
                    else if (donutName == "DonutSuture6")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 2f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutSuture6.transform.DOMove(donutSuture6.GetComponent<Donut>().targetPos, 2f).SetEase(Ease.Linear));

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutSuture6.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutSuture6.SetActive(false);
                                    gumSuture5.SetActive(false);
                                    gumSuture6.SetActive(true);
                                    Player.Instance.ChangeState(Player.PlayerState.SutureComplete);
                                    donutWait1.SetActive(true); // 임시

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
                }
            }

            //RoomWait
            if (Player.Instance.CurrentState == Player.PlayerState.Wait)
            {
                if (donutName == "DonutWait1")
                {
                    donutWait1.SetActive(false);
                    Player.Instance.ChangeState(Player.PlayerState.WaitComplete);
                }
            }

            //RoomHARemove
            if (Player.Instance.CurrentState == Player.PlayerState.HARemove)
            {
                if (controller.currentModel.CompareTag("Driver2"))
                {
                    if (donutName == "DonutHARemove1")
                    {
                        if (isSequenceAssigned == false)
                        {
                            float gaugeInterval = 6f / 12f;

                            moveSequence = DOTween.Sequence()
                                .Append(donutHARemove1.transform.DOMove(donutHARemove1.GetComponent<Donut>().targetPos, 6f).SetEase(Ease.Linear))
                                .Join(healingAbutment2.transform.DOMove(new Vector3(253.0307f, 1.0961f, -6.5761f), 6f).SetEase(Ease.Linear));

                            gaugeSequence = DOTween.Sequence()
                                .AppendInterval(gaugeInterval / 2f);
                            for (int i = 0; i < 12; i++)
                            {
                                int index = i;
                                gaugeSequence
                                    .AppendCallback(() =>
                                    {
                                        donutHARemove1.GetComponent<Donut>().gauge[index].SetActive(true);
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
                                    donutHARemove1.SetActive(false);
                                    healingAbutment2.SetActive(false);
                                    Player.Instance.ChangeState(Player.PlayerState.HARemoveComplete);

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
                }
            }

            //RoomAbutmentPlace
            if (Player.Instance.CurrentState == Player.PlayerState.AbutmentPlace)
            {

            }

            //RoomCrownPlace
            if (Player.Instance.CurrentState == Player.PlayerState.CrownPlace)
            {

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

