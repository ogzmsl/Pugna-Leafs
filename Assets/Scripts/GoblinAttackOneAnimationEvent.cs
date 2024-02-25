using UnityEngine;

public class GoblinAttackOneAnimationEvent : MonoBehaviour
{
    [SerializeField] private PlayerHealt playerHealthScript;
    [SerializeField] private NavMeshControl nav;
    public bool ishield;

    private void Start()
    {
        
        if (playerHealthScript == null)
        {
            Debug.LogError("PlayerHealt scripti yok.");
        }
        if (nav == null)
        {
            Debug.Log("navmesh scriptti yok");
        }
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
