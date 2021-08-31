using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteAnimConfig
{
    public Sprite[] sprites = {};
    public Vector2[] actionPoints = {};
    public float framePerSecond = 24;

    public Sprite GetSpriteFromTime(float time)
    {
        int index = Mathf.FloorToInt(time * framePerSecond);
        return sprites[index % sprites.Length];
    }

    public Sprite GetSpriteFromIndex(int index)
    {
        return sprites[index % sprites.Length];
    }

    public Vector2 GetSpritePoint(int pointIndex, int spriteIndex)
    {
        return actionPoints[pointIndex * sprites.Length + spriteIndex];
    }

    public float GetSpriteIndex(float time)
    {
        return Mathf.FloorToInt(time * framePerSecond);
    }

    public void SetActionPoint(int pointIndex, int spriteIndex, Vector2 point)
    {
        actionPoints[pointIndex * sprites.Length + spriteIndex] = point;
    }

    public void AddActionPoint()
    {
        Vector2[] backup = actionPoints;
        int actionPointCount = actionPoints.Length / sprites.Length;
        actionPoints = new Vector2[(actionPointCount + 1) * sprites.Length];
        for(int i=0; i<backup.Length; i++)
        {
            actionPoints[i] = backup[i];
        }
    }

    public void RemoveActionPoint(int index)
    {
        Vector2[] backup = actionPoints;
        int actionPointCount = actionPoints.Length / sprites.Length;
        actionPoints = new Vector2[(actionPointCount - 1) * sprites.Length];
        for(int i=0; i<sprites.Length * index; i++)
        {
            actionPoints[i] = backup[i];
        }
        for(int i=sprites.Length * (index + 1); i<backup.Length; i++)
        {
            actionPoints[i-sprites.Length] = backup[i];
        }
    }
}
