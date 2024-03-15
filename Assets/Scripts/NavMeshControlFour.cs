using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshControlFour : MonoBehaviour
{
    public bool uzaklasyirFour;
    public NavMeshAgent agent;
    public Transform Player;
    public Animator animator;
    public float kovalamaMesafesi = 5f; // Kovalama mesafesi
    public float kalkan_mesafesi = 2f;
    private float gorunusAcisi = 360; // G�r�� a��s�
    private float idleSpeedThreshold = 0.1f;
    public float patrolRadius = 10f; //devriye
    public float timeAtRandomPoint = 10f;
    public float attackDestination = 1.5f;

    private bool isRandomAttackSet = false;
    private int fixedRandomAttack;

    public bool GoblinDamage = false;
    public delegate void GoblinAttackEventHandler();
    public static event GoblinAttackEventHandler OnGoblinAttack;


    private float timer = 0f;
    private bool isAtRandomPoint = false;
    public bool isDestroy = false;
    public GameObject Goblin;
    Shield shield;
    public GoblinAttackOneAnimationEvent goblin;
    public HealtSystem healt;


    void Start()
    {
        shield = FindObjectOfType<Shield>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {




        float mesafeOyuncu = Vector3.Distance(transform.position, Player.position);

        float ShieldDistance = Vector3.Distance(Goblin.transform.position, Player.transform.position);


        if (isDestroy)
        {

            StartCoroutine(GoblinDestroy());
        }


        if (!healt.isDamageBlood)
        {


            if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination && !isDestroy)
            {
                agent.isStopped = false;
                animator.SetInteger("AttackType", 0);
                agent.destination = Player.position;


                Vector3 lookAtPlayer = new Vector3(Player.position.x - transform.position.x, 0, Player.position.z - transform.position.z);
                Quaternion rotation = Quaternion.LookRotation(lookAtPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

                animator.SetFloat("speed", 1.0f); //Ko�ma
                ResetTimer();
                gorunusAcisi = 360;
                agent.speed = 4f;
                isRandomAttackSet = false; // Yeni random de�er 
                isAtRandomPoint = false; // Yeni random de�er 
            }

            else if (mesafeOyuncu < attackDestination && !isDestroy)
            {

                agent.isStopped = true;

                if (!isRandomAttackSet)
                {
                    // Ko�ul sa�land���nda bir kez olu�turulan randomAttack de�eri
                    fixedRandomAttack = Random.Range(1, 3);
                    isRandomAttackSet = true;
                }

                animator.SetInteger("AttackType", 1);


                if (fixedRandomAttack == 1)
                {
                    OnGoblinAttack?.Invoke();
                }

                gorunusAcisi = 360;
            }
            else
            {
                if (!isAtRandomPoint && !isDestroy)
                {

                    MoveToRandomPoint();
                    isAtRandomPoint = true;
                    isRandomAttackSet = false;
                    agent.speed = 0.8f;
                }
            }

            if (agent.velocity.magnitude <= idleSpeedThreshold && !isDestroy)
            {
                animator.SetFloat("speed", 0.5f); // Walk 


                UpdateTimer();
            }



        }
        else if (healt.isDamageBlood && !isDestroy)
        {

            StartCoroutine(GoblinWait());
        }

        if (uzaklasyirFour && ShieldDistance < 1.85f)
        {
            Vector3 directionToPlayer = (transform.position - Player.position).normalized;
            Vector3 targetPosition = transform.position + directionToPlayer * 3f;
            targetPosition.y = transform.position.y;
            animator.SetInteger("AttackType", 1);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * agent.speed);

        }



    }

    IEnumerator GoblinWait()
    {
        yield return new WaitForSeconds(0.3f);
        agent.speed = 0f;
        agent.isStopped = true;
    }





    IEnumerator GoblinDestroy()
    {
        yield return new WaitForSeconds(1.7f);

        Destroy(Goblin);

    }





    void MoveToRandomPoint()
    {

        animator.SetFloat("speed", 0.5f); // Walk speed


        Vector3 randomPoint = Random.insideUnitSphere * patrolRadius + transform.position;
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, UnityEngine.AI.NavMesh.AllAreas);

        StartCoroutine(MoveToRandomPointCoroutine(hit.position));
    }

    IEnumerator MoveToRandomPointCoroutine(Vector3 destination)
    {
        agent.destination = destination;


        yield return new WaitUntil(() => agent.remainingDistance < 0.1f);


        animator.SetFloat("speed", 0f);
        isAtRandomPoint = false;
    }

    bool IsPlayerInSight()
    {
        Vector3 oyuncuYonu = (Player.position - transform.position).normalized;
        float aci = Vector3.Angle(transform.forward, oyuncuYonu);

        if (aci <= gorunusAcisi * 0.5f)
        {
            return true;
        }

        return false;
    }

    void UpdateTimer()
    {
        timer += Time.deltaTime;

        if (timer >= timeAtRandomPoint)
        {
            isAtRandomPoint = false;
            timer = 0f;
        }
    }

    void ResetTimer()
    {
        timer = 0f;
    }



}
