using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : Interactable
{
    [SerializeField]
    GameObject effect;

    public int amtHealthToAdd;
    public override void Interact()
    {
        Player.Instance.addHealth(amtHealthToAdd);
        if (Player.Instance.health < Player.Instance.maxHealth) {
            Instantiate(effect, transform.position, Quaternion.identity);
        }

    }
}
