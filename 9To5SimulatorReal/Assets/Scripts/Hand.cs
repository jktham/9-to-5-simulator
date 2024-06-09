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
    private SpeakerBehaviour speaker;

    public Collider carry;
    public Collider inRange;

    void OnTriggerEnter(Collider other) {

        // Highlight when interactable in reach
        if (other.gameObject.CompareTag("Interactable") && carry == null) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.green);
            inRange = other;
        }

        // Highlight when clock_in in reach
        if (other.gameObject.name.Equals("clock_in") && !game.inShift && carry == null) {
            Renderer handRender = this.gameObject.GetComponent<Renderer>();
            handRender.material.SetColor("_Color", Color.yellow);
            inRange = other;
        }

        // Highlight when clock_out in reach
        if (other.gameObject.name.Equals("clock_out") && game.inShift && carry == null) {
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
        speaker = GameObject.Find("speaker").GetComponent<SpeakerBehaviour>();
    }

    void FixedUpdate() {

        // Remove doubly carried stuff
        if (bothHandsSame())  {
            this.gameObject.GetComponent<FixedJoint>().connectedBody = null;
            carry = null;
        }

        // INTERACTION LOGIC
        if (input.action.ReadValue<float>() == 1.0f && inRange != null && !carry) {
        // button pressed

            if (inRange.gameObject.CompareTag("Special")) {
            // CLOCK IN OR CLOCK OUT

                if (inRange.gameObject.name.Equals("clock_in")) {

                    inRange.gameObject.GetComponent<AudioSource>().Play();
                    game.startShift();
                    resethand();

                } else if (inRange.gameObject.name.Equals("clock_out")) {

                    inRange.gameObject.GetComponent<AudioSource>().Play();
                    
                    if (!game.win) {

                        if (game.illegalClockOuts == 2) {
                            speaker.playSound(4,1);
                        } else if (game.illegalClockOuts >= 3) {
                            game.ending(Ending.fired);
                        } else {
                            speaker.playSound(3,1);
                        }
                        game.startIllegalClockOutTimer();

                    } else {

                        game.ending(Ending.success);

                    }
                    resethand();

                }
            
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

    // Check if both hands carry the same object
    bool bothHandsSame() {
        return carry != null && (otherHandControl.carry == carry);
    }

}
