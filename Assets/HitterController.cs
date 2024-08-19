using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterController : WallController
{
    public int current;
    PlayerGrower player;
    public GameObject wonkyFab;

    public float[] heights = { 2, 3.115f };
    public float[] floorHeights = { 1.7f, 2.88f };

    public void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerGrower>();
    }

    public override bool GetWall()
    {
        if (player.current >= current)
        {
            player.GetComponent<Heighter>().Stop();
            Instantiate(wonkyFab).GetComponent<WonkyFab>().DoTheThing(current);
            player.transform.position = new Vector3(player.transform.position.x, heights[current - 1], player.transform.position.z);
            return false;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        GetComponentsInChildren<Transform>()[1].position = new Vector3(transform.position.x, floorHeights[current - 1], transform.position.z);
    }
}
