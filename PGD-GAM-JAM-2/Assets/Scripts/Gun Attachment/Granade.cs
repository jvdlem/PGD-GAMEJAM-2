using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : Projectille
{
    List<ParticleSystem> myparticles = new List<ParticleSystem>();
    [SerializeField] private ParticleSystem Sparks;
    [SerializeField] private ParticleSystem Smoke;
    [SerializeField] private ParticleSystem explosion;
    private float delay = 3;
    private float scale;
    private float Timer;
    [SerializeField]private bool onCollision = false;
    private float radius = 30;
    private bool canExplode = true;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        myparticles.Add(Sparks);
        myparticles.Add(Smoke);
        myparticles.Add(explosion);
        Timer = delay;
    }

    public void UpdateScale(float explosionSize, bool OnHit, int add)
    {

        if (add >= 1)
        {
                Sparks.startSize *= explosionSize;
                Sparks.startSpeed *= explosionSize;
                Sparks.gravityModifier *= explosionSize;
                ParticleSystem.ForceOverLifetimeModule force = Sparks.forceOverLifetime;
                force.yMultiplier *= explosionSize;
                Smoke.startSize *= explosionSize;
                Smoke.startSpeed *= explosionSize;
                ParticleSystem.ForceOverLifetimeModule force2 = Smoke.forceOverLifetime;
                force2.yMultiplier *= explosionSize;
                explosion.startSize *= explosionSize;
                explosion.startSpeed *= explosionSize;
                radius *= explosionSize;
        }
        if(add <= -1)
        {
            Sparks.startSize /= explosionSize;
            Sparks.startSpeed /= explosionSize;
            Sparks.gravityModifier /= explosionSize;
            ParticleSystem.ForceOverLifetimeModule force = Sparks.forceOverLifetime;
            force.yMultiplier /= explosionSize;
            Smoke.startSize /= explosionSize;
            Smoke.startSpeed /= explosionSize;
            ParticleSystem.ForceOverLifetimeModule force2 = Smoke.forceOverLifetime;
            force2.yMultiplier /= explosionSize;
            explosion.startSize /= explosionSize;
            explosion.startSpeed /= explosionSize;
            radius /= explosionSize;
        }
        onCollision = OnHit;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0 && canExplode == true)
        {
            Sparks.startDelay = delay;
            Smoke.startDelay = delay;
            explosion.startDelay = delay;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Grenade Launcher/Explosion/Grenade Explosion", this.gameObject.transform.position);
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
            canExplode = false;
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.GetComponent<EnemyBaseScript>() != null)
                {
                    hitCollider.GetComponent<EnemyBaseScript>().TakeDamage((int)(radius / Vector3.Distance(this.transform.position, hitCollider.transform.position)));
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (onCollision)
        {
            delay = 0;
            Timer = 0;
            canExplode = true;
        }
    }


}
