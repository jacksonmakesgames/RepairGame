using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public void Damage(ContactPoint2D point)
    {
        Debug.Log("Damaged building at point " + point.point.ToString());
        
    }

}
