using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPoint : Interactable
{
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Missile")) {
            collision.gameObject.GetComponent<Missile>().PassThrough();
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
