using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPhysics : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float threshold = .1f;
    [SerializeField] private float deadZone = 0.025f;

    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint myJoint;

    public UnityEvent onPressed, onReleased;
    void Start()
    {
        startPos = transform.localPosition;
        myJoint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPressed && GetValue() + threshold >= 1)
        {
            Pressed();

        }
        if (isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / myJoint.linearLimit.limit;
        if (Mathf.Abs(value) < deadZone)
        {
            value = 0;
        }
        return Mathf.Clamp(value, -1f,1f);
    }

    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
        FMODUnity.RuntimeManager.PlayOneShot("event:/MISC/ButtonPress", this.transform.position);
    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
    }


}
