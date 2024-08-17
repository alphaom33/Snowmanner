using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public List<ButtonController> buttons;
    public bool last;

    private Collider myCollider;

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
            ack &= !button.pressed;
        }

        myCollider.enabled = ack;
    }
}
