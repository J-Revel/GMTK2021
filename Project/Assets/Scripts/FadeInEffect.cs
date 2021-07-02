using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInEffect : MonoBehaviour
{
    public float fadeDuration = 1;
    private float time = 0;
    private CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        canvasGroup.alpha = 1 - time / fadeDuration;
        if(time >= fadeDuration)
            Destroy(this);
    }
}
