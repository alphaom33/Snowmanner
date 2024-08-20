using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMaker))]
public class TileEditor : Editor
{
    TileMaker tileMaker;

    private void OnSceneGUI()
    {
        Event guiEvent = Event.current;

        Ray mouseRay = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
        float drawPlaneHeight = 0;
        float dstToDrawPlane = (drawPlaneHeight - mouseRay.origin.y) / mouseRay.direction.y;
        Vector3 mousePosition = mouseRay.GetPoint(dstToDrawPlane);
        mousePosition = tileMaker.grid.GetCellCenterWorld(Vector3Int.RoundToInt(mousePosition / PlayerMovement.gridSize - new Vector3(1, 0, 1)));

        if ((guiEvent.type == EventType.MouseDown || guiEvent.type == EventType.MouseDrag) && guiEvent.button == 0 && !tileMaker.AlreadyTaken(mousePosition))
        {
            GameObject newby = Instantiate(tileMaker.prefab);
            PrefabUtility.ConvertToPrefabInstance(newby, tileMaker.prefab, new ConvertToPrefabInstanceSettings(), InteractionMode.AutomatedAction);

            newby.transform.position = mousePosition;
            newby.transform.parent = tileMaker.father.transform;
            newby.transform.localScale *= PlayerMovement.gridSize;

            newby.GetComponentInChildren<MeshRenderer>().material = Mathf.Abs(mousePosition.x) / PlayerMovement.gridSize % 2 == 1 ^ Mathf.Abs(mousePosition.z) / PlayerMovement.gridSize % 2 == 0 ? tileMaker.material1 : tileMaker.material2;

            newby.tag = tileMaker.tagger;

            Undo.RegisterCreatedObjectUndo(newby, "weee");
        }

            HandleUtility.Repaint();

        if (guiEvent.type == EventType.Layout)
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

        Handles.DrawWireCube(mousePosition, new Vector3(1, 0, 1) * PlayerMovement.gridSize);
    }

    private void OnEnable()
    {
        tileMaker = target as TileMaker;
    }
}
