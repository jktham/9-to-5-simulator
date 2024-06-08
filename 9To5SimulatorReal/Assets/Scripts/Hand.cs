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

    public GameObject otherHand;

    private GameBehaviour game;
    private Hand otherHandControl;

    [SerializeField]
    private bool carry = false;
    private Collider inRange;

    void OnTriggerEnter(Collider other) {

        // Highlight when interactable in reach
        if (other.gameObject.CompareTag("Interactable") && !carry) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.green);
            inRange = other;
        }

        // Highlight when special in reach
        if (other.gameObject.CompareTag("Special") && !game.inShift && !carry) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.yellow);
            inRange = other;
        }

    }

    void OnTriggerExit(Collider other) {

        // Undo highlight when out of reach
        if ((other.gameObject.CompareTag("Interactable") || other.gameObject.CompareTag("Special")) && inRange == other) {
            resethand();
        }

    }

    void Start() {
        game = GameObject.Find("GameController").GetComponent<GameBehaviour>();
        otherHandControl = otherHand.GetComponent<Hand>();
    }

    void Update() {

        // Interaction
        if (input.action.ReadValue<float>() == 1.0f && inRange != null && !carry)
        {
            if (inRange.gameObject.CompareTag("Special")) {
                game.startShift();
                resethand();
            } else if (inRange.gameObject.CompareTag("Interactable")) {
                this.gameObject.GetComponent<FixedJoint>().connectedBody = inRange.gameObject.GetComponent<Rigidbody>();
                carry = true;
            }

        } else if (input.action.ReadValue<float>() == 0.0f && inRange != null && carry) {

            this.gameObject.GetComponent<FixedJoint>().connectedBody = null;
            carry = false;
            
        }
    }

    // Resets color of hands
    void resethand() {
        Renderer handRender = this.gameObject.GetComponent<Renderer>();
        handRender.material.SetColor("_Color", Color.white);
        inRange = null;
    }

    bool checkHands(Collider other) {
        return (otherHandControl.inRange == null && inRange == null) || (otherHandControl.inRange != other);
    }

}
