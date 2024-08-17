using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public Vector3 offset;
    public int current;
    public int childIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerGrower>().current >= current)
        {
            transform.parent = other.transform.GetChild(childIndex);
            transform.localPosition = offset;
        }
    }
}
