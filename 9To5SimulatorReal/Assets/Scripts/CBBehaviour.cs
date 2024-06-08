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

    void OnTriggerEnter(Collider other) {
        Console.WriteLine("Object on conveyor");
        if (other.CompareTag("Interactable"))
        {
            Console.WriteLine("MOVING");
            Vector3 force = new Vector3(conveyorForce * Time.deltaTime, 0f, 0f);
            other.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
        }
    }
}
