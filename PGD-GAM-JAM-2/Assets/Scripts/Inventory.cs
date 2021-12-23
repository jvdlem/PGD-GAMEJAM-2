using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] GameObject[] listOfAttachements;
    [SerializeField] Text[] slotText = new Text[4];
    public static GameObject[] attachements = new GameObject[4];

    int slot = 0;

    public void Update()
    {
        //Pick up/drop item based on key input
        if (Input.GetKey("e")) PickUp();
        else if (Input.GetKey("q")) Drop();
    }

    public void PickUp()
    {
        //Raycast hit and object hit by raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool objectHit = Physics.Raycast(ray, out RaycastHit hit);
        GameObject foundObject;

        //Determine found object
        if (hit.rigidbody != null)
        {
            foundObject = hit.rigidbody.gameObject;

            //If hit object equal to attachement from list, "pick up" hit object
            if (attachements[slot] == null)
            {
                if (objectHit && foundObject == listOfAttachements[slot])
                {
                    attachements[slot] = foundObject;
                    slotText[slot].text = foundObject.name;
                    slot++;
                }
            }
        }
    }

    public void Drop() 
    {

        int prevSlot = slot - 1;

        if (attachements[prevSlot] != null)
        {
            Instantiate(attachements[prevSlot], transform.position, transform.rotation); //Attachement is "dropped"
            attachements[prevSlot] = null; //Clear attachement
            slotText[prevSlot].text = "";
            slot--;
        }
    }
}