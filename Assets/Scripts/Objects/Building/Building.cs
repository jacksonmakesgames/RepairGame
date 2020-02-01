using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    GameObject spriteMask;

    [SerializeField]
    Grid grid;
    private void Awake()
    {
        
    }

    public void Damage(ContactPoint2D point)
    {
        //Debug.Log("Damaged building at point " + point.point.ToString());
        //Vector3Int cellPosition = grid.WorldToCell(point.point);
        var randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Instantiate(spriteMask, point.point, randomRotation);

    }

}
