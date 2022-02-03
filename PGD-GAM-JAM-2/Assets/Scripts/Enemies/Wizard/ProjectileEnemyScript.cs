using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyScript : Moenemies
{
    // Start is called before the first frame update
    [SerializeField] Transform shootPoint;
    public bool projectileEnemyDeath = false;
    public float bulletSpeed, bulletLife;
    public Rigidbody projectile;
    public float spellDelay;
    override public void Start()
    {
        base.Start();
        attackSound = "event:/Enemy/Wizard/Wizard Attack";
        deathSound = "event:/Enemy/Wizard/WizardDeath";
        hurtSound = "event:/Enemy/Wizard/WizardHurt";
        attack = "Cast";
        currentState = States.Chasing;
    }
    override public void Update()
    {
        soundPosition = this.gameObject.transform.position;
        base.Update();
    }
    public override void SearchRandomWalkPoint()
    {
        //Determine a random point in the Golems detection range 
        float randomZ = Random.Range(-detectionDistance * 1.5f, detectionDistance * 1.5f);
        float randomX = Random.Range(-detectionDistance * 1.5f, detectionDistance * 1.5f);

        walkPoint = new Vector3(this.gameObject.transform.position.x + randomX, this.gameObject.transform.position.y, this.gameObject.transform.position.y + randomZ);

        //Check if walkpoint is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer)) { walkPointSet = true; }
    }
    void Cast()
    {
        //Sound
        Rigidbody bullet = (Rigidbody)Instantiate(projectile, shootPoint.position, transform.rotation);
        bullet.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

        Destroy(bullet.gameObject, bulletLife);
    }
    public override IEnumerator TimedAttack()
    {
        PlaySound(attackSound, soundPosition);
        AnimationTrigger(attack);
        yield return new WaitForSeconds(spellDelay);
        Cast();
        yield return new WaitForSeconds(attackTimer);
        isAttacking = false;
    }
}
