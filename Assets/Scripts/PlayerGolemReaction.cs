using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGolemReaction : MonoBehaviour
{
    public GolemDamage isDamage;

    public Transform PlayerTransform;
    public Transform DamageTranform;

    private Transform TargetPos;


    [SerializeField] private float SpeedReact;



    private void FixedUpdate()
    {
        PlayerReact();
    }
    private void PlayerReact()
    {
        if (isDamage.PlayerReaction)
        {

            transform.position = Vector3.MoveTowards(PlayerTransform.position, TargetPos.position, Time.fixedDeltaTime * SpeedReact);

        }


    

    }



}
