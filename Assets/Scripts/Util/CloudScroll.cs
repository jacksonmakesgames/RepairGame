using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScroll : MonoBehaviour
{
    [SerializeField]
    float rate;

    [SerializeField]
    float offset;

    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        anim.SetFloat("Offset", offset);
    }
    //void FixedUpdate()
    //{
    //    transform.Translate(-1 * new Vector3(rate * Time.deltaTime, 0, 0));
    //    if (transform.localPosition.x <= -1600.0f) {
    //        transform.localPosition += new Vector3(4800.0f, 0, 0);
    //    }

    //}
}
