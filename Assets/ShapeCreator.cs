using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class ShapeCreator : MonoBehaviour
{
    public Grid grid;
    public GameObject currentFather;
    public GameObject prefab;

    public Material sprite;
    public GameObject preview;

    private void Start()
    {
        Destroy(preview);
    }

    public void AddTile(Vector3 position)
    {
        Debug.Log(position);
        if (currentFather != null)
        {
            GameObject newTile = Instantiate(prefab, currentFather.transform);
            newTile.transform.position = GetGridSpot(position);
            newTile.transform.position = new Vector3(newTile.transform.position.x, 0, newTile.transform.position.z);

            newTile.name = "Tile " + (currentFather.GetComponentsInChildren<Transform>().Length - 1);
            PrefabUtility.ConvertToPrefabInstance(newTile, prefab, new ConvertToPrefabInstanceSettings(), InteractionMode.AutomatedAction);
            Undo.RegisterCreatedObjectUndo(newTile, "weee");
        }
    }

    public void RemoveTile(Vector3 position)
    {
        position = RoundVector(GetGridSpot(position));
        foreach (Transform t in currentFather.GetComponentsInChildren<Transform>().Where((t, x) => x != 0))
        {
            if (RoundVector(t.position).Equals(position))
            {
                Undo.DestroyObjectImmediate(t.gameObject);
            }
        }
    }

    public Vector3 GetGridSpot(Vector3 position)
    {
        Debug.Log(grid.GetCellCenterWorld(grid.WorldToCell(position)));
        return grid.GetCellCenterWorld(grid.WorldToCell(position));
    }

    public bool AlreadyTaken(Vector3 position)
    {
        position = RoundVector(GetGridSpot(position));
        foreach (Transform t in currentFather.GetComponentsInChildren<Transform>().Where((t, x) => x != 0))
        {
            if (RoundVector(t.position).Equals(position))
            {
                return true;
            }
        }
        return false;
    }

    private Vector3 RoundVector(Vector3 value)
    {
        return new Vector3(Mathf.RoundToInt(value.x), Mathf.RoundToInt(value.y));
    }

    public void MakeThingsRight()
    {
        foreach (Transform t in currentFather.GetComponentsInChildren<Transform>().Where((t, x) => x != 0))
        {
            ConvertToPrefabInstanceSettings convertToPrefabInstanceSettings = new()
            {
                changeRootNameToAssetName = false,
                gameObjectsNotMatchedBecomesOverride = true,
                recordPropertyOverridesOfMatches = true
            };

            PrefabUtility.ConvertToPrefabInstance(t.gameObject, prefab, convertToPrefabInstanceSettings, InteractionMode.AutomatedAction);
        }
    }

    public void NowThisIsJustStupid()
    {
        foreach (Transform t in currentFather.GetComponentsInChildren<Transform>().Where((t, x) => x != 0))
        {
            PrefabReplacingSettings prefabReplacingSettings = new()
            {
                changeRootNameToAssetName = false,
            };
            PrefabUtility.ReplacePrefabAssetOfPrefabInstance(t.gameObject, prefab, prefabReplacingSettings, InteractionMode.AutomatedAction);
        }
    }

    public void RotateSquare()
    {
        preview.transform.Rotate(new Vector3(0, 0, 90));
    }

    public void DrawSquare(Vector3 gridMouse)
    {
        if (preview == null)
        {
            preview = Instantiate(prefab);
            preview.transform.parent = transform;
        }
        preview.GetComponent<MeshRenderer>().material = sprite;
        preview.transform.position = gridMouse;
    }

}
#endif
