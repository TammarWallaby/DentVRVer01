using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DrilMachineUI : MonoBehaviour
{
    public Text SpeedvalueText;      

    public Text TorquevalueText;      


    private int[] speedvalues = { 0, 50, 600, 1200 }; // �� �迭
    private int speedcurrentIndex = 0;               // ���� �迭 �ε���

    private int[] torquevalues = { 0, 30, 40, 50 }; // �� �迭
    private int torquecurrentIndex = 0;               // ���� �迭 �ε���

    void Start()
    {
        // �ʱ� �ؽ�Ʈ ����
        UpdateText();
    }

    // �� ����
    public void SpeedIncreaseValue()
    {
        if (speedcurrentIndex < speedvalues.Length - 1)
        {
            speedcurrentIndex++;
            UpdateText();
        }
    }

    // �� ����
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

    // �� ����
    public void TorqueDecreaseValue()
    {
        if (torquecurrentIndex > 0)
        {
            torquecurrentIndex--;
            UpdateText();
        }
    }
    // �ؽ�Ʈ ������Ʈ
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
