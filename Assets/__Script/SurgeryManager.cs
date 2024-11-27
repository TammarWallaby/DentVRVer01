/* XR Origin의 Left Controller와 Right Controller에 들어갈 스크립트
 * 플레이어가 각 방에서 수행 할 수술 구현
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SurgeryManager : MonoBehaviour
{
    [Header("Main Setting")]
    public InputActionReference selectRef;
    public XRController controller;
    public Player player;

    //[Header("Starter")]

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
    


    private void Awake()
    {
        
    }
}
