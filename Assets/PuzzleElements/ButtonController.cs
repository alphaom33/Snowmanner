using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool unpressable;
    public bool pressed = false;
    public Transform button;

    public float dst;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Box") || other.CompareTag("Player")) && !pressed)
        {
            pressed = true;
            StartCoroutine(Animate(-1));
        }
       else if (!unpressable && other.CompareTag("Player"))
        {
            pressed = true;
            StartCoroutine(Animate(-1));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (unpressable && (other.CompareTag("Box") || other.CompareTag("Player")))
        {
            pressed = false;
            StartCoroutine(Animate(1));
        }
    }

    IEnumerator Animate(int sign)
    {
        Vector3 localPos = button.localPosition;
        for (float i = 0; i < 1; i += 2 * Time.deltaTime)
        {
            localPos.y = Utils.EaseOutInSine(-sign * dst, sign * dst, i);
            button.localPosition = localPos;
            yield return new WaitForEndOfFrame();
        }
    }
}
