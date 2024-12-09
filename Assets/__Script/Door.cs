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

    public Material materialDoorLightAtoBC;
    public Material materialDoorLightBCtoA;

    private void Start()
    {
        Player.Instance.OnStateChanged += DoorOpen;

        doorOpened = false;
        doorClosed = true;
        colliderA.enabled = false;
        colliderBC.enabled = true;
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
                        //문 소리
                        materialDoorLightAtoBC.color = Color.green;
                        materialDoorLightBCtoA.color = Color.green;
                    })
                    .AppendInterval(1f)
                    .Append(door1LeftDoor.transform.DOLocalMoveZ(-3, 2f))
                    .Join(door1RightDoor.transform.DOLocalMoveZ(3, 2f))
                    .Join(door2LeftDoor.transform.DOLocalMoveZ(-3, 2f))
                    .Join(door2RightDoor.transform.DOLocalMoveZ(3, 2f));
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
                        //문 소리
                    })
                    .Append(door1LeftDoor.transform.DOLocalMoveZ(3, 2f))
                    .Join(door1RightDoor.transform.DOLocalMoveZ(-3, 2f))
                    .Join(door2LeftDoor.transform.DOLocalMoveZ(3, 2f))
                    .Join(door2RightDoor.transform.DOLocalMoveZ(-3, 2f))
                    .AppendCallback(() =>
                    {
                        materialDoorLightAtoBC.color = Color.red;
                        materialDoorLightBCtoA.color = Color.red;
                    });
            }
        }
    }
}
