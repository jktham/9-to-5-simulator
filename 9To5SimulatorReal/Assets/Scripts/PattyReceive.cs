using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PattyReceive : MonoBehaviour
{

    [SerializeField]
    Material basic;
    [SerializeField]
    Material correct;
    [SerializeField]
    Color correctLight;
    [SerializeField]
    Material wrong;
    [SerializeField]
    Color wrongLight;
    [SerializeField]
    float offdelay;
    [SerializeField]
    float responseChance;
    [SerializeField]
    int penaltyInSeconds;

    Renderer render;
    Light mylight;
    GameBehaviour game;
    SpeakerBehaviour speaker;

    bool firstMistake = false;
    bool lightswitching = true;


    void Start() {
        render = this.gameObject.GetNamedChild("Light").GetComponent<Renderer>();
        mylight = this.gameObject.GetNamedChild("Light").GetComponent<Light>();
        speaker = GameObject.Find("speaker").GetComponent<SpeakerBehaviour>();
        game = GameObject.Find("GameController").GetComponent<GameBehaviour>();
    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.name.Contains("patty")) {

            if (other.gameObject.GetComponent<PattyBehaviour>().state == PattyState.cooked) {
                isCorrect();
            } else {
                isWrong();
            }

        }

        Destroy(other.gameObject);

    }

    void isCorrect() {
        if (lightswitching) {
            StopAllCoroutines();
            render.material = correct;
            mylight.color = correctLight;
            mylight.enabled = true;
            StartCoroutine( TurnOff(offdelay) );
        }
    }

    void isWrong() {
        StopAllCoroutines();
        render.material = wrong;
        mylight.color = wrongLight;
        mylight.enabled = true;
        if (!firstMistake) {
            speaker.playSound(1,1);
            firstMistake = true;
        } else if (Random.Range(0.0f,1.0f) <= responseChance) {
            speaker.playSound(2,1);
        }
        game.wincondition += penaltyInSeconds;
        StartCoroutine( TurnOffWrong(offdelay) );
    }

    private IEnumerator TurnOff(float Length) {

        WaitForSeconds seconds = new WaitForSeconds(Length);
        yield return seconds;
        render.material = basic;
        mylight.enabled = false;

    }

    private IEnumerator TurnOffWrong(float Length) {

        lightswitching = false;
        WaitForSeconds seconds = new WaitForSeconds(Length);
        yield return seconds;
        render.material = basic;
        mylight.enabled = false;

    }
}
