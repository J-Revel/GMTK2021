using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup fadeElement;
    public string sceneName;
    private float fadeTime;
    public float fadeDuration = 0.5f;
    private bool transition = false;

    void Start()
    {
        
    }

    public void StartTransition()
    {
        fadeTime = 0;
        transition = true;
    }

    void Update()
    {
        fadeElement.alpha = fadeTime / fadeDuration;
        if(transition)
        {
            fadeTime += Time.deltaTime;
            if(fadeTime > fadeDuration)
            {
                transition = false;
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
        }
    }
}
