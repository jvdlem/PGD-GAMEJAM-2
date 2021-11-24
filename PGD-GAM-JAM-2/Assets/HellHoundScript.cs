using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HellHoundScript : GroundEnemyScript
{
    [SerializeField] public Animator anim;
    public LayerMask groundLayer, playerLayer;
    [SerializeField] public Transform target;
    private Transform hound;
    private PlayerHealthScript playerHealth;

    //Attack variables
    public bool isAttacking = false, pounceTargetSet = false;
    public int attackDamage = 2;
    private float attackTimer = .5f;
    private float pounceDistance = 1;
    public Vector3 pounceTarget = Vector3.zero;
    public float attackDistance = 2;

    //Move variables
    private float speed, hopHeight;
    private bool isGrounded = true;
    public Vector3 walkPoint;
    public bool walkPointSet, hopTargetSet = false;
    public int idleTimer = 1;
    private float hopDistance = 1;
    public Vector3 hopTarget = Vector3.zero;

    //States variables
    public float detectionDistance = 10;
    public bool playerDetected, playerInAttackRange, alreadyAttacking;

    enum States
    {
        Attacking,
        Chasing,
        Patrolling,
        Hopping,
        Death
    }
    private States currentState;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealthScript>();
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = States.Patrolling;
        hound = GetComponent<Transform>();
        Health = 5;
        Tier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentState);

        playerDetected = Physics.CheckSphere(transform.position, detectionDistance, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackDistance, playerLayer);

        if (!playerDetected && !playerInAttackRange) { currentState = States.Patrolling; }
        if (playerDetected && !playerInAttackRange) { currentState = States.Chasing; }
        if (Health <= 0) { currentState = States.Death; }

        switch (currentState)
        {
            case States.Patrolling:
                hopTargetSet = false;
                pounceTargetSet = false;
                
                Patrolling();
                break;
            case States.Chasing:
                hopTargetSet = false;
                pounceTargetSet = false;

                Chasing();

                if (playerDetected && playerInAttackRange) { currentState = States.Attacking; }
                break;
            case States.Attacking:
                //Forget previous hop target
                hopTargetSet = false;
                Pounce();
                break;
            case States.Hopping:
                //Erase the previous target position
                pounceTargetSet = false;
                Hop();
                break;
            case States.Death:
                //Dying animation and sounds
                //Destroy(this);
                break;
            default:
                break;
        }
    }
    void Chasing()
    {
        //chases while looking at the player
        hound.transform.LookAt(new Vector3(target.position.x, this.transform.position.y, target.position.z));

        //go to the target
        navMeshAgent.SetDestination(new Vector3(target.position.x, this.transform.position.y, target.position.z));
    }
    void Pounce()
    {
        //Make the hound look at the player
        hound.transform.LookAt(new Vector3(target.position.x, this.transform.position.y, target.position.z));

        if (!pounceTargetSet) {
            //Isn't attacking
            isAttacking = false;

            //Reset the hound speed
            navMeshAgent.speed = 7;

            SearchPounceTarget(); 
        }
        if (pounceTargetSet)
        {
            //Hound attacks
            isAttacking = true;

            //The hound pounces towards target fast
            navMeshAgent.speed = 10;
            navMeshAgent.SetDestination(pounceTarget);

            //Play animation
            anim.Play("Pounce");
        }
        //Calculate distance to pounce target
        Vector3 distanceToPouncePoint = transform.position - pounceTarget;

        //Pounce target reached
        if (distanceToPouncePoint.magnitude < 1f)
        {
            //Enter random state after attack
            float randomState = Random.Range(0, 2);
            if (randomState == 0)
            {
                //The hound enters the hopping state
                currentState = States.Hopping;
            }
            else
            {
                //Erase the previous target position
                pounceTargetSet = false;
            }
        }
    }
    void Hop()
    {
        //hops sideways while looking at the player
        hound.transform.LookAt(new Vector3(target.position.x, this.transform.position.y, target.position.z));

        if (!hopTargetSet)
        {
            SearchHopTarget();
        }
        if (hopTargetSet)
        {
            //Set hop speed
            navMeshAgent.speed = 10;

            //Play animation
            anim.Play("Hop");

            navMeshAgent.SetDestination(hopTarget);
        }
        //Calculate distance to pounce target
        Vector3 distanceToHopPoint = transform.position - hopTarget;

        //Hop target reached
        if (distanceToHopPoint.magnitude < 1f)
        {
            //Reset hound speed
            navMeshAgent.speed = 7;

            anim.Play("IdleHound");

            //go to a random State after the hop
            float randomState = Random.Range(0, 2);
            if (randomState == 0)
            {
                //Forget previous hop target
                hopTargetSet = false;
            }
            else currentState = States.Attacking;
        }

    }
    private void Patrolling()
    {
        //Search a walkpoint if there is none set yet
        if (!walkPointSet)
        {
            Invoke(nameof(SearchRandomWalkPoint), idleTimer);
            anim.Play("IdleHound");
        }

        //Let the golem walk towards the walkpoint only when the walkpoint is set
        if (walkPointSet)
        {
            navMeshAgent.SetDestination(walkPoint);
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
        float randomZ = Random.Range(-detectionDistance / 4, detectionDistance / 4);
        float randomX = Random.Range(-detectionDistance / 4, detectionDistance / 4);

        walkPoint = new Vector3(this.gameObject.transform.position.x + randomX, this.gameObject.transform.position.y, this.gameObject.transform.position.y + randomZ);

        //Check if walkpoint is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer)) { walkPointSet = true; }
    }
    void SearchPounceTarget()
    {
        //the hound aims for the player
        //hound.transform.LookAt(target.position);

        //Set new target from current position
        pounceTarget = new Vector3(target.position.x, transform.position.y, target.position.z);

        //Set target bool true if the target is on the ground 
        if (Physics.Raycast(pounceTarget, -transform.up, 2f, groundLayer)) pounceTargetSet = true;
    }
    void SearchHopTarget()
    {
        //Randomize a direction to hop towards from the current position
        float hopdirection;
        float randomX = Random.Range(0, 1);
        if (randomX == 0)
        {
            hopdirection = 3;
        }
        else hopdirection = -3;
        float randomZ = Random.Range(-2, -1);

        //Set target coordinates from current position
        hopTarget = new Vector3(transform.position.x + hopdirection, transform.position.y, transform.position.z + randomZ);

        //Set target bool true if the hop target is on the ground
        if (Physics.Raycast(hopTarget, -transform.up, 2f, groundLayer)) hopTargetSet = true;
    }
    private void OnTriggerEnter(Collider collision)
    {
        //Hound damages player
        //if (isAttacking)
        //{
            if (gameObject.tag == "Player")
            {
                playerHealth.takeDamage(attackDamage);
            }
        //}

        //Hound takes damage
        if (gameObject.tag == "Projectile")
        {
            Health--;
        }
    }
}
