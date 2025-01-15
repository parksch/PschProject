using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum InteractableType
{
    Hide,
    FrontLock,
}

[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
    [SerializeField] InteractableType interactableType;
    [SerializeField] GameObject frontLockObject;
    [SerializeField] Button button;
    [SerializeField] Text text;

    [SerializeField] Color interactable;
    [SerializeField] Color notInteractable;

    public InteractableType InteractableType => interactableType;
    public Text Text => text;

    public void SetInterractable(bool value)
    {
        button.interactable = value;

        if (text != null)
        {
            text.color = value ? interactable : notInteractable;
        }

        ChangeButton(value);
    }

    void ChangeButton(bool value)
    {
        switch (interactableType)
        {
            case InteractableType.Hide:
                gameObject.SetActive(value);
                break;
            case InteractableType.FrontLock:
                if (frontLockObject != null)
                {
                    frontLockObject.SetActive(!value);
                }
                break;
            default:
                break;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(UIButton))]
public class UIButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("interactableType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("button"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("text"));

        UIButton uiButton = (UIButton)target;
        if (uiButton.InteractableType == InteractableType.FrontLock)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("frontLockObject"));
        }

        if (uiButton.Text != null)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("interactable"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("notInteractable"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif