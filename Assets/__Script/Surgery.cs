/* (미정)에 들어갈 스크립트
 * 플레이어가 각 방에서 수행 할 수술 구현
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Surgery : MonoBehaviour
{
    public InputActionReference aButtonRef;

    public Player.PlayerState CurrentPlayerState;

    private void Awake()
    {
        if (aButtonRef != null)
        {
            aButtonRef.action.started += AddState;
        }
    }

    private void Start()
    {
        CurrentPlayerState = 0;
    }

    void AddState(InputAction.CallbackContext obj)
    {
        CurrentPlayerState++;
        Player.Instance.ChangeState(CurrentPlayerState);
    }
}
