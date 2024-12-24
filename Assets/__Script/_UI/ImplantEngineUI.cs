using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ImplantEngineUI : MonoBehaviour
{
    public Text SpeedvalueText;      

    public Text TorquevalueText;      


    private int[] speedvalues = { 0, 50, 600, 1200 }; // �� �迭
    public int speedCurrentIndex = 0;               // ���� �迭 �ε���

    private int[] torquevalues = { 0, 30, 40, 50 }; // �� �迭
    public int torqueCurrentIndex = 0;               // ���� �迭 �ε���

    void Start()
    {
        // �ʱ� �ؽ�Ʈ ����
        UpdateText();
    }

    // �� ����
    public void SpeedIncreaseValue()
    {
        if (speedCurrentIndex < speedvalues.Length - 1)
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
        if (torqueCurrentIndex < torquevalues.Length - 1)
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
        if (SpeedvalueText != null)
        {
            SpeedvalueText.text = speedvalues[speedCurrentIndex].ToString();
        }

        if (TorquevalueText != null)
        {
            TorquevalueText.text = torquevalues[torqueCurrentIndex].ToString();
        }
    }
}
