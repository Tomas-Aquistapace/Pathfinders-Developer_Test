using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridGenerator))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridGenerator generator = (GridGenerator)target;

        if(GUILayout.Button("Create Grid"))
        {
            generator.GenerateGrid();
        }
        
        if(GUILayout.Button("Delete Grid"))
        {
            generator.DeleteGrid();
        }
    }
}
