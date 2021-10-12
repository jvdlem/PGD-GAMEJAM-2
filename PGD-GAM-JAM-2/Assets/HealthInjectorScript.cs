using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthInjectorScript : MonoBehaviour
{
    PlayerHealthScript playerHealth;
    bool injectorActive = false;
    

    // Update is called once per frame
    void Update()
    {
        if (injectorActive == false) { gameObject.GetComponent<Renderer>().material.color = Color.red; }
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "XR Rig" && injectorActive) 
        {
            FindObjectOfType<PlayerHealthScript>().currentHealth += 3;
            Destroy(gameObject);
        }
    }
    public void inject() 
    {
        injectorActive = true;
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
 
}
