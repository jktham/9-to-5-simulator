using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerBehaviour : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> clips;
    private bool switchSound = false;
    private bool locked = false;

    private AudioSource myAudio;
    private GameBehaviour game;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        game = GameObject.Find("GameController").GetComponent<GameBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (switchSound) {
            playSound(0,1);
            switchSound = false;
        }
    }

    public void start() {
        switchSound = true;
    }

    public void playSound(int index, int delay) {

        if (!locked) {
            myAudio.clip = clips[index];
            myAudio.PlayDelayed(delay);
            StartCoroutine(Waiting(clips[index].length + delay));
            if (index == 3 || index == 4) {
                game.illegalClockOuts++;
            }
        }

    }

    // prevent overlapping soundclips
    private IEnumerator Waiting(float Length) {

        locked = true;
        WaitForSeconds seconds = new WaitForSeconds(Length);
        yield return seconds;
        locked = false;

    }

}
