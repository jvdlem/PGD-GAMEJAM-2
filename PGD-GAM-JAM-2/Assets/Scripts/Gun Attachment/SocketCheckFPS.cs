using System;
using UnityEngine;
using UnityEngine.UI;

public class SocketCheckFPS : MonoBehaviour
{

    [SerializeField]
    GameObject[] sockets = new GameObject[4];

    [SerializeField] 
    Text[] slots = new Text[4];

    string[] playerInput = { "1", "2", "3", "4" }; // List of key inputs

    public void Update()
    {
        foreach (string i in playerInput)
        {
            if (Input.GetKeyDown(i)) 
            {
                GetFromInventory(i);
            }
        }       
    }

    void GetFromInventory(string key) 
    {
        GameObject[] attachements = Inventory.attachements;

        int slot = Convert.ToInt32(key); // Convert string to int
        int currentSlot = slot - 1; // Change value to match array index

        // If equal and not empty, attach to socket
        if (attachements[currentSlot] != null)
        {
            Attach(currentSlot);
        }
    }

    void Attach(int slot) 
    {
        GameObject attachement = Inventory.attachements[slot];

        Rigidbody rb = attachement.GetComponent<Rigidbody>();
        rb.useGravity = false;

        Instantiate(attachement, sockets[slot].transform.position, sockets[slot].transform.rotation);

        Inventory.attachements[slot] = null;
        slots[slot].text = "Empty";
    }
}