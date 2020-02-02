using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    CircleCollider2D collider;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Radio")) {
            collision.GetComponent<Radio>().Damage();
        } 
        else if (collision.gameObject.CompareTag("Player")) {
            collision.GetComponent<Player>().removeHealth(10);
        }
    }

}
