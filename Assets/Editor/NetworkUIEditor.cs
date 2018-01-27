using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(NetworkUI))]
public class NetworkUIEditor : Editor {

    SerializedProperty buttonPrefab;
    SerializedProperty levels;

    Object scene;

    public void OnEnable () {
        buttonPrefab = serializedObject.FindProperty("buttonPrefab");
        levels = serializedObject.FindProperty("levels");
    }

    public override void OnInspectorGUI () {
        serializedObject.Update();

        EditorGUILayout.PropertyField(buttonPrefab);

        List<SceneAsset> scenes = new List<SceneAsset>();
        EditorGUILayout.PropertyField(levels);
        EditorGUI.indentLevel += 1;
        if (levels.isExpanded) {
            EditorGUILayout.PropertyField(levels.FindPropertyRelative("Array.size"));
            for (int i = 0; i < levels.arraySize; i++) {
                scene = EditorGUILayout.ObjectField("Level ", scene, typeof(SceneAsset), true) as SceneAsset;
                if (scene != null) {
                    scenes.Add(scene as SceneAsset);
                }
            }
        }
        EditorGUI.indentLevel -= 1;

        string[] levelNames = scenes.ConvertAll<string>(scene => scene.name).ToArray();
        for (int i = 0; i < levelNames.Length; i++) {
            levels.GetArrayElementAtIndex(i).stringValue = levelNames[i];
        }

        serializedObject.ApplyModifiedProperties();
    }
}