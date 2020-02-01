using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private enum StairType {Bottom, Top }

  
    [SerializeField]
    float disabledDuration = .01f;

    float disabledTime = 0.0f;
    PolygonCollider2D col;

    private void Awake()
    {
       col=  GetComponent<PolygonCollider2D>();
    }

    void FixedUpdate()
    {
       
            if (Input.GetButtonDown("Down")) {
            disabledTime = Time.time;
            col.enabled = false;
            
        }
        if (Time.time > disabledTime + disabledDuration) {
            col.enabled = true;
        }
    }

}
