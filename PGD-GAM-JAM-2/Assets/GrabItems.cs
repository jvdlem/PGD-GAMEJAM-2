using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabItems : MonoBehaviour
{
    LayerMask interactibles;
    [SerializeField] List<GameObject> inventory = new List<GameObject>();
    public SocketCheck socketCheckBarrel;
    private void Start()
    {
        interactibles = LayerMask.GetMask("Interactible");
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30f, interactibles))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                addItems(hit.collider.gameObject);
            }
        }
    }

    public void addItems(GameObject i)
    {
        inventory.Add(i);
    }
}