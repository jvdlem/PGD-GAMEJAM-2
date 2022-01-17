using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectille : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigitbody;
    [SerializeField] private float timer = 10;
    [SerializeField] public float Speed = 500;
    public float dmg = 1;
    [SerializeField] private float mytimer = 10;
    [SerializeField] private float mySpeed = 500;
    public float mydmg = 1;
    // Start is called before the first frame update

    public Projectille()
    {
    }
    protected virtual void Awake()
    {
        myRigitbody.AddRelativeForce(new Vector3(0, 0, Speed));
    }
    protected virtual void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Stats(float speed, float timer, float dmg)
    {
        this.timer = timer;
        this.Speed = speed;
        this.dmg = dmg;
        this.timer += mytimer;
        this.Speed += mySpeed;
        this.mydmg += mydmg;
        
    }
    public void Reset()
    {
        timer = mytimer;
        Speed = mySpeed;
        dmg = mydmg;
    }
}
