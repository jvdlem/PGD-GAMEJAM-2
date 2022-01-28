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

    float attackDistance = 2.5f, attackTimer, idleTimer, deathTimer, distance;
    int rushDistance = 10, rushSpeed = 11, idleSpeed = 0, walkSpeed = 7, playerDistCheck = 100, speed, maxHealth;
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
        slider.value = CalculateHealth(); //set the health bar slider to the health of the goblin.
        if (Health < maxHealth) healthBarUI.SetActive(true); //enables the slider once the goblin loses so hitpoints.
        if (Health <= 0) currentGoblinState = GoblinStates.Death; //if the goblin has no more health. Dies.
        navMeshAgent.speed = speed; //sets the speed of the navMeshAgent to the goblin speed. The goblin speed changes depending on if the goblin is walking or rushing.
        base.Update();

        //contains the switchcase
        #region Switchcase
        switch (currentGoblinState)
        {
            case GoblinStates.Idle:
                Idle();
                if (distance <= playerDistCheck && distance > rushDistance) currentGoblinState = GoblinStates.Following;
                break;
            case GoblinStates.Following:
                Following();
                if (distance > attackDistance && distance <= rushDistance) currentGoblinState = GoblinStates.Rushing;
                break;
            case GoblinStates.Rushing:
                Rush();
                if (distance <= attackDistance) currentGoblinState = GoblinStates.Attacking;
                break;
            case GoblinStates.Attacking:
                Attacking();
                if (distance > attackDistance) currentGoblinState = GoblinStates.Following;
                break;
            case GoblinStates.Death:
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
        speed = idleSpeed;
        anim.Play("Idle");
        idleTimer += Time.deltaTime;
        if (idleTimer > 8)
        {
            idleTimer = 0;
            PlaySound(idleSound, this.gameObject.transform.position);
        }
    }

    void Following()
    {
        speed = walkSpeed;
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
        anim.Play("Walk");
    }

    private void Rush()
    {
        speed = rushSpeed;
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
        anim.Play("Run");
        PlaySound(windupSound, this.gameObject.transform.position);
    }

    void Attacking()
    {
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
        speed = idleSpeed;

        attackTimer += Time.deltaTime;
        if (attackTimer > 1.5f)
        {
            attemptAttack = true;
            attackTimer = 0;
        }
    }

    void Death()
    {
        goblinDied = true;
        anim.Play("Die");
        speed = idleSpeed;
        navMeshAgent.SetDestination(this.transform.position);
        PlaySound(deathSound, this.gameObject.transform.position);
        deathTimer += Time.deltaTime;
        if (deathTimer > 1.65f)
        {
            deathTimer = 0;

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
            if (collision.gameObject.GetComponent<Projectille>() != null)
            {
                PlaySound(hurtSound, this.gameObject.transform.position);
                int dmg = (int)collision.gameObject.GetComponent<Projectille>().dmg;

                Health -= dmg;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        //collision for normal bullets
        if (collision.gameObject.tag == "Projectile" && !goblinDied)
        {
            if (collision.gameObject.GetComponent<Projectille>() != null)
            {
                PlaySound(hurtSound, this.gameObject.transform.position);
                int dmg = (int)collision.gameObject.GetComponent<Projectille>().dmg;

                Health -= dmg;
            }
        }
    }
    #endregion
}
