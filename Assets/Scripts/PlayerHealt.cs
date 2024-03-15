using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using StarterAssets;

public class PlayerHealt : MonoBehaviour
{
    public ThirdPersonController controller;
    public Image PlayerHealthImage;
    [SerializeField] public float PlayerHealthValue = 1;
    public NavMeshControl control;

    private bool isTakingDamage = false;
    public bool die = false;
    public Volume globalVolume;
    public HealtSystem system;

    public bool isdamage;

    private Vignette vignette;
    private float startTime;
    private float duration = 0.5f; // Süre 0.5 saniye
    public Animator animator;
    public HitAnim hit;

    public bool vinyetbool;

    public float currentValue;

    public float speed = 0.5f;

    void Start()
    {
        globalVolume.profile.TryGet(out vignette);
        NavMeshControl.OnGoblinAttack += HandleGoblinAttack;
    }

    private void FixedUpdate()
    {
        if (isdamage)
        {
            TakePlayerDamage();
            DamageEffectPlayer();

        }


        HandleGoblinAttack();
    }

    void OnDestroy()
    {
        NavMeshControl.OnGoblinAttack -= HandleGoblinAttack;
    }

    public void TakePlayerDamage()
    {
        if (isTakingDamage || die)
        {
            PlayerHealthImage.fillAmount = PlayerHealthValue;
        }
    }

    void HandleGoblinAttack()
    {
        if (PlayerHealthValue < 0.01f)
        {
            die = true;
        }
    }

    private void DamageEffectPlayer()
    {
        if (vinyetbool)
        {
            if (!die)
            {
                animator.SetBool("Hit", true);
            }
         
            vignette.intensity.value = 0.3f;
            StartCoroutine(ResetVignette());
            StartCoroutine(ResetAnim());
        }
    }

    IEnumerator ResetVignette()
    {
        yield return new WaitForSeconds(0.5f);
        vinyetbool = false;
        vignette.intensity.value = 0f;
        animator.SetBool("Hit", false);
    }
    IEnumerator ResetAnim()
    {
        yield return new WaitForSeconds(0.2f);
        
        animator.SetBool("Hit", false);
    }
}
