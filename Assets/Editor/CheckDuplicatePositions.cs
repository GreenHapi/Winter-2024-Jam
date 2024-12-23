using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckDuplicatePositions : EditorWindow {
    [MenuItem("Tools/Check Duplicate Positions")]
    public static void ShowWindow() {
        GetWindow<CheckDuplicatePositions>("Check Duplicate Positions");
    }

    private void OnGUI() {
        if (GUILayout.Button("Check Selected Objects for Duplicate Positions")) {
            CheckForDuplicatePositions();
        }
    }

    private void CheckForDuplicatePositions() {
        // Get all selected objects in the editor
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0) {
            Debug.LogWarning("No objects selected.");
            return;
        }

        // Dictionary to store positions and corresponding object names
        Dictionary<Vector3, List<string>> positionMap = new Dictionary<Vector3, List<string>>();

        // Populate the dictionary
        foreach (var obj in selectedObjects) {
            Vector3 position = obj.transform.position;

            if (!positionMap.ContainsKey(position)) {
                positionMap[position] = new List<string>();
            }

            positionMap[position].Add(obj.name);
        }

        // Find duplicates and log them
        bool duplicatesFound = false;

        foreach (var entry in positionMap) {
            if (entry.Value.Count > 1) {
                duplicatesFound = true;
                Debug.Log($"Duplicate position {entry.Key}: {string.Join(", ", entry.Value)}");
            }
        }

        if (!duplicatesFound) {
            Debug.Log("No duplicate positions found among the selected objects.");
        }
    }
}