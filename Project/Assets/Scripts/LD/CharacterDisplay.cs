using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterDisplay : MonoBehaviour
{
    public new SpriteAnimRef animation;
    public SpriteRenderer renderer;
    public float animTime;
    public LookDirection lookDirection;
    public bool playing = true;
    private float directionAnimTime = 0;
    public float directionChangeDuration = 0.3f;

    public Quaternion initialRotation;


    void Start()
    {
        renderer.sprite = animation.GetSprite(animTime);
        if(Application.isPlaying)
        {
            initialRotation = renderer.transform.localRotation;
            directionAnimTime = (lookDirection == LookDirection.Right) ? directionChangeDuration : 0;
            renderer.transform.localRotation = initialRotation * Quaternion.AngleAxis((directionAnimTime * 180 / directionChangeDuration), Vector3.up);
        }
    }

    void Update()
    {
        if(Application.isPlaying && playing)
        {
            animTime += Time.deltaTime;

            if(directionAnimTime > 0 && lookDirection == LookDirection.Left)
            {
                directionAnimTime -= Time.deltaTime;
                directionAnimTime = Mathf.Max(0, directionAnimTime);
                renderer.transform.localRotation = initialRotation * Quaternion.AngleAxis((directionAnimTime * 180 / directionChangeDuration), Vector3.up);
            }
            
            if(directionAnimTime <= directionChangeDuration && lookDirection == LookDirection.Right)
            {
                directionAnimTime += Time.deltaTime;
                directionAnimTime = Mathf.Min(directionAnimTime, directionChangeDuration);
                renderer.transform.localRotation = initialRotation * Quaternion.AngleAxis((directionAnimTime * 180 / directionChangeDuration), Vector3.up);
            }
        }
        else animTime = 0;

        renderer.sprite = animation.GetSprite(animTime);

        
    }
}
