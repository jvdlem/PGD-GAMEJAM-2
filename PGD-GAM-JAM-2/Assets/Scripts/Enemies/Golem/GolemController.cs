using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemController : GroundEnemyScript
{
    // Start is called before the first frame update
    [SerializeField] public Animator anim;
    public LayerMask groundLayer, playerLayer;

    [Header("Movement variables")]
    public Vector3 walkPoint;
    public bool walkPointSet;
    public int idleTimer = 1;

    [Header("Attack variables")]
    public float timeBetweenAttacks;
    private float timeofLastAttack = 0;
    bool isAttacking = false;
    private float attackTimer = 3f;

    [Header("States")]
    public float detectionDistance;
    private float attackDistance;
    public bool playerDetected, playerInAttackRange, alreadyAttacking;
    override public void Start()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        attackDistance = navMeshAgent.stoppingDistance;
        currentState = States.Patrolling;
    }
    // Update is called once per frame
    override public void Update()
    {
        //Detection radius spheres
        playerDetected = Physics.CheckSphere(this.gameObject.transform.position, detectionDistance, playerLayer);
        playerInAttackRange = Physics.CheckSphere(this.gameObject.transform.position, attackDistance, playerLayer);

        //Golem patrolls if no player is detected
        if (!playerInAttackRange && !playerDetected) { currentState = States.Patrolling; }

        switch (currentState)
        {
            case States.Patrolling:
                Patrolling();
                if (!playerInAttackRange && playerDetected) { currentState = States.Chasing; }
                break;
            case States.Chasing:
                Chase();
                if (playerInAttackRange && playerDetected) { currentState = States.Attacking; }
                break;
            case States.Attacking:
                Attacking();
                if (!playerInAttackRange && playerDetected) { currentState = States.Chasing; }
                break;
        }
        Die();
    }
    private void Patrolling()
    {
        //Search a walkpoint if there is none set yet
        if (!walkPointSet)
        {
            SearchRandomWalkPoint();
            Idle();
        }

        //Let the golem walk towards the walkpoint only when the walkpoint is set
        if (walkPointSet)
        {
            navMeshAgent.SetDestination(walkPoint);
            //Golem Looks at the target
            transform.LookAt(new Vector3(walkPoint.x, this.transform.position.y, walkPoint.z));
            Walk();
        }

        //Calculate distance to walkpoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached so set walkpoint set to false
        if (distanceToWalkPoint.magnitude <= navMeshAgent.stoppingDistance) walkPointSet = false;
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
        //Animation trigger
        Walk();

        //Golem looks at player
        transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        //Golem goes towards the player
        navMeshAgent.SetDestination(Player.transform.position);
    }
    private void Walk()
    {
        anim.SetFloat("Speed", 1);
    }
    private void Idle()
    {
        anim.SetFloat("Speed", 0);
    }
    private void Attacking()
    {
  
        //The golem aims at the player
        transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        float distanceToTarget = Vector3.Distance(Player.transform.position, transform.position);
        if (distanceToTarget<=navMeshAgent.stoppingDistance)
        {
            Idle();

            //Start the attack timer
            if (!isAttacking)
            {
                isAttacking = true;
                timeofLastAttack = Time.time;
            }
            if (Time.time >= timeofLastAttack + attackTimer)
            {
      
                timeofLastAttack = Time.time;
                anim.SetTrigger("Punch");
            }
        }
        else
        {
            isAttacking = false;
        }
    }
    void Die()
    {
        if (Health <= 0)
        {
            anim.SetTrigger("Die");
            Instantiate(Coin, transform.position + new Vector3(0, 1, 0), transform.rotation);
            Invoke(nameof(DeSpawn),3f);
        }
    }
    private void DeSpawn()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        //Projectile hurts goem on collision
        if (collision.gameObject.tag == "Projectile")
        {
            anim.SetTrigger("TakeDamage");
            Health--;
        }

        //Golem hurts player on collision

        if (collision.gameObject.tag == "Player")
        {
            //Player loses health
            Player.GetComponent<PlayerHealthScript>().takeDamage(3);
        }
    }
}
