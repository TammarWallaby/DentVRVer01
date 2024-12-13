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
        Start,
        StartComplete,
        Anesthesia,
        AnesthesiaComplete,
        Incision,
        IncisionComplete,
        Elevation,
        ElevationComplete,
        Drill,
        DrillComplete,
        FixturePlace,
        FixturePlaceComplete,
        HAPlace,
        HAPlaceComplete,
        Suture,
        SutureComplete,
        Wait,
        WaitComplete,
        HARemove,
        HARemoveComplete,
        AbutmentPlace,
        AbutmentPlaceComplete,
        CrownPlace,
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
