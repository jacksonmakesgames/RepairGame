using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public int nScrapHeld;
    public int maxScapHeld;
    public bool canBeam = true;
    public float interactRange;
    public int maxHealth = 100;
    public int health;

    [SerializeField]
    TextMeshProUGUI scrapText;
    
    void Awake()
    {
        if (Player.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else Instance = this;

        nScrapHeld = 0;
        health = maxHealth;
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact")) {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, interactRange);
            if (cols.Length == 0) return;
            float minDist = 10000;
            Collider2D closestCol = cols[0];
            foreach (Collider2D collider in cols) {
                if (collider.GetComponent<Interactable>() == null){
                    continue;
                }
                float thisDist = Vector2.Distance(transform.position, collider.gameObject.transform.position);
                if (thisDist< minDist) {
                    minDist = thisDist;
                    closestCol = collider;
                }
            }
            if (closestCol.GetComponent<Interactable>() != null) {
                closestCol.GetComponent<Interactable>().Interact();
            }
        }
        CheckInRange();
        
    }

    public void removeScrap(int amt) {
        nScrapHeld -= amt;
        if (nScrapHeld < 0) nScrapHeld = 0;
        ScrapIndicator.Instance.UpdateScrap(nScrapHeld);

    }
    public void addScap(int amt) {
        nScrapHeld += amt;
        if (nScrapHeld > maxScapHeld) nScrapHeld = maxScapHeld;
        ScrapIndicator.Instance.UpdateScrap(nScrapHeld);
    }

    public void removeHealth(int amt) {
        print("player hit, " + health.ToString() + " health left");
        health -= amt;
        if (health < 0) Die();
    }
    public void addHealth(int amt) {
        health += amt;
        if (health > maxHealth) health = maxHealth;
    }
    
   

    private void Die()
    {
        print("Player Died");
        GameObject.FindGameObjectWithTag("LoseScreen").GetComponent<EndScreen>().Lose();
    }

    public void CheckInRange() {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, interactRange);
        if (cols.Length == 0) return;
        float minDist = 10000;
        Collider2D closestCol = cols[0];
        bool found = false;
        foreach (Collider2D collider in cols)
        {
            if (collider.GetComponent<Interactable>() == null)
            {
                continue;
            }
            float thisDist = Vector2.Distance(transform.position, collider.gameObject.transform.position);
            if (thisDist < minDist)
            {
                minDist = thisDist;
                closestCol = collider;
            }
        }
        if (closestCol.GetComponent<Interactable>() != null)
        {
            found = true;
        }
        if (found && closestCol.GetComponent<Interactable>().canInteract)
        {
            InteractPrompt.Instance.Show();
        }
        else {
            InteractPrompt.Instance.Hide();

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

}
