using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WonkyFab : MonoBehaviour
{
    GameObject player;

    public GameObject[] parts;

    // Start is called before the first frame update
    public void DoTheThing(int current)
    {
        player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position;

        PlayerGrower grower = player.GetComponent<PlayerGrower>();
        int size = grower.current - current;

        for (int i = 0; i < current; i++) {
            GameObject a = Instantiate(parts[i], transform);
            a.GetComponentsInChildren<Collider>().ToList().ForEach(x => x.enabled = true);
            if (i == parts.Length - 1)
            {
                a.AddComponent<PlayeMaker>();
                a.layer = Layers.floor;
            }
        }

        grower.current = size;
        grower.UpdateNumbers();
    }
}
