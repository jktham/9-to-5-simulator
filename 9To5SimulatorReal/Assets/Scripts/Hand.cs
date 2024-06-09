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
    private GameObject otherHand;

    private GameBehaviour game;
    private Hand otherHandControl;

    public Collider carry;
    public Collider inRange;

    void OnTriggerEnter(Collider other) {

        // Highlight when interactable in reach
        if (other.gameObject.CompareTag("Interactable") && carry == null) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.green);
            inRange = other;
        }

        // Highlight when special in reach
        if (other.gameObject.CompareTag("Special") && !game.inShift && carry == null) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.yellow);
            inRange = other;
        }

    }

    void OnTriggerExit(Collider other) {

        // Reset Hand
        if ((other.gameObject.CompareTag("Interactable") || other.gameObject.CompareTag("Special")) 
                && inRange == other) {
            resethand();
        }

    }

    void Start() {
        game = GameObject.Find("GameController").GetComponent<GameBehaviour>();
        otherHandControl = otherHand.GetComponent<Hand>();
    }

    void Update() {

        // Remove doubly carried stuff
        if (bothHandsSame()) carry = null;
        // synchronize carry with inRange
        if (carry != null && inRange == null) inRange = carry;
    }

    void FixedUpdate() {

        // INTERACTION LOGIC
        if (input.action.ReadValue<float>() == 1.0f && inRange != null && !carry) {
        // button pressed

            if (inRange.gameObject.CompareTag("Special")) {
            // ACTIVATE GAME

                game.startShift();
                resethand();
            
            } else if (inRange.gameObject.CompareTag("Interactable")) {
            // GRAB THING

                carry = inRange;
                this.gameObject.GetComponent<FixedJoint>().connectedBody = carry.gameObject.GetComponent<Rigidbody>();
            
            }

        } else if (input.action.ReadValue<float>() == 0.0f && carry != null) {
        // button released

            carry = null;
            this.gameObject.GetComponent<FixedJoint>().connectedBody = null;
            
        }
    }

    // Resets hand
    void resethand() {
        Renderer handRender = this.gameObject.GetComponent<Renderer>();
        handRender.material.SetColor("_Color", Color.white);
        inRange = null;
    }

    bool bothHandsSame() {
        return carry != null && (otherHandControl.carry == carry);
    }

}
