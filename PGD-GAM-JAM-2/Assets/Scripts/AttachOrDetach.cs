using UnityEngine;
using UnityEngine.UI;

public class AttachOrDetach : MonoBehaviour
{

    [SerializeField] Text currentSlot;
    [SerializeField] Text targetSlot;

    private bool hasBeenReplaced = false;

    private void Update()
    {
        ChangeSlot();
        if (hasBeenReplaced) ClearSlot();
    }

    private void ChangeSlot()
    {
        if (enabled && !hasBeenReplaced) 
        {
            targetSlot.text = currentSlot.text;
            currentSlot.text = targetSlot.text;
            hasBeenReplaced = true;
        }
    }

    private void ClearSlot() 
    {
        currentSlot.text = "Empty";
    }
}
