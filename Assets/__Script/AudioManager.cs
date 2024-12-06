using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
<<<<<<< Updated upstream
    private AudioSource myAudio;
=======
    AudioSource myAudio;
>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
<<<<<<< Updated upstream
        myAudio.Play();
=======
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        
=======
        myAudio.Play();
>>>>>>> Stashed changes
    }
}
