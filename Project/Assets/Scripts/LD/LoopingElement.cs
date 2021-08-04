using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingElement : MonoBehaviour
{
    public LineDirection lineDirection;
    public float targetDistance;
    public GameObject spawnParent;

    public SpriteRenderer spriteRenderer;
    public float spawnDistance;

    private bool isInPrefabEditor
    {
        get
        { 
#if UNITY_EDITOR
            return UnityEditor.SceneManagement.StageUtility.GetCurrentStage().GetType() == typeof(UnityEditor.Experimental.SceneManagement.PrefabStage);
#else
            return false;
#endif
        }
    }

    public Vector3 targetPoint 
    { 
        get 
        { 
            Vector3 direction = transform.right;
            if(lineDirection == LineDirection.Vertical)
                direction = transform.up;
            return transform.position + direction * targetDistance;
        }

        set
        {
            targetDistance = Vector3.Distance(transform.position, value);
        }
    }

    public Vector3 targetDirection
    {
        get
        {
            Vector3 direction = transform.right;
            if(lineDirection == LineDirection.Vertical)
                direction = transform.up;
            return direction;
        }
    }

    void Start()
    {
        UpdateElements();
    }

    public void UpdateElements()
    {
       spriteRenderer.transform.position = (targetPoint + transform.position) / 2;
       switch(lineDirection)
       {
           case LineDirection.Horizontal:
                spriteRenderer.size = new Vector2(targetDistance, spriteRenderer.sprite.rect.height / spriteRenderer.sprite.pixelsPerUnit);
                break;
           case LineDirection.Vertical:
                spriteRenderer.size = new Vector2(spriteRenderer.sprite.rect.width / spriteRenderer.sprite.pixelsPerUnit, targetDistance);
                break;

       }
    }
}
