using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(NetworkUI))]
public class NetworkUIEditor : Editor {

    private SerializedProperty discovery;
    private SerializedProperty buttonClick;
    private SerializedProperty buttonPrefab;
    private SerializedProperty matchJoinPanelPrefab;
    private SerializedProperty levelSelectPanel;
    private SerializedProperty matchSelectPanel;
    private SerializedProperty errorMessageObject;
    private SerializedProperty graphicsSlider;
    private SerializedProperty graphicsText;
    private SerializedProperty muteButton;
    private SerializedProperty loadingPanel;
    private SerializedProperty levels;

    public void OnEnable () {
        discovery = serializedObject.FindProperty("discovery");
        buttonClick = serializedObject.FindProperty("buttonClick");
        buttonPrefab = serializedObject.FindProperty("buttonPrefab");
        matchJoinPanelPrefab = serializedObject.FindProperty("matchJoinPanelPrefab");
        levelSelectPanel = serializedObject.FindProperty("levelSelectPanel");
        matchSelectPanel = serializedObject.FindProperty("matchSelectPanel");
        errorMessageObject = serializedObject.FindProperty("errorMessageObject");
        graphicsSlider = serializedObject.FindProperty("graphicsSlider");
        graphicsText = serializedObject.FindProperty("graphicsText");
        muteButton = serializedObject.FindProperty("muteButton");
        loadingPanel = serializedObject.FindProperty("loadingPanel");
        levels = serializedObject.FindProperty("levels");
    }

    public override void OnInspectorGUI () {
        serializedObject.Update();

        EditorGUILayout.PropertyField(discovery);
        EditorGUILayout.PropertyField(buttonClick);
        EditorGUILayout.PropertyField(buttonPrefab);
        EditorGUILayout.PropertyField(matchJoinPanelPrefab);
        EditorGUILayout.PropertyField(levelSelectPanel);
        EditorGUILayout.PropertyField(matchSelectPanel);
        EditorGUILayout.PropertyField(loadingPanel);
        EditorGUILayout.PropertyField(errorMessageObject);
        EditorGUILayout.PropertyField(graphicsSlider);
        EditorGUILayout.PropertyField(graphicsText);
        EditorGUILayout.PropertyField(muteButton);

        EditorGUILayout.PropertyField(levels);
        EditorGUI.indentLevel += 1;
        if (levels.isExpanded) {
            EditorGUILayout.PropertyField(levels.FindPropertyRelative("Array.size"));
            for (int i = 0; i < levels.arraySize; i++) {
                string newVal = "";
                SceneAsset oldScene = GetSceneObject(levels.GetArrayElementAtIndex(i).stringValue);
                SceneAsset newScene = EditorGUILayout.ObjectField("Level ", oldScene, typeof(SceneAsset), true) as SceneAsset;
                if (newScene != null) {
                    SceneAsset scene = GetSceneObject(newScene.name);
                    if (scene != null) {
                        newVal = scene.name;
                    }
                }
                levels.GetArrayElementAtIndex(i).stringValue = newVal;
            }
        }
        EditorGUI.indentLevel -= 1;

        serializedObject.ApplyModifiedProperties();
    }

    private SceneAsset GetSceneObject(string sceneObjectName) {
        if (string.IsNullOrEmpty(sceneObjectName)) {
            return null;
        }
 
        foreach (EditorBuildSettingsScene editorScene in EditorBuildSettings.scenes) {
            if (editorScene.path.IndexOf(sceneObjectName) != -1) {
                return AssetDatabase.LoadAssetAtPath<SceneAsset>(editorScene.path);
            }
        }
        Debug.LogWarning("Scene [" + sceneObjectName + "] cannot be used. Add this scene to the 'Scenes in the Build' in build settings.");
        return null;
    }
}