using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    AudioSource[] myAudio;
    public Text UI;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponents<AudioSource>();
   
    }

    // Update is called once per frame
    void Update()
    {
        myAudio[0].Play(); 
    }
}
