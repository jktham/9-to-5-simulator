using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameBehaviour : MonoBehaviour
{

    public int shiftInSeconds = 0;
    public int shiftInMinutes = 0;
    public int shiftInHours = 0;
    public int wincondition = 3600 * 8;

    public bool DebugMode = false;

    [SerializeField]
    InputActionAsset input;

    [SerializeField]
    private bool switchStartShift = false;
    public bool inShift = false;
    public bool win = false;

    protected void OnEnable() {
        input.Enable();
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
        if (shiftInSeconds >= wincondition && GameObject.FindGameObjectsWithTag("Interactable").Length == 0)
        {
            win = true;
        }
    }

    public void startShift() {
        switchStartShift = true;
    }

    public void endShift() {
        StopCoroutine( StartShift() );
    }

    private IEnumerator StartShift() {

        WaitForSeconds second = new WaitForSeconds(1);
        while (true)
        {

            shiftInSeconds++;
            yield return second;

        }
    }

}
