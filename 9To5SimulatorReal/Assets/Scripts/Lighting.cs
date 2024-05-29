using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Lighting : MonoBehaviour
{
    public float max = 0.0f;
    public float modulateIntensity = 0.0f;
    private Light myLight;
    private float min;
    private bool flip = false;

    // Start is called before the first frame update
    void Start()
    {
        myLight = this.gameObject.GetComponent<Light>();
        this.min = myLight.intensity;
        if (max <= min) max = min + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float r = UnityEngine.Random.Range(0, 1.0f);
        if (!flip)
        {
            myLight.intensity += modulateIntensity * Time.deltaTime;
            if (r <= 0.5f) flip = !flip;
            if (myLight.intensity > max) myLight.intensity = max;
        }
        else
        {
            myLight.intensity -= modulateIntensity * Time.deltaTime;
            if (r <= 0.5f) flip = !flip;
            if (myLight.intensity < min) myLight.intensity = min;
        }
    }
}
