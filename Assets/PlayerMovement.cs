using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveDist = 1.0f;
    public float moveSpeed = 2.0f;

    public UnityEvent moved;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        Vector3 input = new(-Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical"));
        if (input.magnitude > 0 && !running)
        {
            moved.Invoke();
            StartCoroutine(Pos(input));
        }
    }

    bool running = false;
    IEnumerator Pos(Vector3 input)
    {
        running = true;
        Vector3 filteredInput = Vector3Int.RoundToInt(input);
        Vector3 start = transform.position;
        for (float i = 0; i < 1; i += moveSpeed * Time.deltaTime)
        {
            transform.position = Utils.VecEaseOutInSine(start, start + (filteredInput * moveDist), i);
            yield return new WaitForEndOfFrame();
        }
        running = false;
    }
}
