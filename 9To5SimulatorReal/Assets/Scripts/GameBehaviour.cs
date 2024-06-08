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

    [SerializeField]
    InputActionAsset input;

    [SerializeField]
    private bool switchStartShift = false;
    public bool inShift = false;
    public bool win = false;

    protected void OnEnable() {
        input.Enable();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // starts shift one time only!
        if (switchStartShift && !inShift)
        {
            startShift();
            switchStartShift = false;
            inShift = true;
        }

        // update both minutes and hours
        shiftInMinutes = shiftInSeconds / 60;
        shiftInHours = shiftInMinutes / 60;

        // wincondition
        if (shiftInSeconds >= wincondition && GameObject.FindGameObjectsWithTag("Interactable").Length == 0)
        {
            win = true;
        }
    }

    public void startShift() {
        StartCoroutine( StartShift() );
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
