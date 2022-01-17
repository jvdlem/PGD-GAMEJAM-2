using UnityEngine;

public class DragonController : MeleeFlyingEnemyScript
{

    [SerializeField]
    public Animator anim;

    private bool attackTrigger;
    private bool deathTrigger;

    private bool coinDropped;

    public override void Start()
    { 
        base.Start();

        Tier = 1;

        Health = -1;
        Damage = 10;

        currentState = States.Chasing; //Enemy starts chasing
    }

    private void PlayAnimation(string animation)
    {
        anim.SetTrigger(animation);
    }

    public virtual void PlaySound(string soundPath, Vector3 position)
    {
        FMODUnity.RuntimeManager.PlayOneShot(soundPath, position);
    }

    public override void Update()
    {
        base.Update();

        if (currentState == States.Attacking) 
        {          
            PlayAnimation("Attack");
            attackTrigger = true;
        }
        else { PlayAnimation("Idle"); }

        if (Health <= 0) 
        {
            PlayAnimation("Die");

            if (!coinDropped) 
            {
                Instantiate(Coin, transform.position, transform.rotation);
                coinDropped = true;
            }

            deathTrigger = true;
        }

        if (attackTrigger) 
        {
            PlaySound("event:/Enemy/Bat/BatAttack", transform.position);
            attackTrigger = false;
        }

        if (deathTrigger)
        {
            PlaySound("event:/Enemy/Bat/BatDeath", transform.position);
            deathTrigger = false;
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        transform.position = new Vector3(other.gameObject.transform.position.x,
            25, other.gameObject.transform.position.z); //Reset position above target

        currentState = States.Patrolling; //Start patrolling for new target

        //If collision with player, damage player health
        if (other.gameObject.tag == "Player") { Player.GetComponent<PlayerHealthScript>().takeDamage(3); }

        //Lose health if hit by projectile
        if (other.gameObject.tag == "Projectile") { TakeDamage(1); }
    }
}
