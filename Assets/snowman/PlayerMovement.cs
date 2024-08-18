using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Threading;
using Unity.PlasticSCM.Editor.WebApi;

public class PlayerMovement : MonoBehaviour
{
    public static float gridSize = 4;
    public float moveSpeed = 2.0f;
    bool running = false;

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
        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = CamerRelativate(input);
        if (input.magnitude > 0 && !running && !wallCheck.CheckWall(input))
        {
            moved.Invoke(input);
            StartCoroutine(Pos(input));
        }
    }

    Vector3 CamerRelativate(Vector3 input)
    {
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;

        float angle = Mathf.Atan2(forward.x, forward.z);

        angle = angle / Mathf.PI * 2;
        angle = Mathf.Round(angle);
        angle = angle / 2 * 180;
        Debug.Log(angle);

        return Quaternion.Euler(0, angle, 0) * input;
    }

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
