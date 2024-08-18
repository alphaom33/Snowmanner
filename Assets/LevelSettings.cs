using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public int playerStartNum;

    // Start is called before the first frame update
    void Start()
    {
        PlayerGrower tmp = GameObject.FindWithTag("Player").GetComponent<PlayerGrower>(); 
        tmp.current = playerStartNum;
        tmp.UpdateNumbers();
    }
}
