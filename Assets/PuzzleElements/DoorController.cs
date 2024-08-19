using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public List<ButtonController> buttons;
    public bool last = false;

    private Collider myCollider;

    public float speed;
    public int dir;
    public Transform model;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        bool ack = true;
        foreach (var button in buttons)
        {
            ack &= button.pressed;
        }

        if (last != ack)
        {
            StartCoroutine(DoorSwing());
        }
        myCollider.enabled = !ack;
        last = ack;
    }

    IEnumerator DoorSwing()
    {
        float start;
        float end;
        if (!last)
        {
            start = 0;
            end = 90 * dir;
        }
        else
        {
            start = 90 * dir;
            end = 0;
        }

        for (float i = 0; i < 1; i += Time.deltaTime * speed)
        {
            model.localRotation = Quaternion.Euler(model.localEulerAngles.x, Utils.EaseOutInSine(start, end, i), model.localEulerAngles.z);
            yield return new WaitForEndOfFrame();
        }
    }
}
