using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Interactable
{
    public static Radio Instance;

    public bool ready = false;

    public int maxHealth = 6;

    [SerializeField]
    AudioClip radioBackgroundClip;

    AudioSource source;

    public int health;

    SpriteRenderer sr;

    public override void Awake(){
        if (Radio.Instance == null) Radio.Instance = this; 
        source = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start(){
        
    }

    public void Ready() {

        ready = true;
        source.clip = radioBackgroundClip;
        source.Play();
        GetComponent<Animator>().SetBool("Ready", true);

    }

    public override void Interact()
    {
        if (health < maxHealth) {
            if (Player.Instance.nScrapHeld > 0)
            {
                Player.Instance.removeScrap(1);
                health++;
            }
            else
            {
                // No scrap!
            }
        }
        else if (ready)
        {
            EndScreen.Instance.Win();
            return;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Missile") {
            collision.gameObject.GetComponent<Missile>().Explode(collision);
        }
    }

    private void Update()
    {
        canInteract = health < maxHealth || ready;

        if (health<maxHealth) sr.color = new Color(.8f, .8f, .8f, .5f);
        else sr.color = new Color(.8f, .8f, .8f, 1f);

        GetComponent<Animator>().SetBool("Damaged", health < maxHealth); 
    }

    public void Damage() {
        health -= 2;
        if (health <= 0)
        {
            EndScreen.Instance.Lose();
        }
    }
}
