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
    public Canvas attachmentCanvas;
    public GameObject gun;
    [SerializeField] Image[] sprite;
    public Text[] TstatList;
    [Header("Stats")]
    [SerializeField] float spread;
    [SerializeField] float amountOfBullets;
    [SerializeField] float damage;
    [SerializeField] float range;



    public void Start()
    {
        
    }

    public void Update()
    {
        
        if (this.gameObject.GetComponent<XRGrabInteractable>().selectingInteractor != null)
        {
            controller = this.gameObject.GetComponent<XRGrabInteractable>().selectingInteractor.gameObject;
            attachmentCanvas = controller.GetComponentInChildren<Canvas>();
            TstatList = controller.GetComponentsInChildren<Text>();
            sprite = controller.GetComponentsInChildren<Image>();
        }




        //state(default);
    }
    public void state(int state)
    {
        currentState = state;
        switch (state)
        {
            case 1:
                
                    getStats();
                    showStats();
                    attachmentCanvas.GetComponent<Canvas>().enabled = true;

                break;
           
            case 0:
                attachmentCanvas.GetComponent<Canvas>().enabled = false;
                break;
        }
    }
    public void getStats()
    {

        statList = this.GetComponent<AttachmentStats>().statList;
        sprite[1].sprite = this.GetComponent<AttachmentUIPart>().getImage();
        spread = statList[0];
        amountOfBullets = statList[1];
        damage = statList[2];
        range = Mathf.Abs(statList[3] * statList[4]);
    }
    public void showStats()
    {
        TstatList[0].GetComponent<Text>().text = "Spread = " + spread;
        TstatList[1].GetComponent<Text>().text = "Bullets = " + amountOfBullets;
        TstatList[2].GetComponent<Text>().text = "Damage = " + damage;
        TstatList[3].GetComponent<Text>().text = "Range = " + range;
        
        for (int i = 5; i < statList.Count; i++)
        {
            switch (i - 5)
            {
                case 0:
                    if (statList[5] == 1) { TstatList[5].GetComponent<Text>().text = "Minigun"; }
                    break;
                case 1:
                    if (statList[6] == 1) { TstatList[5].GetComponent<Text>().text = "Grenade"; }
                    break;
                case 2:
                    if (statList[7] == 1) { TstatList[5].GetComponent<Text>().text = "Shotgun"; }
                    break;
                case 3:
                    if (statList[8] == 1) { TstatList[5].GetComponent<Text>().text = "Sniper"; }
                    break;
            }
        }
    }
}
