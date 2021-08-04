using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI;
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
    private int selectedAnimIndex = 0;
    private float selectedAnimTime = 0;
    private float displaySize = 1;
    private Vector2 scrollPos;
    

    void OnGUI()
    {
        SpriteAnimList newAnimList = (SpriteAnimList)EditorGUILayout.ObjectField(animList, typeof(SpriteAnimList), false, null);
        if((serializedAnimList == null && newAnimList != null) || newAnimList != animList)
        {
            serializedAnimList = new SerializedObject(newAnimList);
            animList = newAnimList;
            selectedAnimIndex = 0;
            selectedAnimTime = 0;
        }
        if(serializedAnimList != null)
        {
            SerializedProperty animsListProp = serializedAnimList.FindProperty("spriteAnims");
            string[] animNames = new string[animsListProp.arraySize];
            for(int i=0; i<animsListProp.arraySize; i++)
            {
                SerializedProperty animElement = animsListProp.GetArrayElementAtIndex(i);
                string animName = animElement.FindPropertyRelative("name").stringValue;
                animNames[i] = animName;
            }
            selectedAnimIndex = EditorGUILayout.Popup("animation", selectedAnimIndex, animNames);
            SpriteAnimConfig spriteAnim = newAnimList.spriteAnims[selectedAnimIndex].spriteAnim;
            Sprite sprite = spriteAnim.GetSprite(selectedAnimTime);
            displaySize = EditorGUILayout.Slider(displaySize, 0.01f, 10);
            selectedAnimTime = EditorGUILayout.Slider(selectedAnimTime, 0, spriteAnim.framePerSecond * spriteAnim.sprites.Length);
            Rect rect = GUILayoutUtility.GetRect(100.0f, 100.0f, GUILayout.ExpandHeight(true));
                DrawSpriteScrollView(rect, sprite);
        }
    }

    private bool drag = false;
    private Vector2 lastDragPos;
    private void DrawSpriteScrollView(Rect rect, Sprite sprite)
    {
        var e = Event.current;
        if (e.isMouse && e.type == EventType.MouseDown && e.button != 0)
        {
            lastDragPos = e.mousePosition;
            drag = true;
        }
        else if(e.isMouse && e.type == EventType.MouseUp && e.button != 0)
        {
            drag = false;
        }
        if(drag  && e.mousePosition != lastDragPos)
        {
            scrollPos += lastDragPos - e.mousePosition;
            lastDragPos = e.mousePosition;
            Repaint();
        }
        Vector2 center = new Vector2(rect.width / 2, rect.height / 2);
        scrollPos = GUI.BeginScrollView(
            rect, 
            scrollPos, 
            new Rect(-sprite.pivot.x * displaySize, (-sprite.rect.height+sprite.pivot.y) * displaySize, 
            rect.width + sprite.rect.width * displaySize, rect.height + sprite.rect.height * displaySize)
        );
        DrawOnGUISprite(sprite, center, displaySize);
        EditorGUI.DrawRect(new Rect(center.x - 2, center.y - 2, 4, 4), Color.green);
        GUI.EndScrollView();
    }
    
    void DrawOnGUISprite(Sprite aSprite, Vector2 position, float scale)
    {
        Rect c = aSprite.rect;
        float spriteW = c.width;
        float spriteH = c.height;
        Rect rect = new Rect(position.x - aSprite.pivot.x * scale, position.y - (spriteH - aSprite.pivot.y) * scale, spriteW * scale, spriteH * scale);
        float ratio = rect.width / c.width;
        rect.width = rect.height * c.width / c.height;
        if (Event.current.type == EventType.Repaint)
        {
            var tex = aSprite.texture;
            c.xMin /= tex.width;
            c.xMax /= tex.width;
            c.yMin /= tex.height;
            c.yMax /= tex.height;
            GUI.DrawTextureWithTexCoords(rect, tex, c);
        }
    }
}
