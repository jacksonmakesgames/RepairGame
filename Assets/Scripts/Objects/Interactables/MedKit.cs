using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : Interactable
{
    public int amtHealthToAdd;
    public override void Interact()
    {
        Player.Instance.addHealth(amtHealthToAdd);
    }
}
