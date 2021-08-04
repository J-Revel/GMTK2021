
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LoopingElement)), CanEditMultipleObjects]
public class LoopingElementEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        LoopingElement example = (LoopingElement)target;

        EditorGUI.BeginChangeCheck();
        Handles.color = Handles.xAxisColor;
        Vector3 targetPos = example.targetPoint;
        float size = HandleUtility.GetHandleSize(targetPos) * 0.2f;

        Vector3 direction = example.targetDirection;
        Vector3 newPosition = Handles.Slider(targetPos, direction, size, Handles.CubeHandleCap, 0.1f);
        // Vector3 newPosition = Handles.PositionHandle(example.transform.TransformPoint(example.targetPos), example.transform.rotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(example, "Change Look At Target Position");
            example.targetPoint = newPosition;
            example.UpdateElements();
        }
        
    }
}