using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whyyyy : MonoBehaviour
{
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        transform.Translate(0, -transform.position.y, 0);
    }
}
