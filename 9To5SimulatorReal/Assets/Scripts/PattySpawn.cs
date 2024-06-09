using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PattySpawn : MonoBehaviour
{
    public GameObject normalpatty;
    public GameObject mikupatty;
    private GameBehaviour game;

    [SerializeField]
    private float baseDropChance;
    [SerializeField]
    private float rushDropChance;
    [SerializeField]
    private float specialDropChance;
    [SerializeField]
    private int deltaCheck;
    private bool switcher = false;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (switcher) {
            StartCoroutine( burgerTime() );
            switcher = false;
        }
        if (game.win) {
            StopAllCoroutines();
        }
    }

    public void start() {
        switcher = true;
    }

    private IEnumerator burgerTime() {

        WaitForSeconds second = new WaitForSeconds(deltaCheck);
        while (true)
        {
            yield return second;
            Debug.Log("Spawning");
            float temp = Random.Range(0f,1f);
            float which = Random.Range(0f,1f);
            if (game.shiftInHours == 3) {

                if (temp < rushDropChance) {

                    if (which < specialDropChance) {
                        Instantiate(mikupatty, transform.position, mikupatty.transform.rotation);
                    } else {
                        Instantiate(normalpatty, transform.position, normalpatty.transform.rotation);
                    }
                }
                
            } else {

                if (temp < baseDropChance) {

                    if (which < specialDropChance) {
                        Instantiate(mikupatty, transform.position, mikupatty.transform.rotation);
                    } else {
                        Instantiate(normalpatty, transform.position, normalpatty.transform.rotation);
                    }
                }

            }

        }
    }

}
