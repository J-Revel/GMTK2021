using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUIPanel : MonoBehaviour
{
    public string stat;
    void Start()
    {
        if(GameLauncher.instance == null)
            return;
        if(!GameLauncher.instance.config.isStatActive(stat))
        {
            gameObject.SetActive(false);
        }
    }
}
