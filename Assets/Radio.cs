using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Interactable
{
    [SerializeField]
    AudioClip radioBackgroundClip;

    AudioSource source;

    public int health;

    public override void Awake(){
        source = GetComponent<AudioSource>();
    }

    private void Start(){
        source.clip = radioBackgroundClip;
        source.Play();
    }

    public override void Interact()
    {
        return;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Missile") {
            print("RADIO HIT");
            health -= 10;

            if (health <= 0) {
                print("radio destroyed");
            }
            collision.gameObject.GetComponent<Missile>().Explode(collision);
        }
    }
}
