using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private bool interactableInRange = false;
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

    void Start() {
        
    }
    void Update() {
        if (interactableInRange)
        {
            // Accept input to pick up stuff
        }
    }
}
