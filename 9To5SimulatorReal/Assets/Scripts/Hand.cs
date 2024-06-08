using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class Hand : MonoBehaviour
{
    public GameObject inHand;
    public InputActionReference controllerActionInteract;
    [SerializeField]
    private bool inRange = false;
    private bool carry = false;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Interactable") && !carry) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.green);
            inHand = other.gameObject;
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Interactable")) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.white);
            inHand = null;
            inRange = false;
        }
    }

    void Start() {
        
    }
    void Update() {
        
        if (inHand != null && controllerActionInteract.action.triggered)
        {
            inHand.GetComponent<Rigidbody>().useGravity = false;
            inHand.transform.SetParent(this.gameObject.transform);
            carry = true;

        } else if (inHand != null && !controllerActionInteract.action.triggered) {

            inHand.GetComponent<Rigidbody>().useGravity = true;
            inHand.transform.SetParent(null);
            carry = false;
            
        }
    }
}
