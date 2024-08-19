using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeMaker : MonoBehaviour
{
    public int num;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.GetComponent<PlayerMovement>().running)
        {
            PlayerGrower tmp = other.GetComponent<PlayerGrower>();
            tmp.current = 2;
            tmp.UpdateNumbers();
            Destroy(gameObject);
        }
    }
}
