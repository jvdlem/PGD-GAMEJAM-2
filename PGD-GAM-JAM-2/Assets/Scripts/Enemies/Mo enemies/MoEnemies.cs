using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Moenemies : GroundEnemyScript
{  // Start is called before the first frame update
    [SerializeField] public Animator anim;
    public LayerMask groundLayer, playerLayer;
    [SerializeField] public ParticleSystem particles;

    [Header("Movement variables")]
    public Vector3 walkPoint;
    public bool walkPointSet;

    [Header("Attack variables")]
    public float timeofLastAttack = 0;
    public bool isAttacking = false;
    public bool alreadyAttacking;
    [SerializeField] public float attackTimer;

    [Header("Animation Variables")]
    public string attack;
    public string die = "Die";
    public bool triggerDeathAnimation = true, triggerHurtAnimation = true, canBeHurt = true;
    public int deathTimer = 0;

    [Header("States")]
    [SerializeField] public float detectionDistance;
    [SerializeField] public float attackDistance;
    public float hurtTimer;

    [Header("Sounds")]
    public string attackSound, deathSound, hurtSound, windUpSound;
    public Vector3 soundPosition;

    [Header("Health Slider")]
    public int maxHealth;
    public GameObject healthBarUI;
    public Slider slider;

    public virtual bool Radius(float distance) => Physics.CheckSphere(this.gameObject.transform.position, distance, playerLayer);
    public virtual bool playerDetected => Radius(detectionDistance);
    public virtual bool playerInAttackRange => Radius(attackDistance);
    override public void Start()
    {
        healthBarUI.SetActive(false);
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        attackDistance = navMeshAgent.stoppingDistance;
        currentState = States.Patrolling;
        maxHealth = Health;
        Damage = 1;
    }
    // Update is called once per frame
    override public void Update()
    {
        NonStatesRelatedFunctions();

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
            //case States.Hurt:
            //    Hurting();
            //    break;
            case States.Death:

                Dying();
                break;
        }
    }

        void RemoveColliders()
    {

    }

    public void GetDamage(int damage)
    {
        Health -= damage;
    }
    virtual public void NonStatesRelatedFunctions()
    {
        slider.value = CalculateHealth();

        if (Health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if (!playerInAttackRange && !playerDetected) { currentState = States.Patrolling; }
        if (currentState != States.Hurt) triggerHurtAnimation = true;
        EnableParticles();
        if (this.Health <= 0) { canBeHurt = false; currentState = States.Death; }

        if (currentState == States.Hurt)
        {
            hurtTimer += Time.deltaTime;
            if (hurtTimer >= 2)
            {
                hurtTimer = 0;
                currentState = States.Chasing;
            }
        }
    }
    float CalculateHealth()
    {
        return (float)Health / (float)maxHealth;
    }
    virtual public void Patrolling()
    {
        //Search a walkpoint if there is none set yet
        if (!walkPointSet)
        {
            SearchRandomWalkPoint();
            MovementAnimation(false);
        }
        else
        {
            //Let the enemy walk towards the walkpoint only when the walkpoint is set
            navMeshAgent.SetDestination(walkPoint);
            //Enemy Looks at the target
            transform.LookAt(new Vector3(walkPoint.x, this.transform.position.y, walkPoint.z));
            MovementAnimation(true);
        }

        //Calculate distance to walkpoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached so set walkpoint set to false
        if (distanceToWalkPoint.magnitude <= navMeshAgent.stoppingDistance) walkPointSet = false;
    }
    virtual public void SearchRandomWalkPoint()
    {
        //Determine a random point in the Enemy's detection range 
        float randomZ = Random.Range(-detectionDistance / 2, detectionDistance / 2);
        float randomX = Random.Range(-detectionDistance / 2, detectionDistance / 2);

        walkPoint = new Vector3(this.gameObject.transform.position.x + randomX, this.gameObject.transform.position.y, this.gameObject.transform.position.y + randomZ);

        //Check if walkpoint is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer)) { walkPointSet = true; }
    }
    virtual public void Chase()
    {
        //Animation trigger
        MovementAnimation(true);

        //Enemy looks at player
        transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        //Enemy goes towards the player
        navMeshAgent.SetDestination(Player.transform.position);
    }
    virtual public void AnimationTrigger(string animation)
    {
        //Plays enemy animation
        anim.SetTrigger(animation);
    }
    virtual public void MovementAnimation(bool isMoving)
    {
        //changes movement variable based on bool
        int speedVariable = isMoving ? 1 : 0;
        anim.SetFloat("Speed", speedVariable);
    }
    virtual public void Attacking()
    {
        //The enemy aims at the player
        transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        float distanceToTarget = Vector3.Distance(Player.transform.position, transform.position);
        if (distanceToTarget <= attackDistance)
        {
            MovementAnimation(false);

            //Start the attack timer
            if (!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(TimedAttack());
            }
        }
        else
        {
            isAttacking = false;
        }
    }
    virtual public IEnumerator TimedAttack()
    {
        yield return new WaitForSeconds(attackTimer);
        PlaySound(attackSound, soundPosition);
        AnimationTrigger(attack);
        isAttacking = false;
    }
    virtual public void RandomAttackVariations(string attack1, string attack2)
    {
        int random = Random.Range(0, 2);
        if (random == 0) attack = attack1; else attack = attack2;
    }
    virtual public IEnumerator Die(string animation)
    {
        //Stops the enemy movement
        navMeshAgent.SetDestination(this.transform.position);
        AnimationTrigger(animation);
        PlaySound(deathSound, soundPosition);
        yield return new WaitForSeconds(2);
        Instantiate(Coin, transform.position + new Vector3(0, 1, 0), transform.rotation);
        Destroy(this.gameObject);
    }
    virtual public void ReturnFromHurtState()
    {
        currentState = States.Chasing;
    }
    virtual public void Dying()
    {
        if (triggerDeathAnimation)
        {
            StartCoroutine(Die(die));
            triggerDeathAnimation = false;
        }
    }
    //virtual public void Hurting()
    //{
    //    if (triggerHurtAnimation)
    //    {
    //        triggerHurtAnimation = false;
    //        StartCoroutine(Hurt());
    //    }
    //}
    //virtual public IEnumerator Hurt()
    //{
    //    navMeshAgent.SetDestination(this.transform.position);
    //    PlaySound(hurtSound, soundPosition);
    //    AnimationTrigger("TakeDamage");
    //    yield return new WaitForSeconds(.7f);
    //    currentState = States.Chasing;
    //}
    virtual public void OnCollisionEnter(Collision collision)
    {
        //Enemy hurts player on collision
        if (collision.gameObject.tag == "Player")
        {
            //Player loses health
            Player.GetComponent<PlayerHealthScript>().takeDamage(1);
        }

        if (collision.gameObject.tag == "Projectile")
        {
            //Gets the damage modifier from the current gun
            int gunDmg = (int)collision.gameObject.GetComponent<Projectille>().dmg;
            PlaySound(hurtSound, soundPosition);

            //INSERT Damage modifier from GUNS
            GetDamage(gunDmg);

            if (this.Health <= 0) { currentState = States.Death; }
        }


        ////Projectile hurts enemy on collision when not in hurting nor Death state
        //if (currentState != States.Hurt && currentState != States.Death)
        //{
        //    if (canBeHurt)
        //    {
        //        if (collision.gameObject.tag == "Projectile")
        //        {
        //            //Gets the damage modifier from the current gun
        //            int gunDmg = (int)collision.gameObject.GetComponent<Projectille>().dmg;

        //            //INSERT Damage modifier from GUNS
        //            GetDamage(gunDmg);

        //            if (this.Health <= 0) { canBeHurt = false; currentState = States.Death; }
        //            else { currentState = States.Hurt; }
        //        }
        //    }
        //}


    }
    virtual public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Player loses health
            Player.GetComponent<PlayerHealthScript>().takeDamage(Damage);
        }

        if (other.gameObject.tag == "Projectile")
        {
            //Gets the damage modifier from the current gun
            int gunDmg = (int)other.gameObject.GetComponent<Projectille>().dmg;
            PlaySound(hurtSound, soundPosition);

            //INSERT Damage modifier from GUNS
            GetDamage(gunDmg);

            if (this.Health <= 0) { currentState = States.Death; }
        }

        //Projectile hurts enemy on collision when not in hurting nor Death state
        //if (currentState != States.Hurt && currentState != States.Death)
        //{
        //    if (canBeHurt)
        //    {
        //        if (other.gameObject.tag == "Projectile")
        //        {
        //            //Gets the damage modifier from the current gun
        //            int gunDmg = (int)other.gameObject.GetComponent<Projectille>().dmg;
        //            PlaySound(hurtSound, soundPosition);
        //            AnimationTrigger("TakeDamage");
        //            //INSERT Damage modifier from GUNS
        //            GetDamage(gunDmg);

        //            if (this.Health <= 0) { canBeHurt = false; currentState = States.Death; }
        //            else { currentState = States.Hurt; }
        //        }
        //    }
        //}

    }
    public virtual void PlaySound(string soundPath, Vector3 position)
    {
        FMODUnity.RuntimeManager.PlayOneShot(soundPath, position);
    }
    public virtual void EnableParticles()
    {
        if (Tier > 1) particles.Play();
    }
}
