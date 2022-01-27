using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForLava : MonoBehaviour
{
    private float lavaTimer = 0;
    private float hurtTimer = 3;
    private float rayDistance = 2f;
    private bool lavaHurts=true;
    [SerializeField]private PlayerHealthScript playerHP;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit ray;
        Physics.Raycast(transform.position, Vector3.down, out ray, rayDistance);

        if(ray.collider.gameObject.tag == "Lava")
        {
            CheckLava();
        }
    }

    void CheckLava()
    {

        if (lavaHurts)
        {
            //add what happens if the player enters lava here
            //Burn Sound
            Debug.Log("LAVA");
            playerHP.takeDamage(1);
            lavaHurts = false;
        }
        else
        {
        lavaTimer += Time.deltaTime;
        }

        if (lavaTimer > hurtTimer) { lavaHurts = true; lavaTimer = 0; }
    }
}
