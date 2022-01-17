using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class AttachmentStatsUi : MonoBehaviour
{
    [SerializeField] public Canvas attachmentStatsUi;
    public int currentState;
    public List<float> statList;
    private GameObject controller;
    public GameObject gun;
    [SerializeField] Image sprite;
    public Text[] TstatList;
    [Header("Stats")]
    [SerializeField] float spread;
    [SerializeField] float amountOfBullets;
    [SerializeField] float damage;
    [SerializeField] float range;
    [SerializeField] Text setText;





    public void Update()
    {

        controller = this.gameObject.GetComponent<XRDirectInteractor>().selectTarget.gameObject;
        if (controller.GetComponent<AttachmentStats>() != null && gameObject != gun)
        {
            state(default);
            getStats();
            showStats();
        }
        
     

        currentState = default;
    }
    public void state(int state)
    {
        
        switch (state)
        {
            case 1:
                attachmentStatsUi.GetComponent<Canvas>().enabled = true;
                break;
           
            default:
                attachmentStatsUi.GetComponent<Canvas>().enabled = false;
                break;
        }
    }
    public void getStats()
    {

        statList = controller.GetComponent<AttachmentStats>().statList;
        sprite.GetComponent<Image>().sprite = controller.GetComponent<AttachmentUIPart>().getImage();
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
                    if (statList[5] == 1) { setText.GetComponent<Text>().text = "Minigun"; }
                    break;
                case 1:
                    if (statList[6] == 1) { setText.GetComponent<Text>().text = "Grenade"; }
                    break;
                case 2:
                    if (statList[7] == 1) { setText.GetComponent<Text>().text = "Shotgun"; }
                    break;
                case 3:
                    if (statList[8] == 1) { setText.GetComponent<Text>().text = "Sniper"; }
                    break;
            }
        }
    }
}
