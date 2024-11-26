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

    public Player.PlayerState doorOpenState; // 문이 열릴 조건, 설정 필수

    public GameObject door1Arrow;
    public GameObject door1LeftDoor;
    public GameObject door1RightDoor;

    public GameObject door2Arrow;
    public GameObject door2LeftDoor;
    public GameObject door2RightDoor;

    private void Start()
    {
        Player.Instance.OnStateChanged += DoorOpen;

        doorOpened = false;
        doorClosed = false;
    }

    private void OnDisable()
    {
        Player.Instance.OnStateChanged -= DoorOpen;
    }

    //private void Update()
    //{
    //    if (Player.Instance.CurrentState == doorOpenState)
    //    {

    //    }
    //}

    void DoorOpen(Player.PlayerState currentState)
    {
        if (doorOpened==false)
        {
            if (currentState == doorOpenState)
            {
                Sequence DoorOpenSequence = DOTween.Sequence()
                    .AppendCallback(() =>
                    {
                        doorOpened = true;
                        Player.Instance.OnStateChanged -= DoorOpen;
                    })
                    .Append(door1Arrow.transform.DOLocalMoveX(-0.17f, 1f)).SetRelative()
                    .Join(door2Arrow.transform.DOLocalMoveX(0.17f, 1f)).SetRelative()
                    .Append(door1Arrow.transform.DOLocalMoveY(1f, 2f)).SetRelative()
                    .Join(door2Arrow.transform.DOLocalMoveY(1f, 2f)).SetRelative()
                    .Join(door1LeftDoor.transform.DOLocalMoveZ(2.4f, 2f)).SetRelative()
                    .Join(door1RightDoor.transform.DOLocalMoveZ(-2.4f, 2f)).SetRelative()
                    .Join(door2LeftDoor.transform.DOLocalMoveZ(2.4f, 2f)).SetRelative()
                    .Join(door2RightDoor.transform.DOLocalMoveZ(-2.4f, 2f)).SetRelative()
                    .Append(door1Arrow.transform.DOLocalMoveX(0.08f, 1f)).SetRelative()
                    .Join(door2Arrow.transform.DOLocalMoveX(-0.08f, 1f)).SetRelative();
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
                    })
                    .Append(door1Arrow.transform.DOLocalMoveX(-0.08f, 1f)).SetRelative()
                    .Join(door2Arrow.transform.DOLocalMoveX(0.08f, 1f)).SetRelative()
                    .Append(door1Arrow.transform.DOLocalMoveY(-1f, 2f)).SetRelative()
                    .Join(door2Arrow.transform.DOLocalMoveY(-1f, 2f)).SetRelative()
                    .Join(door1LeftDoor.transform.DOLocalMoveZ(-2.4f, 2f)).SetRelative()
                    .Join(door1RightDoor.transform.DOLocalMoveZ(2.4f, 2f)).SetRelative()
                    .Join(door2LeftDoor.transform.DOLocalMoveZ(-2.4f, 2f)).SetRelative()
                    .Join(door2RightDoor.transform.DOLocalMoveZ(2.4f, 2f)).SetRelative()
                    .Append(door1Arrow.transform.DOLocalMoveX(0.17f, 1f)).SetRelative()
                    .Join(door2Arrow.transform.DOLocalMoveX(-0.17f, 1f)).SetRelative();
            }
        }
    }
}
