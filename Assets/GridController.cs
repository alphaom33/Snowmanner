using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject primitive;

    public Transform child;

    public Material mat1;
    public Material mat2;

    public float scaleFactor;

    public float tileScale = PlayerMovement.gridSize;
    public int layer;

    [EditorCools.Button]
    private void GenGrid()
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != child & t != transform) DestroyImmediate(t.gameObject);
        }

        MakeGrid();
    }

    // Start is called before the first frame update
    void Start()
    {
    }


    void MakeGrid()
    {
        for (int i = 0; i < child.localScale.x / tileScale; i++)
        {
            for (int j = 0; j < child.localScale.z / tileScale; j++)
            {
                GameObject tile = Instantiate(primitive);
                tile.transform.localScale *= scaleFactor * tileScale;

                tile.transform.parent = transform;
                tile.transform.localPosition = new Vector3(i, 0, j) * tileScale;


                tile.GetComponentInChildren<MeshRenderer>().material = i % 2 == 1 ^ j % 2 == 0 ? mat1 : mat2;

                tile.layer = layer;
            }
        }
    }
}
