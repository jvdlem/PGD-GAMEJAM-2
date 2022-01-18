using UnityEngine;
using UnityEngine.UI;

public class DragonController : MeleeFlyingEnemyScript
{

    [Header("Player Info")]
    [SerializeField]
    PlayerHealthScript healthScript;

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

        animationTime = 3;

        Tier = 1;

        Health = 100;
        Damage = 2;

        currentState = States.Chasing; //Enemy starts chasing
    }

    private void PlayAnimation(string animation, bool isTriggered = false)
    {
        if (!isTriggered) anim.SetTrigger(animation);
    }

    public virtual void PlaySound(string soundPath, Vector3 position, bool isTriggered)
    {
        if (!isTriggered) FMODUnity.RuntimeManager.PlayOneShot(soundPath, position);
    }

    float CalculateHealth() { return (float)Health / (float)maxHealth; }

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
            PlayAnimation("Die", deathTriggered);

            velocity = Vector3.zero;

            Invoke(nameof(Despawn), 2.5f);

            deathTriggered = true;
        }

        //If in range for collision, despawn
        if (CollisionRange()) 
        {
            healthScript.takeDamage(2);
            Despawn(); 
        }
    }

    public void Despawn() 
    {
        Destroy(gameObject);
       
        if (!coinDropped) 
        {
            Instantiate(Coin, transform.position + new Vector3(0, 1, 0), transform.rotation);
            coinDropped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Lose health if hit by projectile
        if (other.gameObject.tag == "Projectile") { TakeDamage(1); }
    }

    private bool CollisionRange() { return Physics.CheckSphere(transform.position, 0.5f, playerLayer); }
}
