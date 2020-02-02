using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Animator anim;

    public float amt = .15f;

    void Awake()
    {
        if (CameraShake.Instance != null) { Destroy(this); return; } else { CameraShake.Instance = this; }
        anim = GetComponent<Animator>();

    }

    public void Shake() {
        anim.SetTrigger("Shake");
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
