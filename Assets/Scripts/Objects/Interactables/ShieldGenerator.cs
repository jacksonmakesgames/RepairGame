using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : Interactable
{
    public int totalRequiredScrap = 100;

    public int currentScrap = 0;

    public bool repaired = false;


    [SerializeField]
    GameObject hoverEffect;

    [SerializeField]
    GameObject shieldObj;

    private SpriteRenderer renderer;

    bool hovered = false;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public override void Interact(){
        if (!(Vector2.Distance(Player.Instance.transform.position, transform.position) <= Player.Instance.interactRange)) {
            return;
        }
        if (!repaired)
        {
            if (Player.Instance.nScrapHeld > 0)
            {
                Player.Instance.removeScrap(1);
                currentScrap++;
                if (currentScrap > totalRequiredScrap)
                {
                    FinishRepair();
                }
            }
            else
            {
                // No scrap!
            }
        }
        else {
            //repaired:
            ActivateShield();
        }
    }
    private void FinishRepair() {
        print("Done repairing gen");
    }

    private void ActivateShield() {
        shieldObj.SetActive(true);
    }
    private void OnMouseDown()
    {
        Interact();
    }

    private void OnMouseOver()
    {
        if ((Vector2.Distance(Player.Instance.transform.position, transform.position) <= Player.Instance.interactRange))
        {
            Player.Instance.canBeam = false;
            renderer.color = new Color(.85f,.8f,.8f);
            hovered = true;
        }
        else {
            UnHover();
            return;
        }
    }
    private void OnMouseExit()
    {
        if (hovered)
            UnHover();
       
    }

    void UnHover() {
        hovered = false;
        Player.Instance.canBeam = true;
        renderer.color = Color.white;
    }
}
