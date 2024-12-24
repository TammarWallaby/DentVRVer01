using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DrilMachineUI : MonoBehaviour
{
    public Text SpeedvalueText;      

    public Text TorquevalueText;      


    private int[] speedvalues = { 0, 50, 600, 1200 }; // 값 배열
    private int speedcurrentIndex = 0;               // 현재 배열 인덱스

    private int[] torquevalues = { 0, 30, 40, 50 }; // 값 배열
    private int torquecurrentIndex = 0;               // 현재 배열 인덱스

    void Start()
    {
        // 초기 텍스트 설정
        UpdateText();
    }

    // 값 증가
    public void SpeedIncreaseValue()
    {
        if (speedcurrentIndex < speedvalues.Length - 1)
        {
            speedcurrentIndex++;
            UpdateText();
        }
    }

    // 값 감소
    public void SpeedDecreaseValue()
    {
        if (speedcurrentIndex > 0)
        {
            speedcurrentIndex--;
            UpdateText();
        }
    }
    public void TorqueIncreaseValue()
    {
        if (torquecurrentIndex < torquevalues.Length - 1)
        {
            torquecurrentIndex++;
            UpdateText();
        }
    }

    // 값 감소
    public void TorqueDecreaseValue()
    {
        if (torquecurrentIndex > 0)
        {
            torquecurrentIndex--;
            UpdateText();
        }
    }
    // 텍스트 업데이트
    void UpdateText()
    {
        if (SpeedvalueText != null)
        {
            SpeedvalueText.text = speedvalues[speedcurrentIndex].ToString();
        }

        if (TorquevalueText != null)
        {
            TorquevalueText.text = torquevalues[torquecurrentIndex].ToString();
        }
    }
}
