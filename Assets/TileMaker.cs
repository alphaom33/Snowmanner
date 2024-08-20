using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileMaker : MonoBehaviour
{
    public GameObject father;

    public GameObject prefab;
    public Material material1;
    public Material material2;

    public Grid grid;

    public string tagger;

    public bool AlreadyTaken(Vector3 position)
    {
        position = Vector3Int.RoundToInt(position);
        foreach (Transform t in father.GetComponentsInChildren<Transform>().Where((t, x) => x != 0))
        {
            if (t.position.Equals(position))
            {
                return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
