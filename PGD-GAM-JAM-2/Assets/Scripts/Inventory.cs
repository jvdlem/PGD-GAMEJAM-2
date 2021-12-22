using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject[] listOfAttachements;
    [SerializeField] Text[] slotText = new Text[4];
    [SerializeField] public inventoryHud InventoryHud;
    public static GameObject[] attachements = new GameObject[4];
    public bool openedInventory;
    Ray ray;
    public int slot = 0;
    public LayerMask InteractableMask;

    public virtual void Update()
    {
        //Raycast hit and object hit by raycast
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));


        //Pick up/drop item based on key input
        if (Input.GetKeyDown("e")) PickUp();
        else if (Input.GetKeyDown("q")) Drop();
        if (Input.GetKeyDown("i")) OpenInventory();
    }

    public void PickUp()
    {
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
        Debug.DrawRay(transform.position, hit.transform.position - transform.position);
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

    public void OpenInventory()
    {
        if (openedInventory)
        {
            InventoryHud.toggleHudOff();
            openedInventory = false;
        }
        else
        {
            InventoryHud.toggleHudOn();
            openedInventory = true;
        }

    }
}