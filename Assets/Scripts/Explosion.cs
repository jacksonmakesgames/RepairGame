using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    CircleCollider2D collider;

    private void Awake(){
        collider = GetComponent<CircleCollider2D>();
        CheckCollisions();
    }

    private void CheckCollisions(){

       Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, collider.radius);
        foreach (Collider2D collider in cols)
        {
            if (collider.gameObject.CompareTag("Radio")) {
                collider.GetComponent<Radio>().Damage();
            } 
            else if (collider.gameObject.CompareTag("Player")) {
                collider.GetComponent<Player>().removeHealth(10);
            }

        }
    }

}
