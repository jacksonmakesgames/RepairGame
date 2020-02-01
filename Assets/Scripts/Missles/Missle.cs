using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    Vector3 target;
    [SerializeField]
    GameObject explosionEffect;

    public float speed;

    float timeToDestroy = 10f;

    Rigidbody2D rb;

    float createdTime;

    void Start()
    {
        createdTime = Time.time;

        target = MissleSpawner.Instance.transform.position;
        rb = GetComponent<Rigidbody2D>();

        var locVel = transform.InverseTransformDirection(target - transform.position).normalized;
        locVel *= speed;
        rb.velocity = transform.TransformDirection(locVel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > (createdTime + timeToDestroy)) {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode(collision);
    }

    void Explode(Collision2D collision) {
        if (collision.gameObject.tag == "Building") {
            Instantiate(explosionEffect, collision.contacts[0].point, Quaternion.identity);
            collision.gameObject.GetComponent<Building>().Damage(collision.contacts[0]);
             Destroy(this.gameObject);
        }
    }
}
