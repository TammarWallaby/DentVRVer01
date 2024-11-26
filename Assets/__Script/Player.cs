/* XR Origin�� �� ��ũ��Ʈ
 * �÷��̾��� ���� ���¸� ���� �� �뵵
 * �ٸ� ��ũ��Ʈ���� Player.Instance.(CurrentState||ChangeState()||IsInState());�� ����ϸ� ��
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static Player Instance { get; private set; }

    // �������� �״�� ����
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

    // ���� �÷��̾� ����
    public PlayerState CurrentState { get; private set; }

    // Awake �޼��忡�� �̱��� ���� �ʱ�ȭ
    private void Awake()
    {
        // �̹� �ν��Ͻ��� �����ϸ� ���� ������Ʈ ����
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // ���� �ν��Ͻ��� ����
        Instance = this;

        // �ʱ� ���� ����
        CurrentState = PlayerState.Start;
    }

    // ���� ���� �޼���
    public void ChangeState(PlayerState newState)
    {
        CurrentState = newState;
        Debug.Log($"Player state changed to: {newState}");
    }

    // ���� ���� Ȯ�� �޼���
    public bool IsInState(PlayerState state)
    {
        return CurrentState == state;
    }
}
