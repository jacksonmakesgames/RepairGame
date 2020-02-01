﻿using UnityEngine;
using System.Collections.Generic;

public class PlayerTargeter : MonoBehaviour
{

    [SerializeField]
    private float powerRange = 8;

    [SerializeField]
    private float powerSpread;
    
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    
    private PolygonCollider2D polygonCollider;
    private OverlappingWithCollider overlapping;
    private float heading = 0f;

    private Vector2 joyInput;
    private Vector3 beamStartPos;

    // Start is called before the first frame update
    void Awake(){
        polygonCollider = GetComponentInChildren<PolygonCollider2D>();
        polygonCollider.transform.localScale = new Vector3(powerRange, powerSpread, 1.0f);
        overlapping = GetComponentInChildren<OverlappingWithCollider>();
        beamStartPos = polygonCollider.transform.localPosition;
    }

    private void LateUpdate()
    {
        polygonCollider.transform.position = transform.position + new Vector3(1 * Mathf.Sign(transform.localScale.x) * beamStartPos.x, beamStartPos.y, 0f);
        SetTarget();

    }
    void SetTarget()
    {
        spriteRenderer.enabled = false;
        joyInput = new Vector2(Input.GetAxis("JoystickLookX"), Input.GetAxis("JoystickLookY"));
        if (Input.GetButton("Fire1") || joyInput.magnitude > 0.1f)
        {
            spriteRenderer.enabled = true;
            Vector3 input = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(input);
            lookPos = lookPos - transform.position;
            float angle = Mathf.Atan2(1 * Mathf.Sign(transform.localScale.x)*lookPos.y, 1 * Mathf.Sign(transform.localScale.x) * lookPos.x) * Mathf.Rad2Deg;
            polygonCollider.transform.rotation = Quaternion.AngleAxis(angle,new Vector3(0,0,1));

            if (joyInput.magnitude>0.1f) {
                heading = Mathf.Atan2(joyInput.y, -1 * Mathf.Sign(transform.localScale.x) * joyInput.x) * Mathf.Rad2Deg;
                polygonCollider.transform.localRotation = Quaternion.Euler(0f, 0f, heading - 180f);
            }

        }
        else {
            return;
        }

        List<Collider2D> hits = overlapping.colliders;
    foreach (Collider2D hit in hits.ToArray())
    {
            if (hit.gameObject.tag == "Break Point") {
                hit.gameObject.GetComponent<BreakPoint>().Interact();
            }
    }


        // for mouse:
    }

}
