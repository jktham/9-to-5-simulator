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
        if (other.gameObject.CompareTag("Interactable") && !carry && checkHands(other)) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.green);
            inRange = other;
        }

        // Highlight when special in reach
        if (other.gameObject.CompareTag("Special") && !game.inShift && checkHands(other)) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.yellow);
            inRange = other;
        }

    }

    void OnTriggerExit(Collider other) {

        // Undo highlight when out of reach
        if (other.gameObject.CompareTag("Interactable") || other.gameObject.CompareTag("Special")) {
            resethand();
        }

    }

    void OnTriggerStay(Collider other) {

        // Picking stuff up
        if (other.gameObject.CompareTag("Interactable") && input.action.ReadValue<float>() == 1.0f 
            && !carry && inRange == other)
        {
            this.gameObject.GetComponent<FixedJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();

            carry = true;

        } else if (other.gameObject.CompareTag("Interactable") && input.action.ReadValue<float>() == 0.0f && carry) {

            this.gameObject.GetComponent<FixedJoint>().connectedBody = null;
            carry = false;
            
        }

        // startGame
        if (other.gameObject.CompareTag("Special") && input.action.ReadValue<float>() == 1.0f && inRange == other) {
            game.startShift();
            resethand();
        }
    }

    void Start() {
        game = GameObject.Find("GameController").GetComponent<GameBehaviour>();
        otherHandControl = otherHand.GetComponent<Hand>();
    }

    // Resets color of hands
    void resethand() {
        Renderer handRender = this.gameObject.GetComponent<Renderer>();
        handRender.material.SetColor("_Color", Color.white);
        inRange = null;
    }

    bool checkHands(Collider other) {
        return otherHandControl.inRange == null || (otherHandControl.inRange != other);
    }

}
