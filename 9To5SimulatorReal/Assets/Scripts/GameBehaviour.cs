using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{

    public int shiftInSeconds = 0;
    public int shiftInMinutes = 0;
    public int shiftInHours = 0;
    [SerializeField]
    private bool switchStartShift = false;
    public bool inShift = false;
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
