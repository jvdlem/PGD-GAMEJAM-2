using UnityEngine;

public class DragonController : MeleeFlyingEnemyScript
{

    [SerializeField]
    public Animator anim;

    public override void Start()
    { 
        base.Start();

        Tier = 1;

        Health = 100;
        Damage = 10;

        currentState = States.Patrolling; //Enemy starts chasing
    }

    private void PlayAnimation(string animation)
    {
        anim.SetTrigger(animation);
    }

    public override void Update()
    {
        base.Update();

        if (currentState == States.Attacking) { PlayAnimation("Attack"); }
        else { PlayAnimation("Idle"); }

        if (Health < 0) PlayAnimation("Die");
    }

    protected void OnTriggerEnter(Collider other)
    {
        transform.position = new Vector3(other.gameObject.transform.position.x,
            25, other.gameObject.transform.position.z); //Reset position above target

        currentState = States.Patrolling; //Start patrolling for new target

        //If collision with player, damage player health
        if (other.gameObject.tag == "Player") { Player.GetComponent<PlayerHealthScript>().takeDamage(3); }
    }
}
