using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerRotate : MonoBehaviour
{

    public float targetRotX;
    public float targetRotY;
    public float bottomClamp = -30f;
    public float topClamp = 70f;

    public float Min;

    public float speed;

    public GameObject CinemachineCameraTarget;

    // Start is called before the first frame update
    void Start()
    {
        targetRotX = -90;
        targetRotY = 0;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {

        targetRotY += Input.GetAxis("Mouse Y") * Time.deltaTime * speed;
        targetRotX += -Input.GetAxis("Mouse X") * Time.deltaTime * speed;

        targetRotY = ClampAngle(targetRotY, bottomClamp, topClamp);
        targetRotX = ClampAngle(targetRotX, float.MinValue, float.MaxValue);

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(targetRotY, targetRotX, 0);
    }

    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;

        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

}