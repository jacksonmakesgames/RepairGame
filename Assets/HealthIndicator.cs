using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    public static HealthIndicator Instance;

    public SpriteRenderer[] indicators;
    public Animator[] anims;
    private void Awake()
    {
        Instance = this;
        anims = GetComponentsInChildren<Animator>();

        foreach (var sr in indicators)
        {
            sr.enabled = false;
        }
    }

    // Update is called once per frame
    public void UpdateHealth()
    {
        foreach (var a in anims)
        {
            a.speed = .1f;
            a.SetBool("Show", true);
        }
        timeShown = Time.time;

        if (Player.Instance.health == 3) {
            foreach (var sr in indicators)
            {
                sr.enabled = true;
            }
        }
        else if (Player.Instance.health == 2) {
            indicators[0].enabled = true;
            indicators[1].enabled = true;
            indicators[2].enabled = false;
        }else if (Player.Instance.health == 1) {
            indicators[0].enabled = true;
            indicators[1].enabled = false;
            indicators[2].enabled = false;

        }


    }

    float timeToShow = 2.0f;
    float timeShown = 0.0f;

    private void Update()
    {
        if (Time.time > timeShown + timeToShow) {
            foreach (var a in anims)
            {
                a.speed = .1f;
                a.SetBool("Show", false);
            }
        }
    }

}
