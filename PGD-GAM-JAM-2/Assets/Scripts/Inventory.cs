using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] GameObject[] listOfAttachements;
    [SerializeField] Text[] slotText = new Text[4];
    GameObject[] attachements = new GameObject[4];

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
            for (int i = 0; i < listOfAttachements.Length; i++)
            {
                for (int j = 0; j < attachements.Length; j++) 
                {
                    if (attachements[j] == null)
                    {
                        if (objectHit && foundObject == listOfAttachements[i])
                        {
                            attachements[j] = foundObject;
                            slotText[j].text = foundObject.name;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void Drop() 
    {
        for (int j = 0; j < attachements.Length; j++) 
        {
            if (attachements[j] != null)
            {
                Instantiate(attachements[j], transform.position, transform.rotation); //Attachement is "dropped"
                attachements[j] = null; //Clear attachement
                slotText[j].text = "";
                break;
            }
        }
    }
}