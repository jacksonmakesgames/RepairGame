using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    CircleCollider2D collider;

    List<GameObject> hits;
    
    private void Awake(){
        collider = GetComponent<CircleCollider2D>();
        hits = new List<GameObject>();
        CheckCollisions();
    }


    private void CheckCollisions(){

       Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, collider.radius);

        foreach (Collider2D collider in cols)
        {
            if (collider.gameObject.CompareTag("Radio")) {
                if (!hits.Contains(collider.gameObject))
                {
                    collider.GetComponent<Radio>().Damage();
                    hits.Add(collider.gameObject);

                }
            } 
            else if (collider.gameObject.CompareTag("Player")) {
                if (!hits.Contains(collider.gameObject))
                {
                    collider.GetComponent<Player>().removeHealth(10);
                    hits.Add(collider.gameObject);
                }
            }

        }
    }

}
