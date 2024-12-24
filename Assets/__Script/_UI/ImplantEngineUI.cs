using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ImplantEngineUI : MonoBehaviour
{
    public Text SpeedValueText;      

    public Text TorqueValueText;      


    private int[] speedValues = { 0, 50, 600, 1200 }; // �� �迭
    public int speedCurrentIndex = 0;               // ���� �迭 �ε���

    private int[] torqueValues = { 0, 30, 40, 50 }; // �� �迭
    public int torqueCurrentIndex = 0;               // ���� �迭 �ε���

    void Start()
    {
        // �ʱ� �ؽ�Ʈ ����
        UpdateText();
    }

    // �� ����
    public void SpeedIncreaseValue()
    {
        if (speedCurrentIndex < speedValues.Length - 1)
        {
            speedCurrentIndex++;
            UpdateText();
        }
    }

    // �� ����
    public void SpeedDecreaseValue()
    {
        if (speedCurrentIndex > 0)
        {
            speedCurrentIndex--;
            UpdateText();
        }
    }
    public void TorqueIncreaseValue()
    {
        if (torqueCurrentIndex < torqueValues.Length - 1)
        {
            torqueCurrentIndex++;
            UpdateText();
        }
    }

    // �� ����
    public void TorqueDecreaseValue()
    {
        if (torqueCurrentIndex > 0)
        {
            torqueCurrentIndex--;
            UpdateText();
        }
    }
    // �ؽ�Ʈ ������Ʈ
    void UpdateText()
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
