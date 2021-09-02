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

    // Update is called once per frame
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
}
