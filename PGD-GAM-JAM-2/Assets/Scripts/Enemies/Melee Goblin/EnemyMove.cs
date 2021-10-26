using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : GroundEnemyScript
{
    [SerializeField] GameObject player;

    [SerializeField] int trackDistance = 20; //tracking distance of the goblin vs the player.
    [SerializeField] int rushDistance = 8;

    // Start is called before the first frame update
    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Playboy");
    }

    // Update is called once per frame
    public override void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        float step = WalkSpeed * Time.deltaTime; // calculate distance to move

        if (dist < trackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            transform.LookAt(player.transform);
            if (dist < rushDistance)
            {
                step = (WalkSpeed * 2) * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

            }
        }
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Health -= 1;
            Destroy(collision.gameObject);
        }
    }
}
