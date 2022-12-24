#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Made for only testing purposes so use at own discretion
///<summary> A Unity debugger window that allows you to subscribe values you want to debug in realtime easily </summary>
public class RealtimeDebugger : EditorWindow
{
    static Dictionary<string, object> debugProperties = new Dictionary<string, object>();

    [MenuItem("Toolkit/RealtimeDebugger")]
    static void Init()
    {
        GetWindow<RealtimeDebugger>();
    }

    void OnGUI()
    {
        foreach (var debugProperty in debugProperties)
        {
            DisplayObject(debugProperty.Key, debugProperty.Value);
        }
        
        if (GUILayout.Button("Clear debug properties"))
        {
            debugProperties.Clear();
        }
    }

    void DisplayObject(string label, object value)
    {
        if (value.GetType().IsPrimitive)
        {
            switch (value)
            {
                case int integer:
                    EditorGUILayout.IntField(label, integer);
                    break;
                case float floatingPoint:
                    EditorGUILayout.FloatField(label, floatingPoint);
                    break;
                case bool boolean:
                    EditorGUILayout.Toggle(label, boolean);
                    break;
                case string text:
                    EditorGUILayout.LabelField(label,text);
                    break;
                case double doubleFloatingPoint:
                    EditorGUILayout.DoubleField(label, doubleFloatingPoint);
                    break;
                default:
                    throw new System.Exception("Unexpected primitive type");
            }
        }
        else if (value is Object o)
        {
            EditorGUILayout.ObjectField(label, o, o.GetType(), false);
        }
        else
        {
            switch (value)
            {
                case Vector2 vec2:
                    EditorGUILayout.Vector2Field(label, vec2);
                    break;
                case Vector3 vec3:
                    EditorGUILayout.Vector3Field(label, vec3);
                    break;
                default:
                    Debug.Log($"Can't identify type: {value.GetType().Name}");
                    break;
            }

        }

        //Ensure that the window updates regardless of window focus
        Repaint();
    }

    ///<summary>Add property to debug window. Only supports primitive types and vectors (and possibly UnityEngine.Object types).</summary>
    public static void AddDebugProperty(string propertyName, object property)
    {
        debugProperties[propertyName] = property;
    }
}
#else
public class RealtimeDebugger
{
    public static void AddDebugProperty(string propertyName, object property)
    {
    }
}
#endif