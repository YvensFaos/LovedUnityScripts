using System;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Events;


[Serializable]
public class CollisionEvent : ScriptableObject
{
	public enum CollisionTypes { Tag,Script,Name,Object }
    private Type type;
    [HideInInspector] public CollisionTypes collisionType;
    [HideInInspector] public string typeName = "";
    [HideInInspector] public string tagName = "";
    [HideInInspector] public string objectName = "";
    [HideInInspector] public GameObject targetObject;
    [HideInInspector] public bool activateWithAnything = false;
    [SerializeField] public int test;

    public UnityEvent onTriggerEnter = new UnityEvent();    
    public UnityEvent onTriggerStay = new UnityEvent();
    public UnityEvent onTriggerExit = new UnityEvent();

    public UnityEvent onCollisionEnter = new UnityEvent();
    public UnityEvent onCollisionStay = new UnityEvent();
    public UnityEvent onCollisionExit = new UnityEvent();

    public void HandleEvent(UnityEvent unityEvent, GameObject col)
    {
        //No functions anyways
        if (unityEvent == null)
            return;

        //Always activate if true
        if (activateWithAnything)
        {
            unityEvent.Invoke();
            return;
        }

        switch (collisionType)
        {
            case CollisionTypes.Tag:
                try
                {
                    if (col.CompareTag(tagName)) unityEvent.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogError("Does the tag: "+tagName+ " exist? Otherwhise the function contains an error. Error message from object: "+name,this);
                    Debug.LogError(e.ToString());
                }
                break;
            case CollisionTypes.Script:
                InvokeByType(unityEvent, col);
                break;
            case CollisionTypes.Name:
                if (col.name == objectName) unityEvent.Invoke();
                break;
            case CollisionTypes.Object:
                if (col.gameObject == targetObject) unityEvent.Invoke();
                break;
        }
    }

    private void InvokeByType(UnityEvent unityEvent, GameObject col)
    {
        if (type == null && typeName != null)
            type = Type.GetType(typeName);

        if (col.transform.GetComponent(type))
            unityEvent.Invoke();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CollisionEvent),true)]
public class EventTriggerEditor : Editor
{
    private CollisionEvent trigger;
    private bool showTriggerEvents = false;
    private bool showCollisionEvents = false;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (trigger == null)
            trigger = target as CollisionEvent;

        trigger.activateWithAnything = EditorGUILayout.Toggle("Activate with anything", trigger.activateWithAnything);
        if (!trigger.activateWithAnything)
        {
            trigger.collisionType = (CollisionEvent.CollisionTypes) EditorGUILayout.EnumPopup("Type", trigger.collisionType);

            switch (trigger.collisionType)
            {
                case CollisionEvent.CollisionTypes.Tag:
                    SelectCollisionByTag();
                    break;
                case CollisionEvent.CollisionTypes.Script:
                    SelectCollisionByType();
                    break;
                case CollisionEvent.CollisionTypes.Name:
                    SelectCollisionByName();
                    break;
                case CollisionEvent.CollisionTypes.Object:
                    SelectCollisionByObject();
                    break;
            }
        }

        //Unity events
        GUI.color = Color.grey * 0.5f;
        GUILayout.BeginVertical("Box");
        GUI.color = Color.white;

        showTriggerEvents = EditorGUILayout.Foldout(showTriggerEvents, "Show trigger events");
        if (showTriggerEvents)
        {
            //Trigger events
            SerializedProperty onTriggerEnterProperty = serializedObject.FindProperty("onTriggerEnter");
            EditorGUILayout.PropertyField(onTriggerEnterProperty);
            SerializedProperty onTriggerStayProperty = serializedObject.FindProperty("onTriggerStay");
            EditorGUILayout.PropertyField(onTriggerStayProperty);
            SerializedProperty onTriggerExitProperty = serializedObject.FindProperty("onTriggerExit");
            EditorGUILayout.PropertyField(onTriggerExitProperty);
        }

        showCollisionEvents = EditorGUILayout.Foldout(showCollisionEvents, "Show collision events");
        if (showCollisionEvents)
        {
            //Collision events
            SerializedProperty onCollisionEnterProperty = serializedObject.FindProperty("onCollisionEnter");
            EditorGUILayout.PropertyField(onCollisionEnterProperty);
            SerializedProperty onCollisionStayProperty = serializedObject.FindProperty("onCollisionStay");
            EditorGUILayout.PropertyField(onCollisionStayProperty);
            SerializedProperty onCollisionExitProperty = serializedObject.FindProperty("onCollisionExit");
            EditorGUILayout.PropertyField(onCollisionExitProperty);
        }
        GUILayout.EndVertical();


        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);
    }

    private void SelectCollisionByTag()
    {
        trigger.tagName = EditorGUILayout.TextField("Tag", trigger.tagName);
    }

    private void SelectCollisionByName()
    {
        trigger.objectName = EditorGUILayout.TextField("Name", trigger.objectName);
    }

    private void SelectCollisionByType()
    {
        Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();

        List<Type> types = new List<Type>();
        List<string> enumStrings = new List<string>();
        foreach (Type type in allTypes)
        {
            if (type.IsSubclassOf(typeof(MonoBehaviour)))
            {
                types.Add(type);
                enumStrings.Add(type.FullName);
            }
        }

        int selected = trigger.typeName != null ? types.IndexOf(Type.GetType(trigger.typeName)) : 0;
        int id = EditorGUILayout.Popup("Script Type", selected, enumStrings.ToArray());
        if (id == -1) return;
        trigger.typeName = types[id].Name;
    }

    private void SelectCollisionByObject()
    {
        trigger.targetObject = (GameObject) EditorGUILayout.ObjectField(trigger.targetObject, typeof(GameObject), true);
    }
}
#endif