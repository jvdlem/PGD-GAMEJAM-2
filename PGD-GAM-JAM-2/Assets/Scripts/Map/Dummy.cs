using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummy : MonoBehaviour
{
    [SerializeField] private GameObject myText;
    private float elapsedTime;
    [SerializeField] private float desiredDuration = 3f;
    private float currentScale;
    private bool gotHit;
    private Vector3 targetScale;
    [SerializeField] Color startColor = Color.white;
    [SerializeField] Color targetColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gotHit)
        {
            elapsedTime += Time.deltaTime;
            float percantageComplete = elapsedTime / desiredDuration;
            myText.transform.localPosition = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(1, 10, 1), percantageComplete);
            myText.GetComponent<Text>().color = Color.Lerp(startColor, targetColor, percantageComplete);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Projectille>() != null)
        {
            GetDamage(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            if (other.gameObject.GetComponent<Projectille>() != null)
            {
                GetDamage(other.gameObject);
            }
        }
    }

    public void GetDamage(GameObject collision)
    {
        elapsedTime = 0;
        myText.GetComponent<Text>().text = collision.gameObject.GetComponent<Projectille>().dmg.ToString("#.00;#.00");
        wobble(collision.gameObject.GetComponent<Projectille>().dmg);
        collision.gameObject.GetComponent<Projectille>().Speed = 0;
    }

    private void wobble(float intensity)
    {
        gotHit = true;
    }
}
