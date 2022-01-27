using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBossBulletScript : MonoBehaviour
{
    //Values for how the small laset should behave
    private GameObject Target;
    private float LifeTime = 5;
    private float AliveTimer;
    private float Velocity = 30;

    private Vector3 moveDirection;

    private void Start()
    {
        //Get the target and the direction of the target
        Target = transform.parent.GetComponent<BossScript>().Player;
        moveDirection = (Target.transform.position - transform.position).normalized;
    }

    void Update()
    {
        //Move towards the target position
        transform.position += moveDirection * Velocity * Time.deltaTime;

        //Timer that checks how long the bullet has been alive, destroys it if it is longer than LifeTime
        if (AliveTimer < LifeTime)
        {
            AliveTimer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
