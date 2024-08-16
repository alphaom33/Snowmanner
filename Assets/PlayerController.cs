using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDist = 1.0f;
    public float moveSpeed = 2.0f;

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
        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical"));
        if (input.magnitude > 0)
        {
            StartCoroutine(Pos(input));
        }
    }

    IEnumerator Pos(Vector3 input)
    {
        Vector3 filteredInput = Vector3Int.RoundToInt(input);
        Vector3 start = transform.position;
        for (float i = 0; i < 1; i += moveSpeed * Time.deltaTime)
        {
            transform.position = Vector3.Lerp(start, start + (filteredInput * moveDist), i);
            yield return new WaitForEndOfFrame();
        }
    }
}
