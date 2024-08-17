using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;
using TMPro.EditorUtilities;

[CustomEditor(typeof(ShapeCreator))]
public class ShapeEditor : Editor
{
    ShapeCreator shapeCreator; 
    private bool righting;
    private bool needsRepaint;

    [SerializeField]
    private bool buttonToggled;

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("POV: you're an idiot"))
        {
            shapeCreator.MakeThingsRight();
        }
        if (GUILayout.Button("This Person is so Idiotic I can't Even Imagine Looking Through Their Eyes"))
        {
            shapeCreator.NowThisIsJustStupid();
        }
        buttonToggled = GUILayout.Toggle(buttonToggled, "ReSprite", new GUILayoutOption[0]);
        DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        needsRepaint = false;
        Event guiEvent = Event.current;
        Ray mouseRay = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);

        if (buttonToggled)
        {
            ChangeSquares(guiEvent, mouseRay);
        }
        else
        {
            AddSquares(guiEvent, mouseRay);
        }

        if (needsRepaint)
            HandleUtility.Repaint();

        if (guiEvent.type == EventType.Layout)
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
    }

    private void ChangeSquares(Event guiEvent, Ray mouseRay)
    {
        Vector3 gridMouse = shapeCreator.GetGridSpot(mouseRay.origin);

        shapeCreator.DrawSquare(gridMouse);

        if (guiEvent.type == EventType.MouseDrag || guiEvent.type == EventType.MouseDown)
        {
            if (guiEvent.button == 0)
            {
                GameObject toChange = Physics2D.OverlapBox(shapeCreator.GetGridSpot(mouseRay.origin), Vector3.one * 0.5f, 0).gameObject;
                toChange.GetComponent<MeshRenderer>().material = shapeCreator.sprite;
                toChange.transform.rotation = shapeCreator.preview.transform.rotation;
                needsRepaint = true;
                Undo.RegisterCompleteObjectUndo(toChange, "Set Wall Image");
            }
            else if (guiEvent.button == 1 && guiEvent.type != EventType.MouseDrag)
            {
                shapeCreator.RotateSquare();
                needsRepaint = true;
                guiEvent.Use();
            }
        }
    }

    private void AddSquares(Event guiEvent, Ray mouseRay)
    {
        if (guiEvent.type == EventType.MouseDrag || guiEvent.type == EventType.MouseDown)
        {
            if (guiEvent.button == 0 && !shapeCreator.AlreadyTaken(mouseRay.origin))
            {
                shapeCreator.AddTile(mouseRay.origin);
            }
            else if (guiEvent.button == 1)
            {
                shapeCreator.RemoveTile(mouseRay.origin);
                guiEvent.Use();
                righting = true;
            }
            needsRepaint = true;
        }
        if (guiEvent.type == EventType.MouseUp && guiEvent.button == 1)
            righting = false;

        if (!righting)
            Handles.DrawSolidRectangleWithOutline(new Rect(shapeCreator.GetGridSpot(mouseRay.origin) - (Vector3.one / 2), new Vector3(1, 1, 1)), Color.white, Color.white);
    }

    private void OnEnable()
    {
        shapeCreator = (ShapeCreator)target;
    }
}
