using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : Projectille
{
    List<ParticleSystem> myparticles = new List<ParticleSystem>();
    [SerializeField] private ParticleSystem Sparks;
    [SerializeField] private ParticleSystem Smoke;
    [SerializeField] private ParticleSystem exploson;
    private float delay = 3;
    private float scale;
    private float Timer;
    private bool onCollision;
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
        myparticles.Add(exploson);
        Timer = delay;
        foreach (ParticleSystem aParticle in myparticles)
        {
            aParticle.startDelay = delay;
        }
    }

    public void UpdateScale(float explosionSize, bool OnHit, int add)
    {
        if (add >= 1)
        {
            foreach (ParticleSystem particleSystem in myparticles)
            {
                particleSystem.startSize *= explosionSize;
                particleSystem.startSpeed *= explosionSize;
                radius *= explosionSize;
            }
        }
        else
        {
            foreach (ParticleSystem particleSystem in myparticles)
            {
                particleSystem.startSize /= explosionSize;
                particleSystem.startSpeed /= explosionSize;
                radius /= explosionSize;
            }
        }
        onCollision = OnHit;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0 && canExplode == true)
        {
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
        }
    }


}
