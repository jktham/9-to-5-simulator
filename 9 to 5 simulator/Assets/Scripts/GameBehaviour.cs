using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class GameBehaviour : MonoBehaviour
{ 
    public bool inShift = false;
    public bool win = false;
    [SerializeField]
    private float timeSpentWorking = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inShift)
        {
            timeSpentWorking += Time.deltaTime;
        }
    }
}
