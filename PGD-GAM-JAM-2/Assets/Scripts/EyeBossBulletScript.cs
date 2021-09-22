using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBossBulletScript : MonoBehaviour
{
    private GameObject Target;
    private float LifeTime = 5;
    private float AliveTimer;
    private float Velocity = 10;

    private Vector3 moveDirection;

    private void Start()
    {
        Target = transform.parent.GetComponent<BossScript>().Player;
        moveDirection = (Target.transform.position - transform.position).normalized;
    }

    void Update()
    {
        transform.position += moveDirection * Velocity * Time.deltaTime;

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
