using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Transform child;

    public Material mat1;
    public Material mat2;

    public float scaleFactor;

    public float tileScale = PlayerMovement.gridSize;

    [EditorCools.Button]
    private void GenGrid()
    {
        foreach (MeshRenderer t in GetComponentsInChildren<MeshRenderer>())
        {
            if (t != child) DestroyImmediate(t.gameObject);
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
                GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Plane);
                tile.transform.localScale *= scaleFactor * tileScale;

                tile.transform.parent = transform;
                tile.transform.localPosition = new Vector3(i, 0, j) * tileScale;


                tile.GetComponent<MeshRenderer>().material = i % 2 == 1 ^ j % 2 == 0 ? mat1 : mat2;
            }
        }
    }
}
