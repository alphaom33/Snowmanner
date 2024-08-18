using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public List<string> levels;
    public int current;
    public static bool happened;

    public void Start()
    {
        if (happened) return;

        DontDestroyOnLoad(this);
        happened = true;
    }

    public void Next()
    {
        current++;
        SceneManager.LoadScene(levels[current]);
    }
}
