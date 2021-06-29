using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DirectionSpriteAnim
{
    public Direction[] directions;
    public SpriteAnimRef anim;
}

[CreateAssetMenu]
public class CharacterConfig : ScriptableObject
{
    public DirectionSpriteAnim[] anims;

    public SpriteAnimRef GetAnim(Direction direction)
    {
        foreach(DirectionSpriteAnim anim in anims)
        {
            foreach(var animDir in anim.directions)
            {
                if(animDir == direction)
                    return anim.anim;
            }
        }
        return new SpriteAnimRef();
    }
    
}
