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
    public InputActionReference input;

    [SerializeField]
    private bool carry = false;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Interactable") && !carry) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.green);
            inHand = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Interactable")) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.white);
            inHand = null;
        }
    }

    void Start() {
        Debug.Log(input.action.ReadValue<float>());
    }

    void Update() {

        if (input.action.ReadValue<float>() == 1.0f)
        {
            Debug.Log("Hello");
        }

        if (inHand != null && input.action.ReadValue<float>() == 1.0f)
        {
            inHand.GetComponent<Rigidbody>().isKinematic = true;
            inHand.transform.SetParent(this.gameObject.transform);
            carry = true;

        } else if (inHand != null && input.action.ReadValue<float>() != 1.0f) {

            inHand.GetComponent<Rigidbody>().isKinematic = false;
            inHand.transform.SetParent(null);
            carry = false;
            
        }
    }

}
