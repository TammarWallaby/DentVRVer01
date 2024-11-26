/* 각 방의 Door(Empty)에 넣을 스크립트
 * 문이 여닫히는 조건 설정 및 문이 여닫히는 시퀀스 구현
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public bool doorUsed;

    public GameObject door1Arrow;
    public GameObject door1LeftDoor;
    public GameObject door1RightDoor;

    public GameObject door2Arrow;
    public GameObject door2LeftDoor;
    public GameObject door2RightDoor;

    private void Start()
    {
        doorUsed = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}
