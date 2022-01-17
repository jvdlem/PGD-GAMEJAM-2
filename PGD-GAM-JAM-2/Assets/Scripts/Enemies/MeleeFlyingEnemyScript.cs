using UnityEngine;

public class MeleeFlyingEnemyScript : FlyingEnemyScript
{

    public LayerMask playerLayer;
    [SerializeField] private float radius;

    public override void Update()
    {
        base.Update();

        //Changes enemy behavior based on state
        switch (currentState) 
        {
            case States.Patrolling:
                TimeManager(); //Fly in random directions based on timer
                break;
            case States.Chasing:
                Track(target); //Follow target from above
                break;
            case States.Attacking:
                ChargeAt(target); //Charge at target
                break;
        }

        FlyTo(target, flyingSpeed); //Always fly in direction of target

        //Follow target if chasing or attacking
        if (currentState == States.Chasing || currentState == States.Attacking)
        {
            SetRotation(target); //Aim at target
            target = Player.transform.position; //Target is player position
        }

        //Attack target when in range and chasing
        if (InRange() && currentState == States.Chasing) { currentState = States.Attacking; }
    }

    /// <summary>Charge at a specified target.</summary>
    protected Vector3 ChargeAt(Vector3 targetVector) 
    {
        target = targetVector - transform.position; //Compare position vectors
        target.Normalize();

        return target;
    }

    //Checks if enemy is in range of target
    protected bool InRange() { return Physics.CheckSphere(transform.position, radius, playerLayer); }

    protected void OnTriggerEnter(Collider other)
    {
        transform.position = new Vector3(other.gameObject.transform.position.x,
            25, other.gameObject.transform.position.z); //Reset position above target

        currentState = States.Patrolling; //Start patrolling for new target

        //If collision with player, damage player health
        if (other.gameObject.tag == "Player") { Player.GetComponent<PlayerHealthScript>().takeDamage(3); }
    }
}
