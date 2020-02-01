using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private enum StairType {Bottom, Top }

    [SerializeField]
    StairType type;

    [SerializeField]
    LayerMask playerMask;

    [SerializeField]
    Transform target;

    [SerializeField]


    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (playerMask == (playerMask | (1 << collision.gameObject.layer))){
    //        if ((type == StairType.Bottom && Input.GetButtonDown("Up")) || (type == StairType.Top) && Input.GetButtonDown("Down")){
    //         //collision.gameObject.transform.position = target.position;
    //        }
    //    }
    //}



    void Update()
    {
        if (GetComponent<BoxCollider2D>().IsTouchingLayers(playerMask.value))
        {
            if ((type == StairType.Bottom && Input.GetButtonDown("Up")) || (type == StairType.Top) && Input.GetButtonDown("Down"))
            {
                Player.Instance.gameObject.transform.position = target.position;
            }
        }
    }

}
