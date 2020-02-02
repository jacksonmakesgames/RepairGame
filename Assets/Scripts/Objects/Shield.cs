using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public ShieldGenerator gen;
    public float duration = 35f;
    float startTime = 35f;
    public bool on = false;
    AudioSource audioSource;
    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }
    public void TurnOn() {
        on = true;
        startTime = Time.time;
        gameObject.SetActive(true);
        audioSource.Play();
    }
    public void TurnOff()
    {
        gameObject.SetActive(false);
        on = false;
        audioSource.Stop();
    }

    private void Update()
    {
        if (on) {
            if (Time.time > startTime + duration) {
                TurnOff();
            }
        }
    }


}
