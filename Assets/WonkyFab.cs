using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WonkyFab : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    public void DoTheThing(int current)
    {
        player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position;

        PlayerGrower grower = player.GetComponent<PlayerGrower>();
        int size = grower.current - current;

        Transform[] tmp = player.GetComponentsInChildren<Transform>().Where((t, i) => i <= 2 - size && i < 4 && i != 0).ToArray();
        for (int i = 0; i < tmp.Length; i++) {
            Transform a = Instantiate(tmp[i], transform);
            if (i == tmp.Length - 1)
            {
                a.AddComponent<PlayeMaker>();
                a.gameObject.layer = Layers.floor;
            }
        }

        grower.current = size;
        grower.UpdateNumbers();
    }
}
