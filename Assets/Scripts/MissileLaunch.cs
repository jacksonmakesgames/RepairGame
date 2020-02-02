using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLaunch : MonoBehaviour
{
    Vector3 target;
    Rigidbody2D rb;

    public float speed=10;



    //explosions are 170
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = MissileSpawner.Instance.transform.position;

        var locVel = transform.InverseTransformDirection(target - transform.position).normalized;
        locVel *= -speed;
        rb.velocity = transform.TransformDirection(locVel);
    }

    
}
