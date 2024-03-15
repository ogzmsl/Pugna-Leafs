using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinThreeAnimationEvent : MonoBehaviour
{
    [SerializeField] private PlayerHealt playerHealthScript;
    [SerializeField] private NavMeshControlthree nav;
    public bool ishield;
    public bool DamageAnim = false;
    public Animator animator;

    private void Start()
    {
        // Öncelikle gerekli bileþenleri aldýðýnýzdan emin olun
        animator = GetComponent<Animator>();
        if (playerHealthScript == null)
        {
            Debug.LogError("PlayerHealt scripti yok.");
        }
        if (nav == null)
        {
            Debug.Log("NavMeshControl scripti yok");
        }
    }

    private void FixedUpdate()
    {
        if (DamageAnim)
        {
            animator.SetTrigger("Damage");
            StartCoroutine(DamageWait());
        }
    }

    IEnumerator DamageWait()
    {
        yield return new WaitForSeconds(0.5f);
    }

    public void OnGoblinAttackAnimationEvent()
    {
        if (playerHealthScript != null)
        {
            Invoke("TakeDamage", 0.85f);
        }
        else
        {
            Debug.LogError("PlayerHealt scripti atanmamýþ.");
        }
    }

    private void TakeDamage()
    {
        if (ishield)
        {
            playerHealthScript.PlayerHealthValue -= 0.1f;
        }
        playerHealthScript.PlayerHealthValue = Mathf.Clamp01(playerHealthScript.PlayerHealthValue);
        playerHealthScript.PlayerHealthImage.fillAmount = playerHealthScript.PlayerHealthValue;
    }
}
