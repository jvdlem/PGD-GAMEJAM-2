using UnityEngine;

public class FlyingEnemyScript : EnemyBaseScript
{
    protected Vector3 target; //Target to specify direction

    [SerializeField] protected float flyingSpeed = 5; //Sets movement speed

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        Destroy(Rigidbody); //Remove rigidbody     
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

    /// <summary>Track object based on its position.</summary>
    virtual protected Vector3 Track(Vector3 targetVector) 
    {
        target = new Vector3(targetVector.x, 0 , targetVector.z) - 
            new Vector3(transform.position.x, 0, transform.position.z);
        target.Normalize();

        return target;
    }
}
