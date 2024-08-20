using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heighter : MonoBehaviour
{
    public float speed;
    public const float epsilon = 0.01f;

    public bool running;

    // Update is called once per frame
    void Update()
    {
        if (!Physics.Raycast(new Ray(transform.position, Vector3.down), epsilon, Layers.floorMask) && !running)
        {
            if (Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit, 100, Layers.floorMask))
                StartCoroutine(Fall(hit));
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
        enabled = false;
    }

    IEnumerator Fall(RaycastHit hit)
    {
        Debug.Log("fall");
        running = true;
        float start = transform.position.y;
        for (float i = 0; i < 1; i += Time.deltaTime * speed)
        {
            transform.position =  new Vector3(transform.position.x, Utils.EaseInQuad(start, hit.point.y, i), transform.position.z);
            yield return new WaitForEndOfFrame();
        }
        running = false;
    }
}
