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
                SetRotation(target); //Aim at target
                break;
            case States.Attacking:
                SetRotation(target); 
                navMeshAgent.SetDestination(Player.transform.position); //Dive at target
                break;
            case States.Chasing:
                TrackObject(targetObject); //Follow target from above
                break;
        }

        navMeshAgent.SetDestination(target); //Always fly in direction of target

        //Attack target when in range and chasing
        if (InRange() && currentState == States.Chasing) { currentState = States.Attacking; }
    }

    /// <summary>Charge at a specified target.</summary>
    protected Vector3 ChargeAt(GameObject targetObject) 
    {
        target = targetObject.transform.position - transform.position; //Position vectors directly compared to each other
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
