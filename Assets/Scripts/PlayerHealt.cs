using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealt : MonoBehaviour
{
    public Image PlayerHealthImage;
    [SerializeField] public float PlayerHealthValue = 1;
    public NavMeshControl control;

    private bool isTakingDamage = false;
    public bool die = false;

    void Start()
    {
        NavMeshControl.OnGoblinAttack += HandleGoblinAttack;
    }

    private void FixedUpdate()
    {
        TakePlayerDamage();
       
    }

    void OnDestroy()
    {
        NavMeshControl.OnGoblinAttack -= HandleGoblinAttack;
    }

    public void TakePlayerDamage()
    {
        if (isTakingDamage || die) // If taking damage or already dead
        {
            PlayerHealthImage.fillAmount = PlayerHealthValue;
            
        }
    }

  

    void HandleGoblinAttack()
    {
        if (control.animator.GetInteger("AttackType") == 1)
        {
            Debug.Log("Vurdu");
            //StartCoroutine(DealDamageOverTime(0.3333f, 0.001f));
            
        }
        if (PlayerHealthValue < 0.01f)
        {
            die = true;
        }
     
    }

    IEnumerator DealDamageOverTime(float duration, float damageAmount)
    {
        isTakingDamage = true;
        yield return new WaitForSeconds(duration);
        PlayerHealthValue -= damageAmount;
        PlayerHealthImage.fillAmount = PlayerHealthValue;
        isTakingDamage = false;
    }
}
