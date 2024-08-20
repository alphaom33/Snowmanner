using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string next;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && next != "")
        {
            SceneManager.LoadScene(next);
            return;
        }
        if (other.CompareTag("Player") && GameObject.FindWithTag("LevelSelector"))
        {
            GameObject.FindWithTag("LevelSelector").GetComponent<LevelSelector>().Next();
        }
    }
}
