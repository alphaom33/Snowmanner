using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool CheckWall(Vector3 input)
    {
        Vector3 filtered = Vector3Int.RoundToInt(input);
        if (Physics.CheckBox(transform.position + (filtered * PlayerMovement.gridSize), Vector3.one * PlayerMovement.gridSize / 2, transform.rotation, Layers.wallMask))
        {
            return true;
        }

        if (Physics.CheckBox(transform.position + (filtered * PlayerMovement.gridSize), Vector3.one * PlayerMovement.gridSize / 2, transform.rotation, Layers.crateMask))
        {
            WallCheck crateController = Physics.OverlapBox(transform.position + (filtered * PlayerMovement.gridSize), Vector3.one * PlayerMovement.gridSize / 2, transform.rotation, Layers.crateMask)[0].GetComponent<WallCheck>();
            return crateController.CheckWall(input);
        }

        return false;
    }
}
