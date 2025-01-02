/* XR Origin에 들어갈 스크립트
 * 플레이어의 현재 상태를 추적 할 용도
 * 다른 스크립트에서 Player.Instance.(CurrentState||ChangeState()||IsInState());로 사용하면 됨
 */



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public enum PlayerState
    {
        Start, // 시작
        StartComplete,
        Anesthesia, // 마취
        AnesthesiaComplete,
        Incision, // 절개
        IncisionComplete,
        Elevation, // 박리
        ElevationComplete,
        Drill, // 드릴
        DrillComplete,
        FixturePlace, // 픽스쳐 식립
        FixturePlaceComplete,
        HAPlace, // 힐링 어버트먼트 식립 및 박리 덮기
        HAPlaceComplete,
        Suture, // 봉합
        SutureComplete,
        Wait, // 대기
        WaitComplete,
        HARemove, // 힐링 어버트먼트 제거
        HARemoveComplete,
        AbutmentPlace, // 어버트먼트 식립
        AbutmentPlaceComplete,
        CrownPlace, // 크라운 씌우기
        CrownPlaceComplete,
        Finish
    }

    // 현재 플레이어 상태
    [SerializeField] private PlayerState currentState;
    public PlayerState CurrentState
    {
        get { return currentState; }
        private set { currentState = value; }
    }

    public event Action<PlayerState> OnStateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        currentState = PlayerState.Start;
    }

    // 상태 변경 메서드
    public void ChangeState(PlayerState newState)
    {
        currentState = newState;
        OnStateChanged?.Invoke(CurrentState);
    }

    // 현재 상태 확인 메서드
    public bool IsInState(PlayerState state)
    {
        return currentState == state;
    }
}
