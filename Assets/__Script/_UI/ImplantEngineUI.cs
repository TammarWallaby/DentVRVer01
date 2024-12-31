/*
 * ImplantEngine 안에 Engine 오브젝트에 들어가있음
 * 드릴 RPM, 토크 업다운 설정
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ImplantEngineUI : MonoBehaviour
{
    public Text SpeedValueText;      // RPM 값 테스트

    public Text TorqueValueText;     // 토크 값 텍스트


    private int[] speedValues = { 0, 50, 600, 1200 }; // 값 배열
    public int speedCurrentIndex = 0;               // 현재 배열 인덱스

    private int[] torqueValues = { 0, 30, 40, 50 }; // 값 배열
    public int torqueCurrentIndex = 0;               // 현재 배열 인덱스

    void Start()
    {
        // 초기 텍스트 설정
        UpdateText();
    }

    // 값 증가
    public void SpeedIncreaseValue() //후위 증가 연산자 활용
    {
        if (speedCurrentIndex < speedValues.Length - 1)
        {
            speedCurrentIndex++;
            UpdateText();
        }
    }

    // 값 감소
    public void SpeedDecreaseValue() //후위 감소 연산자 활용
    {
        if (speedCurrentIndex > 0)
        {
            speedCurrentIndex--;
            UpdateText();
        }
    }
    public void TorqueIncreaseValue() //후위 증가 연산자 활용
    {
        if (torqueCurrentIndex < torqueValues.Length - 1)
        {
            torqueCurrentIndex++;
            UpdateText();
        }
    }

    // 값 감소
    public void TorqueDecreaseValue() //후위 감소 연산자 활용
    {
        if (torqueCurrentIndex > 0)
        {
            torqueCurrentIndex--;
            UpdateText();
        }
    }
    // 텍스트 업데이트
    void UpdateText() // 배열값 텍스트로 변경
    {
        if (SpeedValueText != null)
        {
            SpeedValueText.text = speedValues[speedCurrentIndex].ToString();
        }

        if (TorqueValueText != null)
        {
            TorqueValueText.text = torqueValues[torqueCurrentIndex].ToString();
        }
    }
}
