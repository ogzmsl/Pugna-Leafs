using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorManager : MonoBehaviour
{
    public Animator animator;
    int horizontal;//animasyon blend tree kontolü
    int vertical;//animasyon blend tree kontolü
    
        
    public void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal"); //performans arttýrýldý input alýndý
        vertical = Animator.StringToHash("Vertical");
        

    }

    public void PlayTargetAnimation(string targetAnimation,bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
       
        if (targetAnimation == "Jump")
        {
            animator.SetBool("isJumping", isInteracting);
        }
    }


    //bu fonksiyon iki parametre alýr ve animasyonlar için float olan blend tree deðiþikliklerini yapar
    public void UptadeAnimatorValues(float horizontalmovement,float verticalmovement, bool isspriting)
    {



        float snappedHorizontal;



        float snappedVertical;



        if (horizontalmovement > 0 && horizontalmovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalmovement > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalmovement < 0 && horizontalmovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalmovement < -0.55f)
        {
            snappedHorizontal = -1f;
        }
        else
        {
            snappedHorizontal = 0;
        }





        if (verticalmovement > 0 && verticalmovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalmovement > 0.55f)
        {
            snappedVertical = 1;
        }
        else if (verticalmovement < 0 && verticalmovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalmovement < -0.55f)
        {
            snappedVertical = -1f;
        }
        else
        {
            snappedVertical = 0;
        }

        if (isspriting)
        {
            snappedHorizontal = horizontalmovement;
            snappedVertical = 2;

        }
        
       



        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
