using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private GameBehaviour control;
    private Transform sec;
    private Transform min;
    private Transform h;

    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("GameController").GetComponent<GameBehaviour>();
        sec = this.gameObject.transform.GetChild(0).transform;
        min = this.gameObject.transform.GetChild(1).transform;
        h = this.gameObject.transform.GetChild(2).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (control.inShift)
        {
            float secAngle = 270 - (control.shiftInSeconds % 60) * 6;
            float minAngle = 270 - (control.shiftInMinutes % 60) * 6;
            float hAngle = control.shiftInHours * -30;
            sec.localEulerAngles = new Vector3(secAngle, 0f, 0f);
            min.localEulerAngles = new Vector3(minAngle, 0f, 0f);
            h.localEulerAngles = new Vector3(hAngle, 0f, 0f);
        }
    }
}
