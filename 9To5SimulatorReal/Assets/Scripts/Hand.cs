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
    public TrackedPoseDriver carrycontrol;
    private bool interactableInRange = false;
    [SerializeField]
    private bool carry = false;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Interactable") && !carry) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.green);
            interactableInRange = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Interactable")) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.white);
            interactableInRange = false;
        }
    }

    void OnTriggerStay(Collider other) {

        // Code for grabbing stuff
        if (other.gameObject.CompareTag("Interactable") && !carry) {
            if (carrycontrol.trackingStateInput.action.triggered) {
                carry = true;
                inHand = other.gameObject;
                inHand.GetComponent<Rigidbody>().useGravity = false;
                inHand.transform.SetParent(this.gameObject.transform);
            }
        } else if (inHand != null && carry) {
            if (!carrycontrol.trackingStateInput.action.triggered) {
                inHand.transform.SetParent(null);
                inHand.GetComponent<Rigidbody>().useGravity = true;
                inHand = null;
                carry = false;
            }
        }
    }

    void Start() {
        
    }
    void Update() {
        if (interactableInRange)
        {
            // Accept input to pick up stuff
        }

    }
}
