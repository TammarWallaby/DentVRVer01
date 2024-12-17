using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    public ParticleSystem flames;
    public ParticleSystem flamesSecondary;
    public ParticleSystem flamesDistortion;

    void OnEnable()
    {
        if (flames != null)
        {
            flames.Play();
        }
        if (flamesSecondary != null)
        {
            flamesSecondary.Play();
        }
        if (flamesDistortion != null)
        {
            flamesDistortion.Play();
        }
    }
}
