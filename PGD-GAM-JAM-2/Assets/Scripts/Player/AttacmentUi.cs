using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttacmentUi : MonoBehaviour
{

    [SerializeField] private float minDistanceFromAttachment = 5f;
    LayerMask attachments;
    private int AmountHit = 0;
    [SerializeField] public Canvas attachmentStatsUi;
    public int currentState;
    public List<float> statList;
    public Text[] statListUI;
    [SerializeField] int amountofRays;
    [Header("CurrentAttachment")]
    [SerializeField] GameObject currentobject;
    [SerializeField] Image sprite;
    [Header("Stats")]
    [SerializeField] float spread;
    [SerializeField] float amountOfBullets;
    [SerializeField] float damage;
    [SerializeField] float range;
    [SerializeField] Text setText;
    [SerializeField] playerAimScript aimingScript;
    public GameObject gun;


    private void Start()
    {
        attachments = LayerMask.GetMask("Interactible");

        currentState = default;

    }
    private void Update()
    {

        Vector3 pos = transform.position;
        AmountHit = 0;

        state();

        for (int x = -amountofRays; x <= amountofRays; x++)
        {
            for (int y = -amountofRays; y <= amountofRays; y++)
            {
                Ray ray = new Ray(pos + new Vector3(x * 0.1f, y * 0.07f, 0), transform.TransformDirection(Vector3.forward));
                RaycastHit hit;
                Debug.DrawRay(pos + new Vector3(x * 0.1f, y * 0.07f, 0), transform.TransformDirection(Vector3.forward) * minDistanceFromAttachment, Color.yellow);

                if (Physics.Raycast(ray, out hit, minDistanceFromAttachment, attachments) )
                {
                    currentobject = hit.rigidbody.gameObject;
                    currentState = 1;
                    AmountHit++;
                }
                
            }
        }
        if (AmountHit == 0) { currentState = default; }
    }
    public void state()
    {
        switch (currentState)
        {
            case 1:
                if (currentobject.GetComponent<AttachmentStats>() != null && !aimingScript.aiming && gun != null && gun.GetComponent<Pistol>().isInMenu == false)
                {
                    getStats();
                    showStats();
                    attachmentStatsUi.GetComponent<Canvas>().enabled = true;
                }
                break;


            default:
                attachmentStatsUi.GetComponent<Canvas>().enabled = false;
                break;
        }
    }
    public void getStats()
    {

        
            statList = currentobject.GetComponent<AttachmentStats>().statList;
            sprite.GetComponent<Image>().sprite = currentobject.GetComponent<AttachmentUIPart>().getImage();
            spread = statList[0];
            amountOfBullets = statList[1];
            damage = statList[2];
            range = Mathf.Abs(statList[3] * statList[4]);
        
       
    }
    public void showStats()
    {
        statListUI[0].GetComponent<Text>().text = "Spread = " + spread;
        statListUI[1].GetComponent<Text>().text = "Bullets = " + amountOfBullets;
        statListUI[2].GetComponent<Text>().text = "Damage = " + damage;
        statListUI[3].GetComponent<Text>().text = "Range = " + range;
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
