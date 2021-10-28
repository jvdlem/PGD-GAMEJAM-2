using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : GroundEnemyScript
{
    [SerializeField] int rushDistance = 8;
    private bool playOnce;
  

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Health = 10;
        Tier = 1;
        checkForPlayerDistance = 20;
        WalkSpeed = 4;
        RotateSpeed = 8;
        AttackRange = 2;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        float dist = Vector3.Distance(Player.transform.position, transform.position);
        float step = WalkSpeed * Time.deltaTime; // calculate distance to move

        if (dist <= checkForPlayerDistance && dist >= AttackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - new Vector3(transform.position.x, transform.position.y - 90, transform.position.z)), RotateSpeed * Time.deltaTime);
            if (dist < rushDistance)
            {

                step = (WalkSpeed * 2) * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
                if (playOnce)
                {

                    FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Goblin/GoblinWindup", this.gameObject.transform.position);
                    playOnce = false;
                }

            }
            else playOnce = true;

        }
        if (Health <= 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Goblin/GoblinDeath", this.gameObject.transform.position);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Goblin/GoblinHurt", this.gameObject.transform.position);
            Health -= 1;
            Destroy(collision.gameObject);
            
            
           
        }
    }
  

}
