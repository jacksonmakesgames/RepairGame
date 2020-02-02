using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    Vector3 target;
    [SerializeField]
    GameObject explosionEffect;

    public float speed;

    float timeToDestroy = 10f;

    Rigidbody2D rb;

    float createdTime;

    BoxCollider2D col;

    [SerializeField]
    AudioClip explosionSound;

    AudioSource audioSource;

    int breakPointCount = 0;

    //explosions are 170
    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        createdTime = Time.time;

        target = MissileSpawner.Instance.transform.position;
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

    public void Explode(Collision2D collision) {
        if (collision.gameObject.tag == "Building")
        {
            collision.gameObject.GetComponent<Building>().Damage(collision.contacts[0]);
            var randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            Instantiate(explosionEffect, collision.contacts[0].point, randomRotation);
        }
        else {
            var randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            Instantiate(explosionEffect, transform.position, randomRotation);
        }
        CameraShake.Instance.Shake();
        audioSource.PlayOneShot(explosionSound);

        Destroy(this.gameObject);
    }

    public void PassThroughEnd()
    {
        breakPointCount--;
        if(breakPointCount<=0)
            gameObject.layer = LayerMask.NameToLayer("Missile");

    }
    public void PassThrough() {
        breakPointCount++;
        gameObject.layer = LayerMask.NameToLayer("Passing Missile");
    }

}
