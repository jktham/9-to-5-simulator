using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{

    public int shiftInSeconds = 0;
    public int shiftInMinutes = 0;
    public int shiftInHours = 0;
    public int wincondition = 3600 * 8;

    public bool DebugMode = false;
    public bool win = false;

    [SerializeField]
    InputActionAsset input;

    [SerializeField]
    private bool switchStartShift = false;
    public bool inShift = false;

    protected void OnEnable() {
        input.Enable();
    }

    void Awake() {
        DontDestroyOnLoad(GameObject.Find("Player"));
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
        if (shiftInSeconds >= wincondition || win == true) {
            ending();
        }
    }

    public void startShift() {
        switchStartShift = true;
    }

    public void endShift() {
        StopCoroutine( StartShift() );
    }

    private void ending() {
        SceneManager.LoadScene("Ending");
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
