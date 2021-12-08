using System;
using UnityEngine;

public class SocketCheckFPS : MonoBehaviour
{

    string[] inventorySlots = { "1", "2", "3", "4" };
    int slot;

    void Update()
    {
        foreach (string i in inventorySlots) 
        {
            HandleInput(i);
            break;
        }
    }

    void HandleInput(string input) 
    {
        if (Input.GetKey(input))
        {
            slot = Convert.ToInt32(input);

            //if (Inventory.attachements[slot] != null) 
            //{
            //    Attach(Inventory.attachements[slot]);
            //}
        }
    }

    void Attach(GameObject _object) 
    {
        Instantiate(_object, transform.position, transform.rotation);
        //Inventory.attachements[slot] = null;
    }
}
