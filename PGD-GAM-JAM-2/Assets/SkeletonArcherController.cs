using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcherController : GroundEnemyScript
{
    // Start is called before the first frame update
    //public Animator anim;
    public LayerMask groundLayer, playerLayer;
    public Transform pivotPoint;

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
    public float detectionDistance = 25;
    public float attackDistance = 15;
    public bool playerDetected, playerInAttackRange, alreadyAttacking;

    override public void Start()
    {
        pivotPoint = GetComponent<Transform>();
        //anim = GetComponent<Animator>();
        //agent = GetComponent<NavMeshAgent>();
        Health = 20;
        Damage = 3;
        Tier = 2;
        attackTimer = 0;
        attackState = Random.Range(0, attackVariations.Length);
    }
    // Update is called once per frame
    override public void Update()
    {
        playerDetected = Physics.CheckSphere(this.gameObject.transform.position, detectionDistance, playerLayer);
        playerInAttackRange = Physics.CheckSphere(this.gameObject.transform.position, attackDistance, playerLayer);

        if (!playerInAttackRange && !playerDetected) { currentState = States.Patrolling; }

        Die();

        switch (currentState)
        {
            case States.Patrolling:
                Patrolling(); attackTimer = 0;
                if (!playerInAttackRange && playerDetected) { currentState = States.Chasing; }
                break;
            case States.Chasing:
                Chase(); attackTimer = 0;
                if (playerInAttackRange && playerDetected) { currentState = States.Attacking; }
                break;
            case States.Attacking:
                Attacking();
                if (!playerInAttackRange && playerDetected) { currentState = States.Chasing; }
                break;
            case States.Death:
                break;
        }
    }
    private void Patrolling()
    {
        //Search a walkpoint if there is none set yet
        if (!walkPointSet)
        {
            Invoke(nameof(SearchRandomWalkPoint), idleTimer);
            //anim.Play("Idle");
        }

        //Let the golem walk towards the walkpoint only when the walkpoint is set
        if (walkPointSet)
        {
            navMeshAgent.SetDestination(walkPoint);
            //Golem Looks at the target
            pivotPoint.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
            //anim.Play("Walking");
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
        //anim.Play("Walking");

        //Golem looks at player and goes to their position
        pivotPoint.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
        navMeshAgent.SetDestination(Player.transform.position);
    }
    private void Attacking()
    {
        //The golem stands stil during attack
        navMeshAgent.SetDestination(transform.position);

        //The golem aims at the player
        pivotPoint.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        //Time between attack timer counts up;
        attackTimer++;

        //Shoot attack
        if (attackTimer < timeBetweenAttacks)
        {
            //anim.Play("SwingAttack");

            if (attackTimer < 2)
            {
                //Play animation once
                //anim.Play("SwingAttack");
                // else if(attackTimer<(timeBetweenAttacks*.75))  //Golem doen't walk when attacking
                //   anim.Play("Idle");
            }
        }
        else //Timer runs out
        {
            //Reset Attack
            attackTimer = 0;
        }
    }
    void Die()
    {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        //Projectile hurts skeleton on collision
        if (collision.gameObject.tag == "Projectile")
        {
            Health--;
        }

        //Skeleton hurts player on collision
        if (attackTimer != 0)
        {
            if (collision.gameObject.tag == "Player")
            {
                //Player loses health
                Player.GetComponent<PlayerHealthScript>().takeDamage(3);
            }
        }
    }
}


