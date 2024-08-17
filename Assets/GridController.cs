using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Transform child;

    public Material mat1;
    public Material mat2;

    public float scaleFactor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (MeshRenderer t in GetComponentsInChildren<MeshRenderer>())
        {
            if (t != child) Destroy(t.gameObject);
        }

        for (int i = 0; i < child.localScale.x; i++)
        {
            for (int j = 0; j < child.localScale.z; j++)
            {
                GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Plane);
                tile.transform.localScale *= scaleFactor;
                tile.transform.localPosition = new Vector3(i, 0, j);

                tile.transform.parent = transform;

                tile.GetComponent<MeshRenderer>().material = i % 2 == 1 ^ j % 2 == 0 ? mat1 : mat2;
            }
        }
    }
}
