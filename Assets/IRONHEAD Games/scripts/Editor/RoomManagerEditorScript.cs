using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.HelpBox("This script is responsible for creating and joining rooms",MessageType.Info);

        RoomManager roomManager = (RoomManager)target;
        

        if (GUILayout.Button("Join School Room"))
        {
            roomManager.OnEnterRoomButtonClicked_School();
        }

        if (GUILayout.Button("Join Outdoor Room"))
        {
            roomManager.OnEnterRoomButtonClicked_Outdoor();
        }
    }
}
