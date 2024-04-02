using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GolemNavMesh : MonoBehaviour
{

    public bool uzaklastir;
    public NavMeshAgent agent;
    public Transform Player;
    public Animator animator;
    public float kovalamaMesafesi = 5f; // Kovalama mesafesi
    public float kalkan_mesafesi = 2f;
    private float gorunusAcisi = 360; // Görüþ açýsý
    private float idleSpeedThreshold = 0.1f;
    public float patrolRadius = 10f; //devriye
    public float timeAtRandomPoint = 10f;
    public float attackDestination = 1.5f;

    public bool isRandomAttackSet = false;
    public int fixedRandomAttack;

    public bool GoblinDamage = false;
    public delegate void GoblinAttackEventHandler();
    public static event GoblinAttackEventHandler OnGoblinAttack;


    private float timer = 0f;
    public bool isAtRandomPoint = false;
    public bool isDestroy = false;
<<<<<<< HEAD
<<<<<<< HEAD
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


>>>>>>> parent of 4e6e600 (NewPlamece)


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
<<<<<<< HEAD
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
    void Start()
    {
        shield = FindObjectOfType<Shield>();
        agent = GetComponent<NavMeshAgent>();
       

    }









    //Range Ýþlemleri

    public void Range()
    {

        if (healt.RangeCounter % 5 == 0&&healt.RangeCounter!=0)
        {

            RangeVfx.Play();


            //burada range iþlemleri yapýlacak


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

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        if (isDestroy)
        {
=======
=======
>>>>>>> parent of c739808 (TapinakFinish)
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
<<<<<<< HEAD
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
        if (Sersemleme)
        {
            animator.SetBool("Injurned", true);
            StartCoroutine(WaitforSersemleme());
        }

        Range();



        if (birth.goblinOneSpawn)
        {
            transform.position = newTransform.position;
            birth.goblinOneSpawn=false;
        }
   


        UpdatePlayerDistance();

        float ShieldDistance = Vector3.Distance(Goblin.transform.position, Player.transform.position);


        if (isDestroy)
        {
            agent.isStopped = true;
>>>>>>> parent of 4e6e600 (NewPlamece)
            StartCoroutine(GoblinDestroy());
        }


        

        if (!healt.isDamageBlood)
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
=======
>>>>>>> parent of c739808 (TapinakFinish)


            if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination && !isDestroy)
            {
                agent.isStopped = false;
                animator.SetInteger("AttackTypeGolem", 0);
                agent.destination = Player.position;
>>>>>>> parent of c739808 (TapinakFinish)


            Vector3 lookAtPlayer = new Vector3(Player.position.x - transform.position.x, 0, Player.position.z - transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(lookAtPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

<<<<<<< HEAD
            animator.SetFloat("GolemSpeed", 0.5f); // Walk 
            ResetTimer();
            gorunusAcisi = 360;
            Golemagent.speed = 2f;
            isRandomAttackSet = false; // Yeni random deðer 
            isAtRandomPoint = false; // Yeni random deðer 
=======
                animator.SetFloat("GolemSpeed", 0.5f); //Koþma
                ResetTimer();
                gorunusAcisi = 360;
                agent.speed = 6f;
                isRandomAttackSet = false; // Yeni random deðer 
                isAtRandomPoint = false; // Yeni random deðer 
            }

            else if (mesafeOyuncu < attackDestination && !isDestroy && playerHealt.PlayerHealthImage.fillAmount >= 0.01f) 
            {
                agent.isStopped = true;

                if (!isRandomAttackSet)
                {
                    // Koþul saðlandýðýnda bir kez oluþturulan randomAttack deðeri
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

            if (agent.velocity.magnitude <= idleSpeedThreshold && !isDestroy)
            {
                animator.SetFloat("GolemSpeed", 0.5f); // Walk 


                UpdateTimer();
            }



        }
        else if (healt.isDamageBlood && !isDestroy)
        {

            StartCoroutine(GoblinWait());
>>>>>>> parent of c739808 (TapinakFinish)
        }

        else if (mesafeOyuncu < attackDestination)
        {
            Golemagent.isStopped = true;

<<<<<<< HEAD
            if (!isRandomAttackSet)
=======
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * agent.speed);



            if (transform.position == targetPosition)
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
            

            if (mesafeOyuncu <= kovalamaMesafesi && IsPlayerInSight() && mesafeOyuncu > attackDestination && !isDestroy&&tetikleme.GolemTetiklemeBool&&!Sersemleme)
>>>>>>> parent of 4e6e600 (NewPlamece)
            {
                agent.isStopped = false;
                animator.SetInteger("AttackTypeGolem", 0);
                agent.destination = Player.position;


                Vector3 lookAtPlayer = new Vector3(Player.position.x - transform.position.x, 0, Player.position.z - transform.position.z);
                Quaternion rotation = Quaternion.LookRotation(lookAtPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

                animator.SetFloat("GolemSpeed", 0.5f); //Koþma
                ResetTimer();
                gorunusAcisi = 360;
                agent.speed = 5f;
                isRandomAttackSet = false; // Yeni random deðer 
                isAtRandomPoint = false; // Yeni random deðer 
            }
<<<<<<< HEAD
<<<<<<< HEAD

            else if (mesafeOyuncu < attackDestination && !isDestroy && playerHealt.PlayerHealthImage.fillAmount >= 0.01f&&tetikleme.GolemTetiklemeBool&&!Sersemleme) 
            {
                agent.isStopped = true;

                if (!isRandomAttackSet)
                {
                    // Koþul saðlandýðýnda bir kez oluþturulan randomAttack deðeri
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
<<<<<<< HEAD

            if (agent.velocity.magnitude <= idleSpeedThreshold && !isDestroy&&!Sersemleme)
            {
                animator.SetFloat("GolemSpeed", 0.25f); // Walk 


                UpdateTimer();
            }
<<<<<<< HEAD
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
        }
=======
>>>>>>> parent of 4e6e600 (NewPlamece)



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
        // Her güncelleme sýrasýnda oyuncu ile mesafeyi güncelle
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
