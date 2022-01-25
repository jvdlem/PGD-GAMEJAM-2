using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : GroundEnemyScript
{
    #region variables
    private GoblinStates currentGoblinState;

    [SerializeField] Animator anim;
    public GameObject healthBarUI;
    public Slider slider;

    float attackDistance = 2.5f, AttackTimer, IdleTimer, DeathTimer, distance;
    int rushDistance = 10, rushSpeed = 11, idleSpeed = 0, walkSpeed = 7, playerDistCheck = 100, Speed, maxHealth;
    bool attemptAttack, goblinDied;
    public LayerMask groundLayer;
    string attackSound, deathSound, hurtSound, windupSound, idleSound;
    #endregion

    enum GoblinStates
    {
        Idle,
        Following,
        Rushing,
        Attacking,
        Death
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        //contains some misc variables from GroundEnemyScript
        #region miscVariables
        maxHealth = 10;
        currentGoblinState = GoblinStates.Idle;
        Tier = 1;
        RotateSpeed = 8;
        AttackRange = 0.5f;
        checkForPlayerDistance = playerDistCheck;
        Health = maxHealth;
        slider.value = CalculateHealth();
        #endregion

        //Initialise Sounds
        #region initialise sounds
        attackSound = "event:/Enemy/Goblin/GoblinAttack";
        deathSound = "event:/Enemy/Goblin/GoblinDeath";
        hurtSound = "event:/Enemy/Goblin/GoblinHurt";
        windupSound = "event:/Enemy/Goblin/GoblinWindup";
        idleSound = "event:/Enemy/Goblin/GoblinIdle";
        #endregion   
    }

    // Update is called once per frame
    public override void Update()
    {
        distance = Vector3.Distance(Player.transform.position, this.transform.position); //checks for distance between the goblin and the player.
        slider.value = CalculateHealth(); //set the hitpoint bar slider to the health of the goblin.
        if (Health < maxHealth) healthBarUI.SetActive(true); //enables the slider once the goblin loses so hitpoints.
        if (Health <= 0) currentGoblinState = GoblinStates.Death; //if the goblin has no more health. Dies.
        navMeshAgent.speed = Speed; //sets the speed of the navMeshAgent to the goblin speed. The goblin speed changes depending on if the goblin is walking or rushing.
        base.Update();

        //contains the switchcase
        #region Switchcase
        switch (currentGoblinState)
        {
            case GoblinStates.Idle:
                Debug.Log("idle");
                Idle();
                if (distance <= playerDistCheck && distance > rushDistance) currentGoblinState = GoblinStates.Following;
                break;
            case GoblinStates.Following:
                Debug.Log("follow");
                Following();
                if (distance > attackDistance && distance <= rushDistance) currentGoblinState = GoblinStates.Rushing;
                break;
            case GoblinStates.Rushing:
                Debug.Log("rush");
                Rush();
                if (distance <= attackDistance) currentGoblinState = GoblinStates.Attacking;
                break;
            case GoblinStates.Attacking:
                Debug.Log("attack");
                Attacking();
                if (distance > attackDistance) currentGoblinState = GoblinStates.Following;
                break;
            case GoblinStates.Death:
                Debug.Log("death");
                Death();
                break;
            default:
                break;
        }
        #endregion
    }

    //contains all the states
    #region states
    void Idle()
    {
        Speed = idleSpeed;
        anim.Play("Idle");
        IdleTimer += Time.deltaTime;
        if (IdleTimer > 8)
        {
            IdleTimer = 0;
            PlaySound(idleSound, this.gameObject.transform.position);
        }
    }

    void Following()
    {
        Speed = walkSpeed;
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
        anim.Play("Walk");
    }

    private void Rush()
    {
        Speed = rushSpeed;
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
        anim.Play("Run");
        PlaySound(windupSound, this.gameObject.transform.position);
    }

    void Attacking()
    {
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
        Speed = idleSpeed;

        AttackTimer += Time.deltaTime;
        if (AttackTimer > 1.5f)
        {
            attemptAttack = true;
            AttackTimer = 0;
        }
    }

    void Death()
    {
        goblinDied = true;
        anim.Play("Die");
        Speed = idleSpeed;
        navMeshAgent.SetDestination(this.transform.position);
        PlaySound(deathSound, this.gameObject.transform.position);
        DeathTimer += Time.deltaTime;
        if (DeathTimer > 1.65f)
        {
            DeathTimer = 0;

            Instantiate(Coin, transform.position + new Vector3(0, 1, 0), transform.rotation);
            Destroy(this.gameObject);
        }

    }
    #endregion

    //contains playsound and calculate health
    #region misc
    private void PlaySound(string sound, Vector3 pos)
    {
        FMODUnity.RuntimeManager.PlayOneShot(sound, pos);
    }

    float CalculateHealth()
    {
        return (float)Health / (float)maxHealth;
    }
    #endregion

    //contains all checks for collision
    #region collision checks
    private void OnTriggerEnter(Collider collision)
    {
        //if an attack is possible the goblin will attempt to attack
        if (attemptAttack)
        {
            anim.Play("Attack_01");
            PlaySound(attackSound, this.gameObject.transform.position);
            if (collision.gameObject.tag == "Player" && !goblinDied)
            {
                Player.GetComponent<PlayerHealthScript>().takeDamage(1);

            }
            attemptAttack = false;
        }

        //collision for sniper bullets
        if (collision.gameObject.tag == "Projectile" && !goblinDied)
        {
            PlaySound(hurtSound, this.gameObject.transform.position);
            int dmg = (int)collision.gameObject.GetComponent<Projectille>().dmg;
            Health -= dmg;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        //collision for normal bullets
        if (collision.gameObject.tag == "Projectile" && !goblinDied)
        {
            PlaySound(hurtSound, this.gameObject.transform.position);
            int dmg = (int)collision.gameObject.GetComponent<Projectille>().dmg;
            Health -= dmg;
        }
    }
    #endregion
}
