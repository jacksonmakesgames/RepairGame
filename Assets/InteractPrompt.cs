using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    public static InteractPrompt Instance;

    public Animator anim;

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }

    public void Hide() {
        anim.SetBool("Show", false);
    }

    public void Show()
    {
        anim.SetBool("Show", true);
    }

}
