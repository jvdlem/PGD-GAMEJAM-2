using UnityEngine;

public class FlyingEnemyScript : EnemyBaseScript
{
    protected Vector3 target; //Target to specify direction

    [SerializeField] protected float flyingSpeed = 5; //Sets movement speed

    //Timer for movement
     protected int moveTimer = 0;
    [SerializeField] protected int timerMax = 150;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        Rigidbody.useGravity = false; //Turn off gravity for proper movement      
    }

    /// <summary>Fly in specified direction.</summary>
    virtual protected Vector3 FlyTo(Vector3 targetVector, float speed) 
    {
        velocity = targetVector * speed;

        return target;
    }

    /// <summary>Sets object rotation.</summary>
    virtual protected Quaternion SetRotation(Vector3 targetRotation) 
    {
        transform.rotation = Quaternion.LookRotation(targetRotation);

        return transform.rotation;
    }

    ///<summary>Generates a random target vector.</summary>
    virtual protected Vector3 SetTarget() 
    {
        target = new Vector3(
                Random.Range(-flyingSpeed, flyingSpeed),
                0,
                Random.Range(-flyingSpeed, flyingSpeed));

        return target;
    }

    /// <summary>Handles use of movement timer.</summary>
    virtual protected int TimeManager() 
    {
        moveTimer++; //Timer keeps counting

        if (moveTimer > timerMax)
        {
            moveTimer = 0; //Reset timer
            SetTarget(); //Set new target/direction
        }

        return moveTimer;
    }

    /// <summary>Track object based on its position.</summary>
    virtual protected Vector3 Track(Vector3 targetVector) 
    {
        target = new Vector3(targetVector.x, 0 , targetVector.z) - 
            new Vector3(transform.position.x, 0, transform.position.z);
        target.Normalize();

        return target;
    }
}
