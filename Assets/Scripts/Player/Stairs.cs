using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private PlatformEffector2D effector;

    float waitTime = .1f;

    float originalOffset;
    private void Awake()
    {
        effector = GetComponent<PlatformEffector2D>();
        originalOffset = effector.rotationalOffset;
    }

    float currentWaitTime = 0.0f;

    void Update()
    {
        if (Input.GetButtonUp("Down")) {
            currentWaitTime = waitTime;
            effector.rotationalOffset = originalOffset;

        }

        if (Input.GetButton("Down")) {
            if (currentWaitTime <= 0)
            {
                effector.rotationalOffset = originalOffset + 180.0f;
                currentWaitTime = waitTime;
            }
            else {
                currentWaitTime -= Time.deltaTime;
            }
        }
       
    }

}
