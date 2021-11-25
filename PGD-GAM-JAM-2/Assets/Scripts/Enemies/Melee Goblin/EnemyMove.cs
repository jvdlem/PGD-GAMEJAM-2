using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : GroundEnemyScript
{
    float AttackTimer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Health = 10;
        Tier = 1;
        checkForPlayerDistance = 20;
        WalkSpeed = 1.5f;
        RotateSpeed = 8;
        AttackRange = 2;
    }

    // Update is called once per frame
    public override void Update()
    {
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));

        base.Update();
        if (Health <= 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Goblin/GoblinDeath", this.gameObject.transform.position);
            Destroy(this.gameObject);
        }
        Debug.Log(WalkSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Goblin/GoblinHurt", this.gameObject.transform.position);
            Health -= 1;
            Destroy(collision.gameObject);
        }

        AttackTimer += Time.deltaTime;
        if (collision.gameObject.tag == "Player")
        {
            if (AttackTimer > 2.5f)
            {
                Player.GetComponent<PlayerHealthScript>().takeDamage(1);
           
            }
        }
    }
}