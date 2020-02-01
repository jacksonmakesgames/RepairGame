using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapCrate : Interactable
{
    public override void Awake(){
        
    }

    public override void Interact() {
        Debug.Log("Interacted with " + name);
    }
}
