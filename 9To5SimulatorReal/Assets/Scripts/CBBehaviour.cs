using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBBehaviour : MonoBehaviour
{

    [SerializeField]
    float conveyorForce;


    void OnCollisionStay(Collision other) {

        // apply force to patties on conveyor belt
        if (other.gameObject.CompareTag("Interactable"))
        {
            Vector3 force = new Vector3(conveyorForce * 100 * Time.deltaTime, 0f, 0f);
            other.gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
        }

    }
}
