using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemNavMesh : MonoBehaviour
{

    public NavMeshAgent Golemagent;
    public Transform Player;
    public Animator animator;
    public float kovalamaMesafesi = 5f; // Kovalama mesafesi
    private float gorunusAcisi = 360; // Görüþ açýsý
    private float idleSpeedThreshold = 0.1f;
    public float patrolRadius = 10f; //devriye
    public float timeAtRandomPoint = 10f;
    public float attackDestination = 3f;

    private bool isRandomAttackSet = false;
    private int fixedRandomAttack;

    public bool GoblemDamage = false;
    public delegate void GOlemAttackEventHandler();
    public static event GOlemAttackEventHandler OnGolemAttack;


    private float timer = 0f;
    private bool isAtRandomPoint = false;
    public bool isDestroy = false;
    public GameObject Golem;


<<<<<<< HEAD
=======
    public GameObject Goblin;
    Shield shield;
    public GoblinAttackOneAnimationEvent goblin;
    [SerializeField] private HealtSystem healt;
    public PlayerBirth birth;
    public PlayerHealt playerHealt;
    private float mesafeOyuncu = 0f;
    private bool spawn;
    public Transform newTransform;
<<<<<<< HEAD
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of d93495c (revert)
    void Start()
    {

        Golemagent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        if (isDestroy)
        {
=======
=======
>>>>>>> parent of c739808 (TapinakFinish)
        if (birth.goblinOneSpawn)
        {
            transform.position = newTransform.position;
            birth.goblinOneSpawn=false;
        }
   


        UpdatePlayerDistance();

        float ShieldDistance = Vector3.Distance(Goblin.transform.position, Player.transform.position);


        if (isDestroy)
        {

<<<<<<< HEAD
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
        if (isDestroy)
        {
>>>>>>> parent of d93495c (revert)
            StartCoroutine(GoblinDestroy());
        }

        float mesafeOyuncu = Vector3.Distance(transform.position, Player.position);

        if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination)
        {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            Golemagent.isStopped = false;
            animator.SetInteger("AttackTypeGolem", 0);
            Golemagent.destination = Player.position;
=======
=======
>>>>>>> parent of c739808 (TapinakFinish)


            if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination && !isDestroy)
            {
                agent.isStopped = false;
                animator.SetInteger("AttackTypeGolem", 0);
                agent.destination = Player.position;
>>>>>>> parent of c739808 (TapinakFinish)
=======
            Golemagent.isStopped = false;
            animator.SetInteger("AttackTypeGolem", 0);
            Golemagent.destination = Player.position;
>>>>>>> parent of d93495c (revert)


            Vector3 lookAtPlayer = new Vector3(Player.position.x - transform.position.x, 0, Player.position.z - transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(lookAtPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

            animator.SetFloat("GolemSpeed", 0.5f); // Walk 
            ResetTimer();
            gorunusAcisi = 360;
            Golemagent.speed = 2f;
            isRandomAttackSet = false; // Yeni random deðer 
            isAtRandomPoint = false; // Yeni random deðer 
        }

        else if (mesafeOyuncu < attackDestination)
        {
            Golemagent.isStopped = true;

            if (!isRandomAttackSet)
<<<<<<< HEAD
=======
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * agent.speed);



            if (transform.position == targetPosition)
<<<<<<< HEAD
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of d93495c (revert)
            {
                // Koþul saðlandýðýnda bir kez oluþturulan randomAttack deðeri
                fixedRandomAttack = Random.Range(1, 3);
                isRandomAttackSet = true;
            }

            animator.SetInteger("AttackTypeGolem", 1);

            Debug.Log(fixedRandomAttack);

            if (fixedRandomAttack == 1)
            {
                OnGolemAttack?.Invoke();
            }
<<<<<<< HEAD

            gorunusAcisi = 360;
        }
        else
        {
            if (!isAtRandomPoint)
            {

                MoveToRandomPoint();
                isAtRandomPoint = true;
                isRandomAttackSet = false;
                Golemagent.speed = 0.8f;
            }
<<<<<<< HEAD
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of d93495c (revert)
        }

        if (Golemagent.velocity.magnitude <= idleSpeedThreshold)
        {
            animator.SetFloat("GolemSpeed", 0.5f); // Walk 

            UpdateTimer();
        }

    }


    IEnumerator GoblinDestroy()
    {
        yield return new WaitForSeconds(1.7f);
        Destroy(Golem);
    }




    void MoveToRandomPoint()
    {

        animator.SetFloat("GolemSpeed", 0.5f); // Walk speed


        Vector3 randomPoint = Random.insideUnitSphere * patrolRadius + transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, NavMesh.AllAreas);

        StartCoroutine(MoveToRandomPointCoroutine(hit.position));
    }

    IEnumerator MoveToRandomPointCoroutine(Vector3 destination)
    {
        Golemagent.destination = destination;


        yield return new WaitUntil(() => Golemagent.remainingDistance < 0.1f);


        animator.SetFloat("GolemSpeed", 0f);
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
