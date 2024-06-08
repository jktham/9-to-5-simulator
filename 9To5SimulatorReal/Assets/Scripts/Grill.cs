using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {

        if (other.transform.name.Contains("patty"))
        {
            other.GetComponent<PattyBehaviour>().setGrill(true);
            other.GetComponent<AudioSource>().Play();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.transform.name.Contains("patty"))
        {
            other.GetComponent<PattyBehaviour>().setGrill(false);
            other.GetComponent<AudioSource>().Stop();
        }
    }
}
