using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeMaker : MonoBehaviour
{
    public int num;
    public GameObject parent;
    
    private void Start()
    {
        parent = GetComponentsInParent<Transform>()[1].gameObject;
        try
        {
            parent = parent.GetComponentsInParent<Transform>()[1].gameObject;
        }
        catch (IndexOutOfRangeException) { }
    }

    public void Go(PlayerGrower playerGrower) {
        playerGrower.current = 2;
        playerGrower.UpdateNumbers();
        playerGrower.transform.position = parent.transform.position;
        Destroy(parent);
    }
}
