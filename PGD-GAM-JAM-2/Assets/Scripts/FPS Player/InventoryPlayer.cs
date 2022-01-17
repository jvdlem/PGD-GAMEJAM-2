using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryPlayer : MonoBehaviour
{
    private int selectedAttachemnt;
    public GameObject gun;

    public List<GameObject> pistolList;
    public List<GameObject> inventoryList;

    private int gunslots = 4;
    private GameObject aSlot;
    [SerializeField] private GameObject images;
    [SerializeField] private GameObject currentSelect;
    [SerializeField] private GameObject subInventory;
    private bool isActive = false;
    private bool swaped = false;

    [SerializeField] public Text coinText;

    // Update is called once per frame
    private void Start()
    {
        for (int i = 0; i < gunslots; i++)
        {
            pistolList.Add(aSlot);
            inventoryList.Add(aSlot);
        }
    }
    void Update()
    {
        coinText.text = "" + FindObjectOfType<PlayerHealthScript>().coins;

        StopCoroutine(timer());
        if (Input.GetKeyDown("q"))
        {

            isActive = !isActive;
            Cursor.visible = isActive;
            gun.GetComponent<Pistol>().isInMenu = isActive;

            subInventory.SetActive(isActive);
            if (isActive == false)
            {

                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void AddAttachment(GameObject aAttachment, string aTag)
    {
        switch (aTag)
        {
            case "Barrel":
                if (inventoryList[0] == null)
                {
                    inventoryList[0] = aAttachment;
                    images.transform.GetChild(0).GetComponent<Image>().sprite = aAttachment.GetComponent<AttachmentUIPart>().getImage();
                    aAttachment.SetActive(false);
                }
                break;
            case "Magazine":
                if (inventoryList[1] == null)
                {
                    inventoryList[1] = aAttachment;
                    images.transform.GetChild(1).GetComponent<Image>().sprite = aAttachment.GetComponent<AttachmentUIPart>().getImage();
                    aAttachment.SetActive(false);
                }
                break;
            case "Sight":
                if (inventoryList[2] == null)
                {
                    inventoryList[2] = aAttachment;
                    images.transform.GetChild(2).GetComponent<Image>().sprite = aAttachment.GetComponent<AttachmentUIPart>().getImage();
                    aAttachment.SetActive(false);
                }
                break;
            case "Stock":
                if (inventoryList[3] == null)
                {
                    inventoryList[3] = aAttachment;
                    images.transform.GetChild(3).GetComponent<Image>().sprite = aAttachment.GetComponent<AttachmentUIPart>().getImage();
                    aAttachment.SetActive(false);
                }
                break;
            default:
                break;
        }
    }

    public void SwapAttachment()
    {

                pistolList[selectedAttachemnt].SetActive(false);
                StartCoroutine(timer());
                ChangeImage();
                GameObject temp;
                
                temp = pistolList[selectedAttachemnt];
                pistolList[selectedAttachemnt].SetActive(false);
                pistolList[selectedAttachemnt].transform.position = inventoryList[selectedAttachemnt].transform.position;
                gun.GetComponent<Pistol>().aCurrentAddon = null;
                inventoryList[selectedAttachemnt].transform.position = gun.transform.GetChild(selectedAttachemnt + 1).position;
                inventoryList[selectedAttachemnt].SetActive(true);

                pistolList[selectedAttachemnt] = inventoryList[selectedAttachemnt];
                inventoryList[selectedAttachemnt] = temp;
                swaped = true;

    }
    public void Attach()
    {
        if (selectedAttachemnt <= pistolList.Count)
        {
            if (pistolList[selectedAttachemnt] != null && inventoryList[selectedAttachemnt] != null)
            {
                pistolList[selectedAttachemnt].SetActive(false);
                swaped = false;
                StartCoroutine(timer());
            }
            else
            {
                if (inventoryList[selectedAttachemnt] != null && pistolList[selectedAttachemnt] == null)
                {
                    ChangeImage();
                    pistolList[selectedAttachemnt] = inventoryList[selectedAttachemnt];
                    pistolList[selectedAttachemnt].transform.position = gun.transform.GetChild(selectedAttachemnt + 1).position;
                    pistolList[selectedAttachemnt].SetActive(true);
                    inventoryList[selectedAttachemnt] = null;
                }
            }

        }
    }

    public void Dettach()
    {
        if (selectedAttachemnt <= pistolList.Count)
        {
            if (pistolList[selectedAttachemnt] != null && inventoryList[selectedAttachemnt] == null)
            {
                ChangeImage();

                inventoryList[selectedAttachemnt] = pistolList[selectedAttachemnt];


                pistolList[selectedAttachemnt].SetActive(false);
                pistolList[selectedAttachemnt] = null;
            }
        }
    }

    public void SelectAttachment(int attachemntslot)
    {
        selectedAttachemnt = attachemntslot;
    }

    IEnumerator timer()
    {

        //returning 0 will make it wait 1 frame

        if (swaped == false)
        {
            yield return new WaitForSeconds(0.1f);
            swaped = true;
            SwapAttachment();
            
        }
        else
        {
            yield return new WaitForSeconds(0);
        }
        //code goes here


    }

    public void ChangeImage()
    {
        Sprite tempIMG;
        tempIMG = images.transform.GetChild(selectedAttachemnt).GetComponent<Image>().sprite;
        images.transform.GetChild(selectedAttachemnt).GetComponent<Image>().sprite = images.transform.GetChild(selectedAttachemnt + gunslots).GetComponent<Image>().sprite;
        images.transform.GetChild(selectedAttachemnt + gunslots).GetComponent<Image>().sprite = tempIMG;
    }
}
