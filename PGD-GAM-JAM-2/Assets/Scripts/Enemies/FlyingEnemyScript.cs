using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyScript : EnemyBaseScript
{
    protected Vector3 velocity; //Velocity for movement
    protected Vector3 target; //Target to specify direction

    [SerializeField] protected float flyingSpeed = 0.05f; //Sets movement speed

    //Timer for movement
     protected int moveTimer = 0;
    [SerializeField] protected int timerMax = 150;

    //Object to set as target
    [SerializeField] protected GameObject targetObject;

    //private float radius = 5;
    //Vector3 center = Vector3.zero;

    // Start is called before the first frame update
    public override void Start()
    {
        transform.position = new Vector3(0, 10); //Spawn above ground       
    }

    // Update is called once per frame
    public override void Update()
    {
        transform.position += velocity; //Velocity tied to position
    }

    ///<summary>Give the velocity a specified target vector</summary>
    virtual protected Vector3 FlyTo(Vector3 targetVector, float speed) 
    {
        velocity = targetVector * speed;

        return velocity;
    }

    virtual protected Quaternion SetRotation(Vector3 newRotation) 
    {
        transform.rotation = Quaternion.LookRotation(newRotation);

        return transform.rotation;
    }

    ///<summary>Generates a random target vector</summary>
    virtual protected Vector3 SetTarget() 
    {
        target = new Vector3(
                Random.Range(-flyingSpeed, flyingSpeed),
                0,
                Random.Range(-flyingSpeed, flyingSpeed));

        return target;
    }

    /// <summary>Handles use of movement timer</summary>
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

    /// <summary>Track object based on its position</summary>
    virtual protected Vector3 TrackObject(GameObject targetObject) 
    {
        target = new Vector3(targetObject.transform.position.x, 0 , targetObject.transform.position.z) - 
            new Vector3(transform.position.x, 0, transform.position.z);
        target.Normalize();

        return target;
    }

    /*
    public void Circle() 
    {
        float angle = Mathf.PI * 2 / moveTimer;

        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        transform.position = center + new Vector3(x, 0, z);

        float angleDegrees = -angle * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angleDegrees, 0);
    }
    */
}
