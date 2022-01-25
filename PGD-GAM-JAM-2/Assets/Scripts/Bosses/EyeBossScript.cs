using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBossScript : EnemyBaseScript
{
    private BossScript parentBoss;
    private GameObject player;
    public float lookSpeed = 3f;

    public new Renderer renderer;

    public bool EyeIsActive;

    override public void Start()
    {
        parentBoss = transform.parent.GetComponent<BossScript>();
        renderer = GetComponent<Renderer>();
        player = parentBoss.Player;
    }

    override public void Update()
    {
        float Distance = Vector3.Distance(transform.position, player.transform.position);
        //Look towards player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z)), lookSpeed * Time.deltaTime);

        //Change color depending on if Active
        if (EyeIsActive)
        {
            renderer.material.color = Color.white;
        }
        else
        {
            renderer.material.color = Color.black;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (EyeIsActive)
        {
            if (collision.gameObject.tag == "Projectile")
            {
                if (collision.gameObject.GetComponent<Projectille>())
                {
                    parentBoss.BossHealth -= (int)collision.gameObject.GetComponent<Projectille>().dmg;

                    if (parentBoss.BossHealth <= parentBoss.NextHealthTrigger)
                    {
                        parentBoss.BossHealth = parentBoss.NextHealthTrigger;
                        parentBoss.CycleToNextEye = true;
                    }

                    if (parentBoss.BossHealth <= 0)
                    {
                        parentBoss.CurrentBossState = BossScript.BossStates.DieState;
                    }
                }
            }
        }
    }
}
