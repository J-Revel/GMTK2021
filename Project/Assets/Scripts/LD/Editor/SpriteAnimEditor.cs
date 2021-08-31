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
    private SpriteAnimList animList;
    private SerializedObject serializedAnimList;
    private int selectedAnimIndex = 0;
    private int selectedAnimSprite = 0;
    private int selectedPointIndex = 0;
    private float displaySize = 1;
    private Vector2 scrollPos;
    private string newPointName;
    private Color[] pointColors;

    void OnGUI()
    {
        SpriteAnimList newAnimList = (SpriteAnimList)EditorGUILayout.ObjectField(animList, typeof(SpriteAnimList), false, null);
        if(newAnimList.actionPointNames == null)
            newAnimList.actionPointNames = new string[0];
        for(int i=0; i<newAnimList.actionPointNames.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
                newAnimList.actionPointNames[i] = EditorGUILayout.TextField(newAnimList.actionPointNames[i]);
                if(GUILayout.Button("Remove"))
                {
                    newAnimList.RemoveActionPoint(i);
                }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Add Point"))
            {
                newAnimList.AddActionPoint("new anim");
            }
        EditorGUILayout.EndHorizontal();
        if((serializedAnimList == null && newAnimList != null) || newAnimList != animList)
        {
            serializedAnimList = new SerializedObject(newAnimList);
            animList = newAnimList;
            selectedAnimIndex = 0;
            selectedAnimSprite = 0;
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
            Sprite sprite = spriteAnim.GetSpriteFromIndex(selectedAnimSprite);
            Vector2 maxSpriteSize = Vector2.zero;
            for(int i=0; i<spriteAnim.sprites.Length; i++)
            {
                maxSpriteSize.x = Mathf.Max(maxSpriteSize.x, spriteAnim.sprites[i].rect.width);
                maxSpriteSize.y = Mathf.Max(maxSpriteSize.y, spriteAnim.sprites[i].rect.height);
            }
            
            displaySize = EditorGUILayout.Slider(displaySize, 0.01f, 10);
            selectedAnimSprite = EditorGUILayout.IntSlider(selectedAnimSprite, 0, spriteAnim.sprites.Length - 1);
            Rect rect = GUILayoutUtility.GetRect(100.0f, 100.0f, GUILayout.ExpandHeight(true));
            Vector2[] points = new Vector2[animList.actionPointNames.Length];
            for(int i=0; i<animList.actionPointNames.Length; i++)
            {
                points[i] = spriteAnim.GetSpritePoint(i, selectedAnimSprite);
            }
            DrawSpriteScrollView(rect, sprite, maxSpriteSize, points, newAnimList);
        }
    }

    private bool drag = false;
    private Vector2 lastDragPos;
    
    private void DrawSpriteScrollView(Rect rect, Sprite sprite, Vector2 maxSpriteSize, Vector2[] points, SpriteAnimList newAnimList)
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
        Vector2 center = new Vector2(maxSpriteSize.x / 2, maxSpriteSize.y / 2);
        Rect contentRect = new Rect(0, 0, rect.width + maxSpriteSize.x * displaySize, rect.height + maxSpriteSize.y * displaySize);
        Vector2 contentCenter = contentRect.position + contentRect.size / 2;
        Vector2 scrollCenter = contentCenter - rect.size / 2;
        scrollPos = GUI.BeginScrollView(
            rect,
            scrollCenter + scrollPos,
            contentRect
        ) - scrollCenter;
        DrawOnGUISprite(sprite, contentCenter, displaySize);

        if (e.isMouse && (e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && e.button == 0)
        {
            SpriteAnimConfig spriteAnim = newAnimList.spriteAnims[selectedAnimIndex].spriteAnim;
            spriteAnim.SetActionPoint(selectedPointIndex, selectedAnimSprite, WorldToRelative(sprite, contentCenter, displaySize, e.mousePosition));
            Repaint();
        }

        EditorGUI.DrawRect(new Rect(contentCenter.x - 2, contentCenter.y - 2, 4, 4), Color.green);
            Color[] colors = SpriteAnimEditorSettings.GetOrCreateSettings().colors;
        for(int i=0; i<points.Length; i++)
        {
            Vector2 pointPos = RelativeToWorld(sprite, contentCenter, displaySize, points[i]);
            EditorGUI.DrawRect(new Rect(pointPos.x - 3, pointPos.y - 3, 6, 6), colors[i % colors.Length]);
        }
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

    Vector2 RelativeToWorld(Sprite aSprite, Vector2 position, float scale, Vector2 relativePoint)
    {
        Rect c = aSprite.rect;
        float spriteW = c.width;
        float spriteH = c.height;
        Rect rect = new Rect(position.x - aSprite.pivot.x * scale, position.y - (spriteH - aSprite.pivot.y) * scale, spriteW * scale, spriteH * scale);
        return rect.position + rect.size * relativePoint;
    }

    Vector2 WorldToRelative(Sprite aSprite, Vector2 position, float scale, Vector2 screenPoint)
    {
        Rect c = aSprite.rect;
        float spriteW = c.width;
        float spriteH = c.height;
        Rect rect = new Rect(position.x - aSprite.pivot.x * scale, position.y - (spriteH - aSprite.pivot.y) * scale, spriteW * scale, spriteH * scale);
        return (screenPoint - rect.position) / rect.size;
    }
}
