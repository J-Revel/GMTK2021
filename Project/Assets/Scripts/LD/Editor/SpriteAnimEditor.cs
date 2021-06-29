using UnityEditor;
using UnityEngine;
using System;

[CustomPropertyDrawer( typeof( SpriteAnimRef ) )]
public class SpriteAnimRefDrawer : PropertyDrawer {

    public override float GetPropertyHeight( SerializedProperty property, GUIContent label ) {
        // The 6 comes from extra spacing between the fields (2px each)
        return EditorGUIUtility.singleLineHeight * 4 + 6;
    }

    public override void OnEditorGUI( Rect position, SerializedProperty property, GUIContent label ) {
        // EditorGUI.BeginProperty( position, label, property );

        // EditorGUI.LabelField( position, label );

        // EditorGUI.indentLevel++;

        // var nameRect = EditorGUILayout.BeginHorizontal ();
        // EditorGUI.PropertyField( nameRect, property.FindPropertyRelative( "animName" ), GUIContent.none);
        // EditorGUILayout.EndHorizontal();
        var nameRect = EditorGUILayout.BeginHorizontal ();
        EditorGUI.Popup(nameRect, 0, new string[]{"a", "b", "c", "d"});
        EditorGUILayout.EndHorizontal();

        // EditorGUI.indentLevel--;

        // EditorGUI.EndProperty();
    }
}