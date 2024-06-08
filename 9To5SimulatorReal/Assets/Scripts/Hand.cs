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

    private GameBehaviour game;

    [SerializeField]
    private bool carry = false;

    void OnTriggerEnter(Collider other) {

        // Highlight when interactable in reach
        if (other.gameObject.CompareTag("Interactable") && !carry) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.green);
        }

        // Highlight when special in reach
        if (other.gameObject.CompareTag("Special") && !game.inShift) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.yellow);
        }

    }

    void OnTriggerExit(Collider other) {

        // Undo highlight when out of reach
        if (other.gameObject.CompareTag("Interactable") || other.gameObject.CompareTag("Special")) {
            resetRender();
        }

    }

    void OnTriggerStay(Collider other) {

        // Picking stuff up
        if (other.gameObject.CompareTag("Interactable") && input.action.ReadValue<float>() == 1.0f && !carry)
        {
            this.gameObject.GetComponent<FixedJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
            carry = true;

        } else if (other.gameObject.CompareTag("Interactable") && input.action.ReadValue<float>() == 0.0f && carry) {

            this.gameObject.GetComponent<FixedJoint>().connectedBody = null;
            carry = false;
            
        }

        // startGame
        if (other.gameObject.CompareTag("Special") && input.action.ReadValue<float>() == 1.0f) {
            game.startShift();
            resetRender();
        }
    }

    void Start() {
        game = GameObject.Find("GameController").GetComponent<GameBehaviour>();
    }

    // Resets color of hands
    void resetRender() {
        Renderer handRender = this.gameObject.GetComponent<Renderer>();
        handRender.material.SetColor("_Color", Color.white);
    }

}
