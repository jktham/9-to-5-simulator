using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public enum PattyState
{
    raw, cooked, burnt, fire
}

public class PattyBehaviour : MonoBehaviour
{
    public PattyState state = PattyState.raw;
    public int timeForCook = 60;
    public int timeForBurn = 90;
    public int timeForFire = 120;
    private float timeCooking = 0f;
    private bool isgrill = false;

    // Update is called once per frame
    void Update()
    {
        // grill logic
        if (isgrill) stateUpdate();

        // Set correct pattymode
        switch (state)
        {
            case PattyState.raw :
                pattyUpdate(0);
                break;
            case PattyState.cooked :
                pattyUpdate(1);
                break;
            case PattyState.burnt :
                pattyUpdate(2);
                break;
            case PattyState.fire :
                pattyUpdate(3);
                break;
        }
    }

    // set correct patty gameObject active
    private void pattyUpdate(int index) {
        GameObject patty = getPatty(index);
        if (!patty.activeSelf)
        {
            patty.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                if (i != index) {
                    getPatty(i).SetActive(false);
                }
            }
        }
    }

    // update pattyState
    private void stateUpdate() {
        timeCooking += Time.deltaTime;
        if (timeCooking >= timeForCook && timeCooking < timeForBurn) {
            state = PattyState.cooked;
        } else if (timeCooking >= timeForBurn && timeCooking < timeForFire) {
            state = PattyState.burnt;
        } else if (timeCooking >= timeForFire) {
            state = PattyState.fire;
        }
    }

    // get correct patty
    private GameObject getPatty(int index) {
        return this.gameObject.transform.GetChild(index).gameObject;
    }

    // setting patty's grilled status
    public void setGrill(bool x) {
        isgrill = x;
    }
}
