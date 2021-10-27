using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigitbody;
    [SerializeField] private float timer = 0;
    [SerializeField] private float Speed = 0;
    public float dmg = 0;
    public Bullet(float damage,float time,float speed)
    {
        this.timer += time;
        this.dmg += damage;
        this.Speed += speed;
    }
    private void Awake()
    {
        myRigitbody.AddRelativeForce(new Vector3(0,0, Speed));
    }
    private void Update()
    {
        timer-= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetStats(float damage, float timer, float speed)
    {
        dmg = damage;
        this.timer = timer;
        this.Speed = speed;
    }
}
