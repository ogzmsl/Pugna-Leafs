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
    private float gorunusAcisi = 360; // G�r�� a��s�
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
<<<<<<< HEAD
<<<<<<< Updated upstream
    public GameObject Golem;
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
    public FButtonEffectDistance effectDistance;

    //Range
    public float RangeTimer = 0;
    private int AtakSayisi = 0;
    private bool Atak;

    public ParticleSystem RangeVfx;

    public GolemTetikleme tetikleme;

    private bool Sersemleme = false;
    private Vector3 targetPosition;


>>>>>>> Stashed changes



    /*  public GameObject Goblin;
      Shield shield;
      public GoblinAttackOneAnimationEvent goblin;
      [SerializeField] private HealtSystem healt;
      public PlayerBirth birth;
      public PlayerHealt playerHealt;
      private float mesafeOyuncu = 0f;
      private bool spawn;
      public Transform newTransform;*/

=======
    public GameObject Golem;


>>>>>>> parent of d93495c (revert)
    void Start()
    {
<<<<<<< Updated upstream
=======
        shield = FindObjectOfType<Shield>();
        agent = GetComponent<NavMeshAgent>();

>>>>>>> Stashed changes

        Golemagent = GetComponent<NavMeshAgent>();
    }

<<<<<<< Updated upstream
    private void FixedUpdate()
    {

<<<<<<< HEAD
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
            isRandomAttackSet = false; // Yeni random de�er 
            isAtRandomPoint = false; // Yeni random de�er 
        }

        else if (mesafeOyuncu < attackDestination)
        {
            Golemagent.isStopped = true;

            if (!isRandomAttackSet)
<<<<<<< HEAD
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
=======
>>>>>>> parent of d93495c (revert)
            {
                // Ko�ul sa�land���nda bir kez olu�turulan randomAttack de�eri
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
<<<<<<< HEAD
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of d93495c (revert)
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
=======








    //Range ��lemleri

    public void Range()
    {

        if (healt.RangeCounter % 5 == 0 && healt.RangeCounter != 0)
        {

            RangeVfx.Play();


            //burada range i�lemleri yap�lacak


        }

        else
        {
            RangeVfx.Stop();
        }
        if (healt.RangeCounter % 8 == 0 && healt.RangeCounter != 0)
        {
            Sersemleme = true;
        }







    }


    private IEnumerator WaitforSersemleme()
    {
        yield return new WaitForSeconds(7);
        animator.SetBool("Injurned", false);
        healt.RangeCounter++;
        Sersemleme = false;
    }





    private void FixedUpdate()
    {


        if (isDestroy)
        {
            if (birth.goblinOneSpawn)
            {
                transform.position = newTransform.position;
                birth.goblinOneSpawn = false;
            }



            //  UpdatePlayerDistance();

            float ShieldDistance = Vector3.Distance(Goblin.transform.position, Player.transform.position);


            if (isDestroy)
            {


                if (Sersemleme)
                {
                    animator.SetBool("Injurned", true);
                    StartCoroutine(WaitforSersemleme());
                }

                Range();



                if (birth.goblinOneSpawn)
                {
                    transform.position = newTransform.position;
                    birth.goblinOneSpawn = false;
                }



                UpdatePlayerDistance();

                // float ShieldDistance = Vector3.Distance(Goblin.transform.position, Player.transform.position);


                if (isDestroy)
                {
                    agent.isStopped = true;

                    StartCoroutine(GoblinDestroy());
                }




                if (!healt.isDamageBlood)
                {

                    // Golemagent.isStopped = false;
                    animator.SetInteger("AttackTypeGolem", 0);
                    // Golemagent.destination = Player.position;



                    if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination && !isDestroy)
                    {
                        agent.isStopped = false;
                        animator.SetInteger("AttackTypeGolem", 0);
                        agent.destination = Player.position;

                        // Golemagent.isStopped = false;
                        animator.SetInteger("AttackTypeGolem", 0);
                        // Golemagent.destination = Player.position;



                        Vector3 lookAtPlayer = new Vector3(Player.position.x - transform.position.x, 0, Player.position.z - transform.position.z);
                        Quaternion rotation = Quaternion.LookRotation(lookAtPlayer);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

                        animator.SetFloat("GolemSpeed", 0.5f); // Walk 
                        ResetTimer();
                        gorunusAcisi = 360;

                        // Golemagent.speed = 2f;
                        isRandomAttackSet = false; // Yeni random de�er 
                        isAtRandomPoint = false; // Yeni random de�er 
                    }

                    else if (mesafeOyuncu < attackDestination)
                    {
                        // Golemagent.isStopped = true;

                        if (!isRandomAttackSet)

                            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * agent.speed);



                        if (transform.position == targetPosition)



                            if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination && !isDestroy && tetikleme.GolemTetiklemeBool && !Sersemleme)

                            {
                                agent.isStopped = false;
                                animator.SetInteger("AttackTypeGolem", 0);
                                agent.destination = Player.position;


                                Vector3 lookAtPlayer = new Vector3(Player.position.x - transform.position.x, 0, Player.position.z - transform.position.z);
                                Quaternion rotation = Quaternion.LookRotation(lookAtPlayer);
                                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

                                animator.SetFloat("GolemSpeed", 0.5f); //Ko�ma
                                ResetTimer();
                                gorunusAcisi = 360;
                                agent.speed = 5f;
                                isRandomAttackSet = false; // Yeni random de�er 
                                isAtRandomPoint = false; // Yeni random de�er 
                            }

                            else if (mesafeOyuncu < attackDestination && !isDestroy && playerHealt.PlayerHealthImage.fillAmount >= 0.01f && tetikleme.GolemTetiklemeBool && !Sersemleme)
                            {
                                agent.isStopped = true;

                                if (!isRandomAttackSet)
                                {
                                    // Ko�ul sa�land���nda bir kez olu�turulan randomAttack de�eri
                                    fixedRandomAttack = Random.Range(1, 3);
                                    animator.SetInteger("AttackTypeGolem", fixedRandomAttack);
                                    isRandomAttackSet = true;

                                }





                                Debug.Log(fixedRandomAttack);

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


                        if (agent.velocity.magnitude <= idleSpeedThreshold && !isDestroy && !Sersemleme)
                        {
                            animator.SetFloat("GolemSpeed", 0.25f); // Walk 


                            UpdateTimer();
                        }



                    }
                    else if (healt.isDamageBlood && !isDestroy)
                    {

                        StartCoroutine(GoblinWait());
                    }

                    if (uzaklastir && ShieldDistance < 1.85f)
                    {
                        Vector3 directionToPlayer = (transform.position - Player.position).normalized;
                        Vector3 targetPosition = transform.position + directionToPlayer * 3f;
                        targetPosition.y = transform.position.y;
                        animator.SetInteger("AttackTypeGolem", fixedRandomAttack);

                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * agent.speed);



                        if (transform.position == targetPosition)
                        {
                            uzaklastir = false;
                        }

                    }

                    if (effectDistance.isDistanceFButton && ShieldDistance < 5f || playerHealt.PlayerHealthValue < 0.01f)
                    {
                        Vector3 directionToPlayer = (transform.position - Player.position).normalized;
                        Vector3 targetPosition = transform.position + directionToPlayer * 6f;
                        targetPosition.y = transform.position.y;
                        animator.SetFloat("speed", 0f);//idle


                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.fixedDeltaTime * agent.speed * 6);

                    }



                }

                void UpdatePlayerDistance()
                {
                    // Her g�ncelleme s�ras�nda oyuncu ile mesafeyi g�ncelle
                    mesafeOyuncu = Vector3.Distance(transform.position, Player.position);
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

                    animator.SetFloat("GolemSpeed", 0.25f); // Walk speed


                    Vector3 randomPoint = Random.insideUnitSphere * patrolRadius + transform.position;
                    NavMeshHit hit;
                    NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, NavMesh.AllAreas);

                    StartCoroutine(MoveToRandomPointCoroutine(hit.position));
                }

                IEnumerator MoveToRandomPointCoroutine(Vector3 destination)
                {
                    agent.destination = destination;


                    yield return new WaitUntil(() => agent.remainingDistance < 0.1f);


                    animator.SetFloat("GolemSpeed", 0.5f);
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
        }
    }
>>>>>>> Stashed changes
}

