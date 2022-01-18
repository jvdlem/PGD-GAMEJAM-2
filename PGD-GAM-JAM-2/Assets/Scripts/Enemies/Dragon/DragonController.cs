using UnityEngine;

public class DragonController : MeleeFlyingEnemyScript
{

    [SerializeField]
    public Animator anim;

    private bool attackTriggered = false;
    private bool deathTriggered = false;

    private bool coinDropped;

    public override void Start()
    { 
        base.Start();

        Tier = 1;

        Health = 100;
        Damage = 10;

        currentState = States.Chasing; //Enemy starts chasing
    }

    private void PlayAnimation(string animation)
    {
        anim.SetTrigger(animation);
    }

    public virtual void PlaySound(string soundPath, Vector3 position, bool isTriggered)
    {
        if (!isTriggered) FMODUnity.RuntimeManager.PlayOneShot(soundPath, position);
    }

    public override void Update()
    {
        base.Update();

        if (currentState == States.Attacking) 
        {
            PlaySound("event:/Enemy/Bat/BatAttack", transform.position, attackTriggered);
            PlayAnimation("Attack");
            attackTriggered = true;
        }
        else { PlayAnimation("Idle"); }

        if (Health <= 0) 
        {
            PlaySound("event:/Enemy/Bat/BatDeath", transform.position, deathTriggered);
            PlayAnimation("Die");

            if (!coinDropped) 
            {
                Instantiate(Coin, transform.position, transform.rotation);
                coinDropped = true;
            }

            Invoke(nameof(Despawn), 3f);

            deathTriggered = true;
        }

        if (currentState != States.Attacking) { attackTriggered = false; }
        else if (Health > 0) { deathTriggered = false; }
    }

    private void Despawn() 
    {
        Destroy(gameObject);
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
