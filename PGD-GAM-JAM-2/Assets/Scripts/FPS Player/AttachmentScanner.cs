using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentScanner : MonoBehaviour
{
    [SerializeField] private GameObject Inventory;
    [SerializeField] private GameObject pressText;
    [SerializeField] public playerAimScript aimingScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int layer_mask = LayerMask.GetMask("Interactible");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layer_mask) && !aimingScript.aiming)
        {
            pressText.SetActive(true);
            if (Input.GetKeyDown("e"))
            {
                Inventory.GetComponent<InventoryPlayer>().AddAttachment(hit.transform.gameObject, hit.transform.tag);
            }
        }
        else
        {
            pressText.SetActive(false);
        }
    }
}
