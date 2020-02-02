using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndScreen : MonoBehaviour
{
    public static EndScreen Instance;
    public float time;
    float startTime;
    public GameObject loseScreen;
    public GameObject winScreen;
    // Start is called before the first frame update
    void Awake()
    {
        if(EndScreen.Instance == null) Instance = this;
        startTime = float.MaxValue - time;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + time) {
            MainMenu();
        }
    }

    public void Win() {
        MissileSpawner.Instance.spawn = false;
        startTime = Time.time;
        winScreen.SetActive(true);
        GameObject.FindGameObjectWithTag("Win").GetComponent<Win>().Winner();
    }
    public void Lose()
    {
        MissileSpawner.Instance.spawn = false;

        startTime = Time.time;
        loseScreen.SetActive(true);
        AudioManager.Instance.SetClip(AudioManager.Instance.audioClips["Lose"]);
    }

    public void MainMenu() {
       GameManager.Instance.GetComponent<GameManager>().LoadScene(0);

    }
}
