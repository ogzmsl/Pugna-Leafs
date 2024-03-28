using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public AudioSource introMusic;
    public AudioSource mainMusic;

    public Animator WASD;
    public Animator Space;


    void Start()
    {
        
        introMusic.Play();
        
        Invoke("PlayMainMusic", introMusic.clip.length);
        Invoke("playWASD", introMusic.clip.length);
        Invoke("playWASDend", introMusic.clip.length+11);
    }

    void PlayMainMusic()
    {
      
        mainMusic.Play();
    }



    void playWASD()
    {
        Space.SetBool("SpaceBool", true);
        WASD.SetBool("WASD", true);
    }
     void playWASDend()
    {
        Space.SetBool("SpaceBool", false);
        WASD.SetBool("WASD", false);
    }


}