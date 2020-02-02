using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool canInteract = false;
    public virtual void Awake() { }

    public abstract void Interact();
    
}
