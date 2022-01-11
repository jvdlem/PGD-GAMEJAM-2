using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] GameObject[] listOfAttachements;
    [SerializeField] Text[] slots = new Text[4];

    GameObject[] attachements = new GameObject[4];

    public LayerMask layer;

    int currentSlot = 0;

    public GameObject[] Attachements 
    {
        get { return attachements; }
    }

    public void Update()
    {
        //Pick up/drop item based on key input
        if (Input.GetKey(KeyCode.E)) PickUp();
        else if (Input.GetKey(KeyCode.Q)) Drop();

        SetNewSlot();
    }

    public void PickUp()
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

    public void PutInInventory(GameObject attachement, int slot) 
    {
        if (attachements[slot] == null)
        {
            attachements[slot] = attachement; //Fill attachement
            slots[slot].text = attachement.tag; //Fill slot
        }
    }

    public void Drop() 
    {
        ClearInventory(currentSlot); // Remove attachement from inventory
    }

    public void ClearInventory(int slot) 
    {
        if (attachements[slot] != null) 
        {
            Instantiate(attachements[slot], transform.position, transform.rotation); //Attachement is "dropped"
            attachements[slot] = null; //Clear attachement
            slots[slot].text = "Empty"; //Clear slot
        }
    }
}