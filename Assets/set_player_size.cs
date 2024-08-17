using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_player_size : MonoBehaviour
{
    public int startSize;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerGrower>().current = startSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
