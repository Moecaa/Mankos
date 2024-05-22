using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(SizeChanger))]
public class SizeChangerEditor : Editor
{
    private SerializedProperty playerManagerProp;

    private void OnEnable()
    {
        playerManagerProp = serializedObject.FindProperty("m_Script");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUI.enabled = false;
        EditorGUILayout.PropertyField(playerManagerProp);
        GUI.enabled = true;

        base.OnInspectorGUI();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Scale options", EditorStyles.boldLabel);

        if (GUILayout.Button("Scale up"))
        {
            SizeChanger targetManager = (SizeChanger)target;
            targetManager.scaling = true;
            targetManager.scaleup = true;

            targetManager.scaledown = false;
            targetManager.resetscale = false;
        }

        if (GUILayout.Button("Scale down"))
        {
            SizeChanger targetManager = (SizeChanger)target;
            targetManager.scaling = true;
            targetManager.scaledown = true;

            targetManager.scaleup = false;
            targetManager.resetscale = false;
        }

        if (GUILayout.Button("Reset scale"))
        {
            SizeChanger targetManager = (SizeChanger)target;
            targetManager.scaling = true;
            targetManager.resetscale = true;

            targetManager.scaleup = false;
            targetManager.scaledown = false;
        }

        if (GUILayout.Button("Stop scaling"))
        {
            SizeChanger targetManager = (SizeChanger)target;
            targetManager.scaling = false;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
