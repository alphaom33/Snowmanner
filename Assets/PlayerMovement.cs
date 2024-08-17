using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float gridSize = 4;
    public float moveSpeed = 2.0f;

    public static UnityEvent<Vector3> moved = new();

    WallCheck wallCheck;

    // Start is called before the first frame update
    void Start()
    {
        wallCheck = GetComponent<WallCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        Vector3 input = new(-Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical"));
        if (input.magnitude > 0 && !running && !wallCheck.CheckWall(input))
        {
            moved.Invoke(input);
            StartCoroutine(Pos(input));
        }
    }

    bool running = false;
    IEnumerator Pos(Vector3 input)
    {
        running = true;
        Vector3 filteredInput = Vector3Int.RoundToInt(input);
        Vector3 start = transform.position;
        Vector3 end = start + (filteredInput * gridSize);
        for (float i = 0; i < 1; i += moveSpeed * Time.deltaTime)
        {
            if (CrateController.back)
            {
                (start, end) = (end, start);
                CrateController.back = false;
            }
            transform.position = Utils.VecEaseOutInSine(start, end, i);
            yield return new WaitForEndOfFrame();
        }
        running = false;
    }
}
