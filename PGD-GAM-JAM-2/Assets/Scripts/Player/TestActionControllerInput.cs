using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestActionControllerInput : MonoBehaviour
{
    [SerializeField]private GameObject Bullet;
    private ActionBasedController controller;
    private float movment;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
        movment = controller.positionAction.action.ReadValue<float>();
        bool isPressed = controller.selectAction.action.ReadValue<bool>();
        bool ispulled = controller.activateAction.action.ReadValue<bool>();
        controller.positionAction.action.performed += TuchPad;
        controller.activateAction.action.performed += Squees;
        controller.activateAction.action.performed += Trigger;
    }
    private void Squees(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }
    private void TuchPad(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }
    private void Trigger(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Instantiate(Bullet, this.transform.position+(transform.forward*0.5f),this.transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
