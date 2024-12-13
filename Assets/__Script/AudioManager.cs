using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get;  set; }  
    AudioSource[] myAudio;
    public Text UI;
    // Start is called before the first frame update

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);    
        }
        
    }
    void Start()
    {
        myAudio = GetComponents<AudioSource>();   
    }

    // Update is called once per frame
    public void PlayAudio(int index)
    {
        if(index == 0)
        {
            myAudio[0].Play();
        }
        else
        {
            myAudio[index].Play();
        }
                   
    }
}
