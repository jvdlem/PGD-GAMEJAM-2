using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentScanner : MonoBehaviour
{
    [SerializeField] private GameObject Inventory;
    [SerializeField] private GameObject pressText;
    [SerializeField] public playerAimScript aimingScript;

    bool invActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q") && !invActive) invActive = true;
        else if (Input.GetKeyDown("q") && invActive) invActive = false;

        if (!invActive) {
            int layer_mask = LayerMask.GetMask("Interactible");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layer_mask) && aimingScript.allowPickUp)
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
}
