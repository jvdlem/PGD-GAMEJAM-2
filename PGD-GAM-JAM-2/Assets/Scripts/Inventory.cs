using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] 
    GameObject[] listOfAttachements;
    
    [SerializeField] 
    Text[] slots = new Text[4];

    public static GameObject[] attachements = new GameObject[4];

    public LayerMask layer;

    int currentSlot = 0;

    public void Update()
    {
        //Pick up/drop item based on key input
        if (Input.GetKeyDown(KeyCode.E)) PickUp();
        else if (Input.GetKeyDown(KeyCode.Q)) Drop();

        SetNewSlot();
    }

    private void PickUp()
    {
        //Raycast hit and object hit by raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Determine found object
        if (Physics.Raycast(ray, out RaycastHit hit, layer))
        {
            if (hit.rigidbody != null) 
            {
                GameObject foundObject = hit.rigidbody.gameObject; //Set object found

                for (int i = 0; i < listOfAttachements.Length; i++)
                {
                    if (foundObject.tag == listOfAttachements[i].tag) 
                    {
                        PutInInventory(foundObject, currentSlot); // If attachement, put in inventory   
                    }
                }
            }
        }
    }

    private void SetNewSlot() 
    {
        if (attachements[currentSlot] != null)
        {
            currentSlot++;
        }
        else if (attachements[currentSlot] == null)
        {
            currentSlot--;
        }

        if (currentSlot > attachements.Length - 1)
        {
            currentSlot = attachements.Length - 1;
        }
        else if (currentSlot < 0) 
        {
            currentSlot = 0;
        }
    }

    private void PutInInventory(GameObject attachement, int slot) 
    {
        if (attachements[slot] == null)
        {
            attachements[slot] = attachement; //Fill attachement
            ChangeText(slot, attachement.tag); //Fill slot
        }
    }

    private void Drop() 
    {
        ClearInventory(currentSlot); // Remove attachement from inventory
    }

    private void ClearInventory(int slot) 
    {
        if (attachements[slot] != null) 
        {
            Instantiate(attachements[slot], transform.position, transform.rotation); //Attachement is "dropped"
            attachements[slot] = null; //Clear attachement
            ChangeText(slot, "Empty"); //Clear slot
        }
    }

    private void ChangeText(int slot, string value) 
    {
        slots[slot].text = value;
    }
}