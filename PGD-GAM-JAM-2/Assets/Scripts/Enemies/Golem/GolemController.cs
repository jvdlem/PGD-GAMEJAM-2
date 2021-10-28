using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemController : GroundEnemyScript
{
    // Start is called before the first frame update
    //[SerializeField]private GameObject golem;
    public Animator anim;
    [SerializeField]public GameObject pivotPoint;
    //public NavMeshAgent agent;
    public LayerMask groundLayer, playerLayer;

    [Header("Movement variables")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public int idleTimer;

    [Header("Attack variables")]
    public float timeBetweenAttacks;
    bool isAttacking;
    private float attackState, attackTimer;
    private string[] attackVariations = { "Swing", "Slam" };

    [Header("States")]
    public float detectionDistance = 20;
    public float attackDistance = 10;
    public bool playerDetected, playerInAttackRange, alreadyAttacking;

    override public void Start()
    {
        pivotPoint = GetComponent<GameObject>();
        anim = GetComponent<Animator>();
        //agent = GetComponent<NavMeshAgent>();
        Health = 20;
        Damage = 3;
        Tier = 2;
        attackTimer =0;
        attackState = Random.Range(0, attackVariations.Length);
    }
    // Update is called once per frame
    override public void Update()
    {
        //base.Update();

        playerDetected = Physics.CheckSphere(this.gameObject.transform.position, detectionDistance, playerLayer);
        playerInAttackRange = Physics.CheckSphere(this.gameObject.transform.position, attackDistance, playerLayer);

        if (!playerInAttackRange && !playerDetected) { Patrolling(); attackTimer = 0; }
        if (!playerInAttackRange && playerDetected) { Chase(); attackTimer = 0; }
        if (playerInAttackRange && playerDetected) { Attacking(); }
        Die();

        Debug.Log(attackTimer);
        Debug.Log(timeBetweenAttacks * .75);
    }
    private void Patrolling()
    {
        //Search a walkpoint if there is none set yet
        if (!walkPointSet) {
            Invoke(nameof(SearchRandomWalkPoint), idleTimer);
            anim.Play("Idle");
        }

        //Let the golem walk towards the walkpoint only when the walkpoint is set
        if (walkPointSet)
        {
            navMeshAgent.SetDestination(walkPoint);
            pivotPoint.transform.LookAt(walkPoint);
            anim.Play("Walking");
        }

        //Calculate distance to walkpoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached so set walkpoint set to false
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }
    private void SearchRandomWalkPoint()
    {
        //Determine a random point in the Golems detection range 
        float randomZ = Random.Range(-detectionDistance / 2, detectionDistance / 2);
        float randomX = Random.Range(-detectionDistance / 2, detectionDistance / 2);

        walkPoint = new Vector3(this.gameObject.transform.position.x + randomX, this.gameObject.transform.position.y, this.gameObject.transform.position.y + randomZ);

        //Check if walkpoint is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer)) { walkPointSet = true; }
    }
    private void Chase()
    {
        anim.Play("Walking");

        //Golem looks at player and goes to their position
        //base.Update();
        pivotPoint.transform.LookAt(Player.transform.position);
        navMeshAgent.SetDestination(Player.transform.position);
    }
    private void Attacking()
    {
        //The golem stands stil during attack
        navMeshAgent.SetDestination(transform.position);

        //The golem aims at the player
       pivotPoint.transform.LookAt(Player.transform.position);

        //Time between attack timer counts up;
        attackTimer++;

        //Different attacks
        switch (attackState)
        {
            case 0: //Swing attack
                if (attackTimer < timeBetweenAttacks)
                {
                    //anim.Play("SwingAttack");

                    if (attackTimer < 2) //Play animation once
                        anim.Play("SwingAttack");
                   // else if(attackTimer<(timeBetweenAttacks*.75))  //Golem doen't walk when attacking
                     //   anim.Play("Idle");
                }
                else //Timer runs out
                {

                    //Switch to a random state
                    attackState = Random.Range(0, attackVariations.Length);
                    attackTimer = 0;
                }
                break;
            case 1: //Slam Attack
                if (attackTimer < timeBetweenAttacks)
                {
                    //anim.Play("SlamAttack");
                    if (attackTimer < 2)//Play animation once
                        anim.Play("SlamAttack");
                   // else if (attackTimer < (timeBetweenAttacks * .75))  //Golem doen't wlka when attacking
                     //   anim.Play("Idle");
        }
                else
                {
                    attackState = Random.Range(0, attackVariations.Length);
                    attackTimer = 0;
                }
                break;
            default:
                break;
        }
    }
    void Die()
    {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Projectile hurts goem on collision
        if (collision.gameObject.tag == "Projectile")
        { Health--; }

        //Golem hurts player on collision
        if (collision.gameObject.tag == "Player")
        {
            //Player loses health
           //Player.GetComponent<>
        }
    }
}
