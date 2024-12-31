/* UI -> ChristmasButton에 들어갈 간단한 스크립트
 * 회전 담당
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasButton : MonoBehaviour
{
    public float rotationSpeed = 20f; // 눈송이 회전 속도값 조절

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
