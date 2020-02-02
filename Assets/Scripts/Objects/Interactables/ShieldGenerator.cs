using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : Interactable
{
    public AnimatorOverrideController[] animatorOverrideControllers;

    public int totalRequiredScrap = 20;

    public int currentScrap = 0;

    public bool repaired = false;


    [SerializeField]
    GameObject hoverEffect;

    [SerializeField]
    GameObject shieldObj;

    private SpriteRenderer renderer;

    bool hovered = false;
    Animator anim;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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

                UpdateAnim();
              
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
        repaired = true;
    }

    private void ActivateShield() {
        if (shieldObj.GetComponent<Shield>().on) return;
        repaired = false;
        currentScrap = 0;
        UpdateAnim();
        shieldObj.GetComponent<Shield>().TurnOn();
    }
    private void OnMouseDown()
    {
        Interact();
    }

    void UpdateAnim() {
        int index = 0;
        if (currentScrap >= totalRequiredScrap / 4)
            index = 1;
        if (currentScrap >= totalRequiredScrap / 2)
            index = 2;
        if (currentScrap >= totalRequiredScrap)
            index = 3;

        if (currentScrap > totalRequiredScrap)
        {
            FinishRepair();
        }

        anim.runtimeAnimatorController = animatorOverrideControllers[index];
        anim.Play("ShieldGen");
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

    public void Damage() {
        print("gen damaged");
        currentScrap--;
        if (currentScrap < 0) currentScrap = 0;
        UpdateAnim();
    }
}
