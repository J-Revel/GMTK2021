using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteAnimList animList;
    public int animIndex;
    private float time;
    private int frameIndex = 0;

    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        int newFrameIndex = animList.spriteAnims[animIndex].spriteAnim.GetSpriteIndex(time);
        if(newFrameIndex != frameIndex)
        {
            spriteRenderer.sprite = animList.spriteAnims[animIndex].spriteAnim.GetSpriteFromIndex(newFrameIndex);
            frameIndex = newFrameIndex;
        }
    }

    public void SelectAnim(string animName)
    {
        for(int i=0; i<animList.spriteAnims.Length; i++)
        {
            if(animList.spriteAnims[i].name == animName)
                animIndex = i;
        }
    }

    public Vector3 GetPointPosition(string pointName)
    {
        int pointIndex = 0;
        for(int i=0; i<animList.actionPointNames.Length; i++)
        {
            if(animList.actionPointNames[i] == pointName)
                pointIndex = i;
        }
        Sprite sprite = animList.spriteAnims[animIndex].spriteAnim.GetSpriteFromIndex(frameIndex);
        Vector2 posInSprite = animList.spriteAnims[animIndex].spriteAnim.GetSpritePoint(pointIndex, frameIndex);
        Vector2 textureSize = sprite.textureRect.size / sprite.pixelsPerUnit;
        return transform.position + transform.right * posInSprite.x * textureSize.x * (spriteRenderer.flipX ? -1 : 1) - transform.up * posInSprite.y * textureSize.y;
    }
}
