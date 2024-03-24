using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditorInternal;

/// <summary>
///
/// License:
/// Copyrighted to Brian "VerzatileDev" Lätt ©2024.
/// Do not copy, modify, or redistribute this code without prior consent.
/// All rights reserved.
///
/// </summary>
/// <Remarks>
/// 
/// Info Currently not available.
/// 
/// </Remarks>
public class GameObjectController : MonoBehaviour
{
    [System.Serializable]
    public class TargetObject
    {
        public GameObject gameObject;
        public bool isEnabled;
    }

    public List<TargetObject> targetObjects = new List<TargetObject>();

    [CustomEditor(typeof(GameObjectController))]
    public class ObjectControllerEditor : Editor
    {
        private SerializedProperty targetObjectsProperty;
        private ReorderableList targetObjectsList;

        private void OnEnable()
        {
            targetObjectsProperty = serializedObject.FindProperty("targetObjects");
            targetObjectsList = new ReorderableList(serializedObject, targetObjectsProperty, true, true, true, true);

            targetObjectsList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Target Objects");
            };

            targetObjectsList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = targetObjectsList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;

                EditorGUI.BeginProperty(rect, GUIContent.none, element.FindPropertyRelative("gameObject"));
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("gameObject"), GUIContent.none);
                EditorGUI.EndProperty();

                rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                EditorGUI.BeginProperty(rect, GUIContent.none, element.FindPropertyRelative("isEnabled"));
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("isEnabled"));
                EditorGUI.EndProperty();
            };

            targetObjectsList.elementHeightCallback = index =>
            {
                float elementHeight = EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
                return elementHeight;
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            targetObjectsList.DoLayoutList();

            EditorGUILayout.Space();

            GameObjectController objectController = (GameObjectController)target;

            if (GUILayout.Button("Toggle All Object States"))
            {
                objectController.ToggleAllObjectStates();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

    private void OnValidate()
    {
        EditorApplication.delayCall += () =>
        {
            for (int i = 0; i < targetObjects.Count; i++)
            {
                SetObjectState(i, targetObjects[i].isEnabled);
            }
        };
    }

    public void SetObjectState(int index, bool state)
    {
        if (index >= 0 && index < targetObjects.Count)
        {
            targetObjects[index].isEnabled = state;
            GameObject targetObject = targetObjects[index].gameObject;
            if (targetObject != null)
            {
                targetObject.SetActive(state);
            }
        }
    }

    public void ToggleAllObjectStates()
    {
        foreach (TargetObject targetObject in targetObjects)
        {
            if (targetObject.gameObject != null)
            {
                targetObject.isEnabled = !targetObject.isEnabled;
                targetObject.gameObject.SetActive(targetObject.isEnabled);
            }
        }
    }
}
