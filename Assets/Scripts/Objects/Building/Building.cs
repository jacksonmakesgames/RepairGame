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
        //Vector3Int cellPosition = grid.WorldToCell();
        float x = Mathf.Round((point.point.x * 5.0f)) / 5.0f;
        float y = Mathf.Round((point.point.y * 5.0f)) / 5.0f;

        //bool found = false;
        //foreach (Collider2D col in Physics2D.OverlapBoxAll(new Vector2(x, y), new Vector2(grid.cellSize.x, grid.cellSize.y), 0)) {
        //    if (col.tag== "Break Point") {
        //        found = true;
        //    }
        
        //}
        //if (!found)
        //{

            Vector3 loc = new Vector3(x, y, 0);
            var randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            Instantiate(spriteMask, loc, randomRotation);
        //}

        //var randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        //Instantiate(spriteMask, point.point, randomRotation);

    }

}
