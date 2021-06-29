using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NamedSpriteAnim
{
    public string name;
    public SpriteAnimConfig spriteAnim;
}

[System.Serializable]
public struct SpriteAnimRef
{
    public string animName;
    public SpriteAnimList animList;

    public Sprite GetSprite(float time)
    {
        foreach(var anim in animList.spriteAnims)
        {
            if(anim.name == animName)
            {
                return anim.spriteAnim.GetSprite(time);
            }
        }
        return null;
    }
}

[CreateAssetMenu]
public class SpriteAnimList : ScriptableObject
{
    public NamedSpriteAnim[] spriteAnims;
}
