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

    //private float radius = 5;
    //Vector3 center = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 10); //Spawn above ground
        SetTarget(); //Set starting target/direction
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity; //Velocity tied to position
        moveTimer++; //Timer keeps counting

        if (moveTimer > timerMax) 
        {
            moveTimer = 0; //Reset timer
            SetTarget(); //Set new target/direction
        }

        FlyTo(target); //Fly in direction of target
    }

    ///<summary>Give the velocity a specified target vector</summary>
    virtual public Vector3 FlyTo(Vector3 targetVector) 
    {
        velocity = targetVector;
        transform.rotation = Quaternion.LookRotation(target);

        return velocity;
    }

    ///<summary>Generates a random target vector</summary>
    virtual public Vector3 SetTarget() 
    {
        target = new Vector3(
                Random.Range(-flyingSpeed, flyingSpeed),
                0,
                Random.Range(-flyingSpeed, flyingSpeed));

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
