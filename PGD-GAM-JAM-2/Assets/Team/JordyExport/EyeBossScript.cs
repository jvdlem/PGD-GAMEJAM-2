using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBossScript : MonoBehaviour
{
    private BossScript parentBoss;
    private GameObject player;
    public new Animation animation;
    public float lookSpeed = 3f;

    public new Renderer renderer;

    public bool EyeIsActive;

    void Start()
    {
        parentBoss = transform.parent.GetComponent<BossScript>();
        renderer = GetComponent<Renderer>();
        animation = GetComponent<Animation>();
        player = parentBoss.Player;
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - new Vector3(transform.position.x, transform.position.y - 90, transform.position.z)), lookSpeed * Time.deltaTime);

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
                parentBoss.BossHealth -= 5;
                parentBoss.EyeHits++;
                if (parentBoss.BossHealth <= 0)
                {
                    parentBoss.bossState = 5;
                }
            }
        }
    }
}
