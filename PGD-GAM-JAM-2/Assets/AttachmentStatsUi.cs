using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class AttachmentStatsUi : MonoBehaviour
{
    [SerializeField] public GameObject attachmentStatsUi;
    public int currentState;
    public List<float> statList;
    private GameObject controller;
    private Canvas attachmentCanvas;
    [SerializeField] Image[] sprite;
    public Text[] statListUI;
    [Header("Stats")]
    [SerializeField] float spread;
    [SerializeField] float amountOfBullets;
    [SerializeField] float damage;
    [SerializeField] float range;




    public void Update()
    {
        //Get all the components needed that are attached to the controller
        if (this.gameObject.GetComponent<XRGrabInteractable>().selectingInteractor != null)
        {
            controller = this.gameObject.GetComponent<XRGrabInteractable>().selectingInteractor.gameObject;
            attachmentCanvas = controller.GetComponentInChildren<Canvas>();
            statListUI = controller.GetComponentsInChildren<Text>();
            sprite = controller.GetComponentsInChildren<Image>();
        }

    }
    public void state(int state)
    {
        currentState = state;
        switch (state)
        {
            case 1:
                //When in this case it means the attachment has been picked up by a controller First we get the stats from the attachment and show them on the controller UI
                    getStats();
                    showStats();
                    attachmentCanvas.GetComponent<Canvas>().enabled = true;

                break;
           
            case 0:
                //When in this State we turn of the UI of the controller (default state)
                attachmentCanvas.GetComponent<Canvas>().enabled = false;
                break;
        }
    }
    public void getStats()
    {
        // get our current attachments stats
        statList = this.GetComponent<AttachmentStats>().statList;
        sprite[1].sprite = this.GetComponent<AttachmentUIPart>().getImage();
        spread = statList[0];
        amountOfBullets = statList[1];
        damage = statList[2];
        range = Mathf.Abs(statList[3] * statList[4]);
    }
    public void showStats()
    {
        //show our current attachments stats
        statListUI[0].GetComponent<Text>().text = "Spread = " + spread;
        statListUI[1].GetComponent<Text>().text = "Bullets = " + amountOfBullets;
        statListUI[2].GetComponent<Text>().text = "Damage = " + damage;
        statListUI[3].GetComponent<Text>().text = "Range = " + range;
        
        for (int i = 5; i < statList.Count; i++)
        {
            //check what SET this attachment belongs to and show it.
            switch (i - 5)
            {
                //TO DO add matching colors for each set to the UI?
                case 0:
                    if (statList[5] == 1) { statListUI[5].GetComponent<Text>().text = "Minigun"; }
                    break;
                case 1:
                    if (statList[6] == 1) { statListUI[5].GetComponent<Text>().text = "Grenade"; }
                    break;
                case 2:
                    if (statList[7] == 1) { statListUI[5].GetComponent<Text>().text = "Shotgun"; }
                    break;
                case 3:
                    if (statList[8] == 1) { statListUI[5].GetComponent<Text>().text = "Sniper"; }
                    break;
            }
        }
    }
}
