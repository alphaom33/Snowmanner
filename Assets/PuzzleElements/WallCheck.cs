using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool CheckWall(Vector3 input)
    {
        Vector3 filtered = Vector3Int.RoundToInt(input);
        if (Physics.CheckBox(transform.position + (filtered * PlayerMovement.gridSize), new Vector3(1, 0, 1) * PlayerMovement.gridSize / 2, transform.rotation, Layers.wallMask))
        {
            return Physics.OverlapBox(transform.position + (filtered * PlayerMovement.gridSize), Vector3.one * PlayerMovement.gridSize / 2, transform.rotation, Layers.wallMask)[0].GetComponent<WallController>().GetWall();
        }

        if (Physics.CheckBox(transform.position + (filtered * PlayerMovement.gridSize), Vector3.one * PlayerMovement.gridSize / 2, transform.rotation, Layers.crateMask))
        {
            WallCheck crateController = Physics.OverlapBox(transform.position + (filtered * PlayerMovement.gridSize), Vector3.one * PlayerMovement.gridSize / 2, transform.rotation, Layers.crateMask)[0].GetComponent<WallCheck>();
            return crateController.CheckWall(input);
        }

        return false;
    }
}
