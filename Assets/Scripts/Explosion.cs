using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip[] explosionSounds;
    public AudioClip radioExplosion;
    CircleCollider2D collider;

    List<GameObject> hits;

    AudioSource audioSource;
    private void Awake(){
        collider = GetComponent<CircleCollider2D>();
        hits = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = explosionSounds[Random.Range(0, explosionSounds.Length)];
        CheckCollisions();

        audioSource.Play();
    }


    private void CheckCollisions(){

       Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, collider.radius);

        foreach (Collider2D collider in cols)
        {
            if (collider.gameObject.CompareTag("Radio"))
            {
                audioSource.clip = radioExplosion;
                if (!hits.Contains(collider.gameObject))
                {
                    collider.GetComponent<Radio>().Damage();
                    hits.Add(collider.gameObject);

                }
            }
            else if (collider.gameObject.CompareTag("MedKit"))
            {
                collider.GetComponent<MedKit>().damaged = true;
            }
            else if (collider.gameObject.CompareTag("ShieldGen"))
            {
                collider.GetComponent<ShieldGenerator>().Damage();
            }
            else if (collider.gameObject.CompareTag("Player"))
            {
                if (!hits.Contains(collider.gameObject))
                {
                    collider.GetComponent<Player>().removeHealth(1);
                    hits.Add(collider.gameObject);
                }
            }

        }
    }

}
