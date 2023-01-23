using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Original Author: GlynnLeine
/// Reviewer: YvensFaos
/// </summary>
[CustomPropertyDrawer(typeof(SceneAttribute))]
public class ScenePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            var sceneObject = GetSceneObject(property.stringValue);
            var scene = EditorGUI.ObjectField(position, label, sceneObject, typeof(SceneAsset), true);
            if (scene == null)
            {
                property.stringValue = "";
            }
            else if (!scene.name.Equals(property.stringValue))
            {
                var sceneObj = GetSceneObject(scene);
                if (sceneObj != null)
                {
                    property.stringValue = scene.name;
                }
            }
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use [Scene] with strings.");
        }
    }

    protected SceneAsset GetSceneObject(string assetName)
    {
        foreach (var editorScene in EditorBuildSettings.scenes)
        {
            if (editorScene.path.IndexOf(assetName, StringComparison.Ordinal) != -1)
            {
                return AssetDatabase.LoadAssetAtPath(editorScene.path, typeof(SceneAsset)) as SceneAsset;
            }
        }

        List<EditorBuildSettingsScene> scenes = EditorBuildSettings.scenes.ToList();

        var GUIDs = AssetDatabase.FindAssets(assetName + ".unity");
        if (GUIDs == null || GUIDs.Length == 0)
        {
            return null;
        }

        var assetPath = AssetDatabase.GUIDToAssetPath(GUIDs[0]);
        scenes.Add(new EditorBuildSettingsScene(assetPath, true));
        EditorBuildSettings.scenes = scenes.ToArray();
        return AssetDatabase.LoadAssetAtPath(assetPath, typeof(SceneAsset)) as SceneAsset;
    }

    protected SceneAsset GetSceneObject(UnityEngine.Object asset)
    {
        foreach (var editorScene in EditorBuildSettings.scenes)
        {
            if (editorScene.path.IndexOf(asset.name, StringComparison.Ordinal) != -1)
            {
                return asset as SceneAsset;
            }
        }

        List<EditorBuildSettingsScene> scenes = EditorBuildSettings.scenes.ToList();

        scenes.Add(new EditorBuildSettingsScene(AssetDatabase.GetAssetPath(asset), true));
        EditorBuildSettings.scenes = scenes.ToArray();
        return asset as SceneAsset;
    }
}