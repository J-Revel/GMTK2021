using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AnimEditor : EditorWindow
{
    [MenuItem ("Window/Anim Editor")]

    public static void  ShowWindow () {
        EditorWindow.GetWindow(typeof(AnimEditor));
    }

    private bool groupEnabled = false;
    private bool myBool;
    private float myFloat;
    private SpriteAnimList animList;
    private SerializedObject serializedAnimList;

    void OnGUI()
    {
        SpriteAnimList newAnimList = (SpriteAnimList)EditorGUILayout.ObjectField(animList, typeof(SpriteAnimList), false, null);
        if(newAnimList != animList)
        {
            serializedAnimList = new SerializedObject(newAnimList);
            animList = newAnimList;
        }
        SerializedProperty animsListProp = serializedAnimList.FindProperty("spriteAnims");
        for(int i=0; i<animsListProp.arraySize; i++)
        {
            string animName = animsListProp.GetArrayElementAtIndex(i).FindPropertyRelative("animName").stringValue;
            EditorGUILayout.SelectableLabel(animName);
        }
    }
}
