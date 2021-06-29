using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterDisplay : MonoBehaviour
{
    public CharacterConfig characterConfig;
    public SpriteRenderer[] renderers;
    public float animTime;
    public Direction lookDirection;


    void Start()
    {
        foreach(SpriteRenderer renderer in renderers)
        {
            renderer.sprite = characterConfig.GetAnim(lookDirection).GetSprite(animTime);
        }
    }

    void Update()
    {
        if(Application.isPlaying)
        {
            animTime += Time.deltaTime;
        }
        else animTime = 0;
        foreach(SpriteRenderer renderer in renderers)
        {
            renderer.sprite = characterConfig.GetAnim(lookDirection).GetSprite(animTime);
        }
    }
}
