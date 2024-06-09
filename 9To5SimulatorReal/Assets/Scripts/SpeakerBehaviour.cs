using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float delay;
    private bool switchSound = false;

    private AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (switchSound) {
            myAudio.PlayDelayed(delay);
            switchSound = false;
        }
    }

    public void start() {
        switchSound = true;
    }
}
