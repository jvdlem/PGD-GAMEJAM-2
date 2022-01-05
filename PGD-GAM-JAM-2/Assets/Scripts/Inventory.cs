using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] Camera rayOrigin;

    [SerializeField] GameObject[] listOfAttachements;
    [SerializeField] Text[] slots = new Text[4];

    GameObject[] attachements = new GameObject[4];

    public LayerMask layer;

    int prevSlot = -1;
    int currentSlot = 0;

    public GameObject[] Attachements 
    {
        get { return attachements; }
    }

    public void Update()
    {
        //Pick up/drop item based on key input
        PickUp();
        Drop();
    }

    public void PickUp()
    {
        //Raycast hit and object hit by raycast
        Ray ray = rayOrigin.ScreenPointToRay(Input.mousePosition);

        //Determine found object
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.rigidbody != null) 
            {
                GameObject foundObject = hit.rigidbody.gameObject; //Set object found

                for (int i = 0; i < listOfAttachements.Length; i++) 
                {
                    if (Input.GetKey("e") && foundObject == listOfAttachements[i])
                    {
                        attachements[currentSlot] = foundObject; //Fill attachement
                        slots[currentSlot].text = foundObject.name; //Fill slot
                        currentSlot++;

                        prevSlot = currentSlot - 1;

                        Debug.Log("Picked up!");
                    }
                }
            }
        }
    }

    public void Drop() 
    {
        if (Input.GetKey("q") && attachements[currentSlot] != null)
        {
            Instantiate(attachements[currentSlot], transform.position, transform.rotation); //Attachement is "dropped"
            attachements[currentSlot] = null; //Clear attachement
            slots[currentSlot].text = ""; //Clear slot

            currentSlot = prevSlot;        
        }
    }
}