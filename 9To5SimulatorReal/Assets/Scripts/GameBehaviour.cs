using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{

    public int shiftInSeconds = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
