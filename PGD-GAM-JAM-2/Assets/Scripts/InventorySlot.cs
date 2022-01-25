using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    readonly GameObject[] attachements = new GameObject[10]; //List of attachements
    readonly string[] tags = new string[10]; //List of attachement tags

    new string tag; //Stores tag of picked-up attachement

    /// <summary>
    /// Pick up item and store it in slot.
    /// </summary>
    /// <param name="item">Item to be stored.</param>
    public void PickUpItem(GameObject item) 
    {
        for (int i = 0; i < tags.Length; i++) 
        {
            //Tag of attachement is compared to list of known tags
            if (item.transform.CompareTag(tags[i])) 
            {
                tag = item.transform.tag; //If matching, slot gets attachement tag
                Destroy(item); //Item is "picked up" and stored in slot
            }
        }
    }

    /// <summary>
    /// Drop item and clear slot.
    /// </summary>
    /// <param name="item">Item to be dropped.</param>
    public void DropItem(GameObject item) 
    {
        for (int i = 0; i < tags.Length; i++) 
        {
            for (int j = 0; j < attachements.Length; j++) 
            {
                if (attachements[j].transform.tag == tags[i]) 
                {
                    tag = ""; //Clear tag if not empty

                    //Item is "dropped" back into scene
                    Instantiate(item, gameObject.transform.position, gameObject.transform.rotation); 
                }
            }
        }
    }
}
