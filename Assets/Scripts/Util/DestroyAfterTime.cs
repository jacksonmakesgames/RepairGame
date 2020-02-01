using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    float time;


    private float createdTime;
    // Start is called before the first frame update
    void Start()
    {
        createdTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > createdTime + time) {
            Destroy(this.gameObject);
        }
    }
}
