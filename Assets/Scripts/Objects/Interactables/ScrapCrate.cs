using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapCrate : Interactable
{
    public int amtScrapGivenPerInteraction;
    public override void Awake(){
        
    }

    public override void Interact() {
        Player.Instance.addScap(amtScrapGivenPerInteraction);
    }
}
