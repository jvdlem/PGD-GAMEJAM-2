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
        if (InRange()) { currentState = States.Attacking; }
    }

    /// <summary>Charge at a specified target.</summary>
    protected Vector3 ChargeAt(Vector3 targetVector) 
    {
        target = targetVector - transform.position; //Set target vector
        target.Normalize();

        return target;
    }

    /// <summary>Check if enemy is in range of target.</summary>
    protected bool InRange() { return Physics.CheckSphere(transform.position, radius, playerLayer); }
}
