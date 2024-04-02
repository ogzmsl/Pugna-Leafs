using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshControl : MonoBehaviour
{

    public bool uzaklastir;
   public  NavMeshAgent agent;
    public Transform Player;
    public Animator animator;
    public float kovalamaMesafesi = 5f; // Kovalama mesafesi
    public float kalkan_mesafesi=2f;
    private float gorunusAcisi = 360; // Görüþ açýsý
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
    [SerializeField] private HealtSystem healt;

<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======

>>>>>>> Stashed changes
    public PlayerBirth birth;
    public Transform newTransform;
    public PlayerHealt playerHealt;

<<<<<<< Updated upstream

<<<<<<< HEAD
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of d93495c (revert)
=======
    public GoblinTetikleme tetikleme;
>>>>>>> Stashed changes
=======
>>>>>>> parent of d93495c (revert)

    void Start()
    {
        shield = FindObjectOfType<Shield>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {

<<<<<<< Updated upstream
      
    
=======



        if (birth.Spawn)
        {

            transform.position = newTransform.position;
            birth.Spawn = false;
        }


        if (healt.isDamageBlood)
        {

            StartCoroutine(GoblinWait());


        }

>>>>>>> Stashed changes

        float mesafeOyuncu = Vector3.Distance(transform.position, Player.position);

        float ShieldDistance = Vector3.Distance(Goblin.transform.position, Player.transform.position);


        if (isDestroy)
        {
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
        
>>>>>>> parent of c739808 (TapinakFinish)
=======
        
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of d93495c (revert)
=======

            agent.speed = 0;

>>>>>>> Stashed changes
=======
>>>>>>> parent of d93495c (revert)
            StartCoroutine(GoblinDestroy());
        }


        if (!healt.isDamageBlood)
        {


<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination)
=======
            if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination&&!isDestroy)
>>>>>>> parent of c739808 (TapinakFinish)
=======
            if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination&&!isDestroy)
>>>>>>> parent of c739808 (TapinakFinish)
=======
            if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination)
>>>>>>> parent of d93495c (revert)
=======
            if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination)
>>>>>>> parent of d93495c (revert)
            {
                agent.isStopped = false;
                animator.SetInteger("AttackType", 0);
                agent.destination = Player.position;


                Vector3 lookAtPlayer = new Vector3(Player.position.x - transform.position.x, 0, Player.position.z - transform.position.z);
                Quaternion rotation = Quaternion.LookRotation(lookAtPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

                animator.SetFloat("speed", 1.0f); //Koþma
                ResetTimer();
                gorunusAcisi = 360;
                agent.speed = 4f;
                isRandomAttackSet = false; // Yeni random deðer 
                isAtRandomPoint = false; // Yeni random deðer 
            }

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> parent of d93495c (revert)
=======
>>>>>>> parent of d93495c (revert)
            else if (mesafeOyuncu < attackDestination)
            {

                agent.isStopped = true;
<<<<<<< HEAD
<<<<<<< HEAD
=======
            else if (mesafeOyuncu < attackDestination && !isDestroy && playerHealt.PlayerHealthImage.fillAmount >= 0.01)
            {

                agent.isStopped = true;

>>>>>>> parent of c739808 (TapinakFinish)
=======
            else if (mesafeOyuncu < attackDestination && !isDestroy && playerHealt.PlayerHealthImage.fillAmount >= 0.01)
            {

                agent.isStopped = true;

>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of d93495c (revert)
=======
>>>>>>> parent of d93495c (revert)

                if (!isRandomAttackSet)
                {
                    // Koþul saðlandýðýnda bir kez oluþturulan randomAttack deðeri
                    fixedRandomAttack = Random.Range(1, 3);
                    isRandomAttackSet = true;
                }

                animator.SetInteger("AttackType", fixedRandomAttack);

                Debug.Log(fixedRandomAttack);

                if (fixedRandomAttack == 1)
                {
                    OnGoblinAttack?.Invoke();
                }

                gorunusAcisi = 360;
            }
            else
            {
                if (!isAtRandomPoint)
                {

                    MoveToRandomPoint();
                    isAtRandomPoint = true;
                    isRandomAttackSet = false;
                    agent.speed = 0.8f;
                }
            }

            if (agent.velocity.magnitude <= idleSpeedThreshold)
            {
                animator.SetFloat("speed", 0.5f); // Walk 
=======

            if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination)

                if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination && !isDestroy)
>>>>>>> Stashed changes


                    if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination && !isDestroy)

                        if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination)

                            if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination && !isDestroy && tetikleme.GoblinTetiklemeBool)

                            {
                                agent.isStopped = false;
                                animator.SetInteger("AttackType", 0);
                                agent.destination = Player.position;
                                // MainMusic.Stop();

                                Vector3 lookAtPlayer = new Vector3(Player.position.x - transform.position.x, 0, Player.position.z - transform.position.z);
                                Quaternion rotation = Quaternion.LookRotation(lookAtPlayer);
                                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

                                animator.SetFloat("speed", 1.0f); //Koþma
                               // ResetTimer();
                                gorunusAcisi = 360;
                                agent.speed = 4f;
                                isRandomAttackSet = false; // Yeni random deðer 
                                isAtRandomPoint = false; // Yeni random deðer 
                            }


<<<<<<< Updated upstream

        }
        else if (healt.isDamageBlood)
        {
            agent.isStopped = true;
            animator.SetFloat("speed", 0f); //idle
        }
        if (uzaklastir&&ShieldDistance<1.85f)
        {
            Vector3 directionToPlayer = (transform.position - Player.position).normalized;
            Vector3 targetPosition = transform.position + directionToPlayer * 3f;
            targetPosition.y = transform.position.y;
            animator.SetInteger("AttackType", 1);
          
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * agent.speed);
            
          

            if (transform.position == targetPosition)
            {
                uzaklastir = false;
            }
        }



    }


=======
                           
>>>>>>> Stashed changes




<<<<<<< Updated upstream
    IEnumerator GoblinDestroy()
    {
        yield return new WaitForSeconds(1.7f);
        Destroy(Goblin);
        
    }
=======
                             else if (mesafeOyuncu < attackDestination && !isDestroy && playerHealt.PlayerHealthImage.fillAmount >= 0.01 && tetikleme.GoblinTetiklemeBool)
                                        {
>>>>>>> Stashed changes

                                            agent.isStopped = true;

                                            if (!isRandomAttackSet)
                                            {
                                                // Koþul saðlandýðýnda bir kez oluþturulan randomAttack deðeri
                                                fixedRandomAttack = Random.Range(1, 3);
                                                isRandomAttackSet = true;
                                            }

                                            animator.SetInteger("AttackType", fixedRandomAttack);



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




                                    if (uzaklastir && ShieldDistance < 1.85f)
                                    {
                                        Vector3 directionToPlayer = (transform.position - Player.position).normalized;
                                        Vector3 targetPosition = transform.position + directionToPlayer * 3f;
                                        targetPosition.y = transform.position.y;
                                        animator.SetInteger("AttackType", 1);

                                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * agent.speed);

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

                                IEnumerator GoblinWait()
                                {
                                    yield return new WaitForSeconds(0.1f);
                                    agent.speed = 0f;
                                    agent.isStopped = false;
                                }
                                IEnumerator GoblinSpawn()
                                {
                                    yield return new WaitForSeconds(4f);
                                    animator.enabled = true;
                                    animator.SetInteger("AttackType", 0);
                                    agent.speed = 0f;

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
                                    NavMeshHit hit;
                                    NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, NavMesh.AllAreas);

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
