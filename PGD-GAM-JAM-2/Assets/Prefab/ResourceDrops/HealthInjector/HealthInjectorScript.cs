using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthInjectorScript : MonoBehaviour
{
    PlayerHealthScript playerHealth;
    bool injectorActive = false;
    //default value for heal = 3 value gets overwritten if changed in inspector dont worry about changing it here
    [SerializeField] int healAmount = 3;
    //TO DO Redo script with a switch

    
    void Update()
    {
        
        //injector is as default inactive so its color is red
        if (injectorActive == false) { gameObject.GetComponent<Renderer>().material.color = Color.red; }
    }
    public void OnTriggerEnter(Collider collision)
    {
        //if an active injector interacts with the player(XR RIG) then add health to the current health
        if (collision.gameObject.name == "XR Rig" && injectorActive) 
        {
            FindObjectOfType<PlayerHealthScript>().currentHealth += healAmount;
            Destroy(gameObject);
        }
    }
    public void inject() 
    {
        //if the player activates the injector (Done trough XR input) make the color green for feedback and activate the injector
        injectorActive = true;
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
 
}
