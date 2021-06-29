using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteAnimConfig
{
    public Sprite[] sprites;
    public float framePerSecond = 24;

    public Sprite GetSprite(float time)
    {
        int index = Mathf.FloorToInt(time * framePerSecond);
        return sprites[index % sprites.Length];
    }
}
