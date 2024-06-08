using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBBehaviour : MonoBehaviour
{

    public float conveyorForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Interactable"))
        {
            Vector3 force = new Vector3(conveyorForce * 100 * Time.deltaTime, 0f, 0f);
            other.gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
        }
    }
}
