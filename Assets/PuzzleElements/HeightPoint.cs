using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightPoint : WallController 
{
    public int current;

    public override bool GetWall()
    {
        Debug.Log(GameObject.FindWithTag("Player").GetComponent<PlayerGrower>().current <= current);
        return GameObject.FindWithTag("Player").GetComponent<PlayerGrower>().current > current;
    }
}
