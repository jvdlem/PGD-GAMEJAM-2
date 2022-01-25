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

    private bool attackTriggered = false; //Set if dragon is attacking
    private bool deathTriggered = false; //Set if dragon is dying

    private bool coinDropped; //Set if coin has been dropped

    public override void Start()
    { 
        base.Start();

        healthBarUI.SetActive(false);

        animationTime = 3; //Set default length of animations

        //Assign dragon parameters
        Tier = 1;
        Health = 100;
        Damage = 2;

        currentState = States.Chasing; //Enemy starts chasing
    }

    /// <summary>
    /// Play dragon animation based on trigger.
    /// </summary>
    /// <param name="animation">Animation to be played.</param>
    /// <param name="isTriggered">Trigger used.</param>
    private void PlayAnimation(string animation, bool isTriggered = false)
    {
        if (!isTriggered) anim.SetTrigger(animation);
    }

    /// <summary>
    /// Play dragon sound based on trigger.
    /// </summary>
    /// <param name="soundPath">Soundeffect to be played.</param>
    /// <param name="position">Position where sound is coming from.</param>
    /// <param name="isTriggered">Trigger used.</param>
    public virtual void PlaySound(string soundPath, Vector3 position, bool isTriggered)
    {
        if (!isTriggered) FMODUnity.RuntimeManager.PlayOneShot(soundPath, position);
    }

    /// <summary>Calculates dragon health.</summary>
    float CalculateHealth() { return (float)Health / (float)maxHealth; }

    public override void Update()
    {
        base.Update();

        //Calculate and display dragon health
        slider.value = CalculateHealth();
        if (Health < maxHealth) { healthBarUI.SetActive(true); }

        //Play sound and start animation if dragon starts to attack
        if (currentState == States.Attacking)
        {
            PlaySound("event:/Enemy/Bat/BatAttack", transform.position, attackTriggered);
            PlayAnimation("Attack");
            attackTriggered = true;
        }

        //Dragon dies if health less than 0
        if (Health <= 0) { currentState = States.Death; }

        //Play sound and start animation if dragon dies
        if (currentState == States.Death) 
        {
            PlaySound("event:/Enemy/Bat/BatDeath", transform.position, deathTriggered);
            PlayAnimation("Die", deathTriggered);

            velocity = Vector3.zero; //Dragon stops moving

            Invoke(nameof(Despawn), 2.5f); //Wait with despawning until animation has been played

            deathTriggered = true;
        }

        //If in range for collision, despawn
        if (CollisionRange()) 
        {
            healthScript.takeDamage(2); //Player takes damage
            Despawn(); 
        }
    }

    public void Despawn() 
    {
        Destroy(gameObject);
       
        //If dragon dies, drop coin
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

    /// <summary>Checks if dragon is in range for collision.</summary>
    private bool CollisionRange() { return Physics.CheckSphere(transform.position, 0.5f, playerLayer); }
}
