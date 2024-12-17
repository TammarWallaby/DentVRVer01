/* 각 방의 Door(Empty)에 넣을 스크립트
 * 문이 여닫히는 조건 설정 및 문이 여닫히는 시퀀스 구현
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public bool doorOpened;
    public bool doorClosed;

    public Player.PlayerState doorOpenState1; // 문이 열릴 조건, 설정 필수
    public Player.PlayerState doorOpenState2; // 문이 열릴 조건2, 설정 필수

    public GameObject door1LeftDoor;
    public GameObject door1RightDoor;

    public GameObject door2LeftDoor;
    public GameObject door2RightDoor;

    public BoxCollider colliderA;
    public BoxCollider colliderBC;

    public Material materialDoorLight;

    private void Start()
    {
        Player.Instance.OnStateChanged += DoorOpen;

        doorOpened = false;
        doorClosed = true;
        colliderA.enabled = false;
        colliderBC.enabled = true;

        materialDoorLight.color = Color.red;
        materialDoorLight.SetColor("_EmissionColor", new Color(100 / 255f, 0, 0));
    }

    private void OnDisable()
    {
        Player.Instance.OnStateChanged -= DoorOpen;
    }

    void DoorOpen(Player.PlayerState currentState)
    {
        if (doorOpened==false)
        {
            if (currentState == doorOpenState1 || currentState == doorOpenState2)
            {
                Sequence DoorOpenSequence = DOTween.Sequence()
                    .AppendCallback(() =>
                    {
                        doorOpened = true;
                        doorClosed = false;

                        materialDoorLight.color = Color.green;
                        materialDoorLight.SetColor("_EmissionColor", new Color(0, 100 / 255f, 0));
                    })
                    .AppendInterval(1f)
                    .Append(door1LeftDoor.transform.DOLocalMoveZ(-3, 2f).SetRelative())
                    .Join(door1RightDoor.transform.DOLocalMoveZ(3, 2f).SetRelative())
                    .Join(door2LeftDoor.transform.DOLocalMoveZ(-3, 2f).SetRelative())
                    .Join(door2RightDoor.transform.DOLocalMoveZ(3, 2f).SetRelative());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(doorClosed==false)
        {
            if (other.CompareTag("Player"))
            {
                Sequence DoorCloseSequence = DOTween.Sequence()
                    .AppendCallback(() =>
                    {
                        doorClosed = true;
                        doorOpened = false;
                        colliderA.enabled = !colliderA.enabled;
                        colliderBC.enabled = !colliderBC.enabled;
                    })
                    .Append(door1LeftDoor.transform.DOLocalMoveZ(3, 2f).SetRelative())
                    .Join(door1RightDoor.transform.DOLocalMoveZ(-3, 2f).SetRelative())
                    .Join(door2LeftDoor.transform.DOLocalMoveZ(3, 2f).SetRelative())
                    .Join(door2RightDoor.transform.DOLocalMoveZ(-3, 2f).SetRelative())
                    .AppendCallback(() =>
                    {
                        materialDoorLight.color = Color.red;
                        materialDoorLight.SetColor("_EmissionColor", new Color(100 / 255f, 0, 0));
                    });

                if (Player.Instance.CurrentState == Player.PlayerState.StartComplete)
                {
                    Player.Instance.ChangeState(Player.PlayerState.Anesthesia);
                }
                else if (Player.Instance.CurrentState == Player.PlayerState.SutureComplete)
                {
                    Player.Instance.ChangeState(Player.PlayerState.Wait);
                }
                else if (Player.Instance.CurrentState == Player.PlayerState.WaitComplete)
                {
                    Player.Instance.ChangeState(Player.PlayerState.HARemove);
                }
                else if (Player.Instance.CurrentState == Player.PlayerState.CrownPlaceComplete)
                {
                    Player.Instance.ChangeState(Player.PlayerState.Finish);
                }
            }
        }
    }
}
