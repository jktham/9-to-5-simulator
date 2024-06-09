using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum Ending
{
    fired, success, real
}
public class GameBehaviour : MonoBehaviour
{

    public int shiftInSeconds = 0;
    public int shiftInMinutes = 0;
    public int shiftInHours = 0;
    public int wincondition = 3600 * 8;

    public bool DebugMode = false;
    public bool win = false;
    public bool end = false;
    public Ending endingType;

    public AudioMixer mixer;

    [SerializeField]
    InputActionAsset input;

    [SerializeField]
    private bool switchStartShift = false;
    public bool inShift = false;

    public int illegalClockOuts = 0;
    [SerializeField]
    private float clockOutResetTime;

    SpeakerBehaviour speaker;

    protected void OnEnable() {
        input.Enable();
    }

    void Awake() {
        DontDestroyOnLoad(GameObject.Find("Player"));
    }

    void Start() {
        speaker = GameObject.Find("speaker").GetComponent<SpeakerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        // starts shift one time only!
        if (switchStartShift && !inShift)
        {
            if (!DebugMode) StartCoroutine( StartShift() );
            switchStartShift = false;
            inShift = true;
            GameObject.Find("PattySpawner").GetComponent<PattySpawn>().start();
            GameObject.Find("speaker").GetComponent<SpeakerBehaviour>().start();
        }

        // update both minutes and hours
        if (!DebugMode) shiftInMinutes = shiftInSeconds / 60;
        if (!DebugMode) shiftInHours = shiftInMinutes / 60;

        // wincondition
        if (shiftInSeconds >= wincondition) {
            win = true;
            speaker.playSound(6,1);
        }

        // Sound fadeout
        if (end) {
            GameObject.Find("Fridge").GetComponent<AudioSource>().volume -= 0.001f;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Interactable")) {
                if (obj.GetComponent<AudioSource>() != null) {
                    obj.GetComponent<AudioSource>().volume -= 0.001f;
                }
            }
        }

        // Debug instant Win Condition
        if (DebugMode && win) {
            ending(endingType);
            win = false;
        }
    }

    public void startShift() {
        switchStartShift = true;
    }

    public void startIllegalClockOutTimer() {
        StartCoroutine( IllegalClockOutReset(clockOutResetTime) );
    }

    public void ending(Ending type) {
        
        StopCoroutine( StartShift() );
        end = true;
        switch (type)
        {
            case Ending.fired :
                StartCoroutine(Fired());
                break;
            case Ending.success :
                StartCoroutine(Success());
                break;
        }

    }

    // Counter of seconds
    private IEnumerator StartShift() {

        WaitForSeconds second = new WaitForSeconds(1);
        while (true)
        {

            shiftInSeconds++;
            yield return second;

        }
    }

    // resets illegalClock timer
    private IEnumerator IllegalClockOutReset(float resetTime) {

        WaitForSeconds seconds = new WaitForSeconds(resetTime);
        yield return seconds;
        illegalClockOuts = 0;

    }

    // ENDING getting fired
    private IEnumerator Fired() {

        speaker.playSound(5,1);
        yield return new WaitForSeconds(15);
        Application.Quit();

    }

    // ENDING success
    private IEnumerator Success() {

        GameObject.Find("Player").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(44);
        Application.Quit();

    }

}
