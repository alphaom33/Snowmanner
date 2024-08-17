using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    Vector3 lastDir;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.moved.AddListener(CheckSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckSelf(Vector3 dir)
    {
        lastDir = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Move());
            IEnumerator Move()
            {
                Vector3 filteredInput = Vector3Int.RoundToInt(lastDir);
                Vector3 start = transform.position;
                for (float i = 0; i < 1; i += 2 * Time.deltaTime)
                {
                    transform.position = Utils.VecEaseOutInSine(start, start + (filteredInput * PlayerMovement.gridSize), i);
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
}
