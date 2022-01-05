using System;
using UnityEngine;

public class SocketCheckFPS : MonoBehaviour
{

    [SerializeField]
    GameObject[] sockets = new GameObject[4];

    Inventory inventory = new Inventory();

    public void Update()
    {
        GetFromInventory(inventory.Attachements);
    }

    void GetFromInventory(GameObject[] attachements) 
    {
        string[] playerInput = { "1", "2", "3", "4" }; // List of key inputs

        foreach (string i in playerInput)
        {
            int slot = Convert.ToInt32(i); // Convert string to int
            int currentSlot = slot - 1; // Change value to match array index

            // If equal and not empty, attach to socket
            if (Input.GetKey(i) == sockets[currentSlot] && attachements[currentSlot] != null)
            {
                Attach(currentSlot);
                Debug.Log("attached!");
            }
        }
    }

    void Attach(int slot) 
    {
        Instantiate(inventory.Attachements[slot],
                        sockets[slot].transform.position, sockets[slot].transform.rotation);
    }
}