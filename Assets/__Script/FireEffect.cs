/* 크리스마스팩 Fire에 들어갈 스크립트
 * 크리스마스팩 벽난로 불 파티클
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    public ParticleSystem flamesDistortion;

    void OnEnable()
    {
        if (flamesDistortion != null)
        {
            flamesDistortion.Play();
        }
    }
}
