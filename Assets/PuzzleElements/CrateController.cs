using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    Vector3 lastDir;
    public static bool back;
    Heighter toStop;

    // Start is called before the first frame update
    void Start()
    {
        toStop = GetComponent<Heighter>();
        PlayerMovement.moved.AddListener(CheckSelf);
    }

    void CheckSelf(Vector3 dir)
    {
        lastDir = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            toStop.running = true;
            Vector3 filteredInput = Vector3Int.RoundToInt(lastDir);
            StartCoroutine(Move());
            IEnumerator Move()
            {
                Vector3 start = transform.position;
                for (float i = 0; i < 1; i += 2 * Time.deltaTime)
                {
                    transform.position = Utils.VecEaseOutInSine(start, start + (filteredInput * PlayerMovement.gridSize), i);
                    yield return new WaitForEndOfFrame();
                }
                toStop.running = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (lastDir * PlayerMovement.gridSize), Vector3.one * PlayerMovement.gridSize);
    }
}
