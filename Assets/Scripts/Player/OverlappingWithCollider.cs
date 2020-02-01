using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlappingWithCollider : MonoBehaviour
{
    public List<Collider2D> colliders = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!colliders.Contains(collider))
        {
            colliders.Add(collider);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (colliders.Contains(collider))
        {
            colliders.Remove(collider);
        }
    }

}
