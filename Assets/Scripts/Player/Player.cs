using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance;
    void Awake()
    {
        if (Player.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else Instance = this;
    }

    void Update()
    {
        
    }
}
