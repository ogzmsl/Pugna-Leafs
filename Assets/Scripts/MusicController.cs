using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public AudioSource introMusic;
    public AudioSource mainMusic;

    public Animator WASD;
    public Animator Space;
    public Animator FirstStep;
    //public ArrowDestroy arrow;
    public Animator MouseLeft;

    public GolemTetikleme tetikleme;


    void Start()
    {
        
        introMusic.Play();
        
        Invoke("PlayMainMusic", introMusic.clip.length);
        Invoke("playWASD", introMusic.clip.length);
        Invoke("playWASDend", introMusic.clip.length+8);
        
     

    }

    private void Update()
    {
        if (tetikleme.GolemTetiklemeBool)
        {

            mainMusic.Stop();

        }
    }





    void PlayMainMusic()
    {
      
        mainMusic.Play();
    }



    void playWASD()
    {
        Space.SetBool("SpaceBool", true);
        WASD.SetBool("WASD", true);
        FirstStep.SetBool("firstStep", true);
    }
     void playWASDend()
    {
        Space.SetBool("SpaceBool", false);
        WASD.SetBool("WASD", false);

        FirstStep.SetBool("firstStep", false);

    }



    void playMouseLeft()
    {
        MouseLeft.SetBool("MouseLeft", true);
    } 
    void playMouseLefEndt()
    {
        MouseLeft.SetBool("MouseLeft", false);
    }
       
}