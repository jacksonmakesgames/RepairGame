using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : Interactable
{
    [SerializeField]
    GameObject effect;

    public bool damaged = false;
    SpriteRenderer sr;
    AudioSource source;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }
    public int amtHealthToAdd;
    public override void Interact()
    {
        if (!damaged)
        {
            Player.Instance.addHealth(amtHealthToAdd);
            if (Player.Instance.health < Player.Instance.maxHealth)
            {
                source.Play();
                Instantiate(effect, transform.position, Quaternion.identity);
            }

        }

        else {
            if (Player.Instance.nScrapHeld > 0)
            {
                Player.Instance.removeScrap(1);
                damaged = false;

            }
            else
            {
                // No scrap!
            }
        }

    }

    private void Update()
    {
        if (damaged) sr.color = new Color(.8f,.8f,.8f,.5f);
        else sr.color = new Color(.8f,.8f,.8f,1f);
    }
}
