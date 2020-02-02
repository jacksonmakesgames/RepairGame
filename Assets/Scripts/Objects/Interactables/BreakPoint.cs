using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPoint : Interactable
{
    [SerializeField]
    GameObject smokeEffect;

    public override void Interact(){
        if (Player.Instance.nScrapHeld > 0)
        {
            Player.Instance.removeScrap(1);
            Destroy(this.gameObject);
        }
        else {
        // No scrap!
        }
    }

    private void Awake()
    {
        if (Random.Range(0.0f, 2.0f) < 1.0f) {
            Instantiate(smokeEffect, transform.position, Quaternion.identity, transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Missile")) {
            collision.gameObject.GetComponent<Missile>().PassThrough();
        }
        if (collision.gameObject.CompareTag("Break Point")) {
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Missile"))
        {
            collision.gameObject.GetComponent<Missile>().PassThroughEnd();
        }
    }
}
