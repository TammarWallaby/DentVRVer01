/* XR Origin에 들어갈 스크립트
 * 플레이어의 현재 상태를 추적 할 용도
 * 다른 스크립트에서 Player.Instance.(CurrentState||ChangeState()||IsInState());로 사용하면 됨
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static Player Instance { get; private set; }

    // 열거형은 그대로 유지
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
        Waiting,
        WaitingComplete,
        HARemove,
        HARemoveComplete,
        AbutmentPlace,
        AbutmentPlaceComplete,
        CrownPlace,
        CrownPlaceComplete,
        Finish
    }

    // 현재 플레이어 상태
    public PlayerState CurrentState { get; private set; }

    // Awake 메서드에서 싱글톤 패턴 초기화
    private void Awake()
    {
        // 이미 인스턴스가 존재하면 현재 오브젝트 제거
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // 현재 인스턴스를 설정
        Instance = this;

        // 초기 상태 설정
        CurrentState = PlayerState.Start;
    }

    // 상태 변경 메서드
    public void ChangeState(PlayerState newState)
    {
        CurrentState = newState;
        Debug.Log($"Player state changed to: {newState}");
    }

    // 현재 상태 확인 메서드
    public bool IsInState(PlayerState state)
    {
        return CurrentState == state;
    }
}
