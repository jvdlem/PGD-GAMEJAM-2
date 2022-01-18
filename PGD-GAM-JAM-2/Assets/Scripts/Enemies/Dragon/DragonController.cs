using UnityEngine;
using UnityEngine.UI;

public class DragonController : MeleeFlyingEnemyScript
{

    [Header("Animation")]
    [SerializeField]
    public Animator anim;

    [Header("Health")]
    public int maxHealth;
    public GameObject healthBarUI;
    public Slider slider;

    private bool attackTriggered = false;
    private bool deathTriggered = false;

    private bool coinDropped;

    public override void Start()
    { 
        base.Start();

        healthBarUI.SetActive(false);

        Tier = 1;

        Health = 100;
        Damage = 2;

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

    float CalculateHealth()
    {
        return (float)Health / (float)maxHealth;
    }

    public override void Update()
    {
        base.Update();

        slider.value = CalculateHealth();

        if (Health < maxHealth) { healthBarUI.SetActive(true); }

        if (currentState == States.Attacking) 
        {
            PlaySound("event:/Enemy/Bat/BatAttack", transform.position, attackTriggered);
            PlayAnimation("Attack");
            attackTriggered = true;
        }

        if (Health <= 0) { currentState = States.Death; }

        if (currentState == States.Death) 
        {
            PlaySound("event:/Enemy/Bat/BatDeath", transform.position, deathTriggered);
            PlayAnimation("Die");

            velocity = Vector3.zero;

            Invoke(nameof(Despawn), 3f);

            deathTriggered = true;
        }
    }

    private void Despawn() 
    {
        Destroy(gameObject);

        //Drop coin
        if (!coinDropped)
        {
            Instantiate(Coin, transform.position, transform.rotation);
            coinDropped = true;
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
