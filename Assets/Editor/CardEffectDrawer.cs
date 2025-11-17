using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;

[CustomPropertyDrawer(typeof(CardEffect), true)]  // Applies to CardEffect and all subclasses
public class CardEffectDrawer : PropertyDrawer
{
    // Cache of available subclass types for dropdown
    private static Type[] subclassTypes;
    private static string[] subclassNames;

    static CardEffectDrawer()
    {
        // Find all non-abstract subclasses of CardEffect
        subclassTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(CardEffect)) && !type.IsAbstract)
            .ToArray();

        // Create display names for the dropdown (e.g., "Projectile Effect", "Walk Effect")
        subclassNames = subclassTypes.Select(type => ObjectNames.NicifyVariableName(type.Name)).ToArray();
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // If the instance is null, show a dropdown to create a new subclass instance
        if (property.managedReferenceValue == null)
        {
            // Draw label
            Rect labelRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(labelRect, label);

            // Draw dropdown button next to it
            Rect buttonRect = new Rect(position.x + position.width - 100, position.y, 100, EditorGUIUtility.singleLineHeight);
            if (GUI.Button(buttonRect, "Select Type"))
            {
                // Show a generic menu with subclass options
                GenericMenu menu = new GenericMenu();
                for (int i = 0; i < subclassTypes.Length; i++)
                {
                    int index = i;  // Capture for closure
                    menu.AddItem(new GUIContent(subclassNames[index]), false, () =>
                    {
                        // Create instance of selected type and assign
                        var instance = Activator.CreateInstance(subclassTypes[index]);
                        property.managedReferenceValue = instance;
                        property.serializedObject.ApplyModifiedProperties();
                    });
                }
                menu.ShowAsContext();
            }
        }
        else
        {
            // If already assigned, draw the default property fields and add a "Change Type" button
            EditorGUI.PropertyField(position, property, label, true);

            // Optional: Add a button to change type
            Rect changeButtonRect = new Rect(position.x + position.width - 100, position.y, 100, EditorGUIUtility.singleLineHeight);
            if (GUI.Button(changeButtonRect, "Change Type"))
            {
                // Similar menu to change the type (replaces current instance)
                GenericMenu menu = new GenericMenu();
                for (int i = 0; i < subclassTypes.Length; i++)
                {
                    int index = i;
                    menu.AddItem(new GUIContent(subclassNames[index]), false, () =>
                    {
                        var instance = Activator.CreateInstance(subclassTypes[index]);
                        property.managedReferenceValue = instance;
                        property.serializedObject.ApplyModifiedProperties();
                    });
                }
                menu.ShowAsContext();
            }
        }

        EditorGUI.EndProperty();
    }

    // Override height to accommodate expanded fields
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.managedReferenceValue == null)
        {
            return EditorGUIUtility.singleLineHeight;  // Just the button if null
        }
        return EditorGUI.GetPropertyHeight(property, true);  // Default height for expanded fields
    }
}