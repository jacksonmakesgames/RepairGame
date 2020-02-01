using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScroll : MonoBehaviour
{
    [SerializeField]
    float rate;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-1 * new Vector3(rate * Time.deltaTime, 0, 0));
        if (transform.localPosition.x <= -1600.0f) {
            transform.localPosition += new Vector3(4800.0f, 0, 0);
        }
            
    }
}
