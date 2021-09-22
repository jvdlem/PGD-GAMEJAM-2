using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigitbody;
    [SerializeField] private float timer = 10;
    [SerializeField] private float Speed = 500;
    public float dmg = 1;
    public Bullet(float damage)
    {
        dmg += damage;
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

    public void Setdmg(float damage)
    {
        dmg += damage;
    }
}
