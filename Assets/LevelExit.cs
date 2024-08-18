tusing System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindWithTag("LevelSelector").GetComponent<LevelSelector>().Next();
        }
    }
}
