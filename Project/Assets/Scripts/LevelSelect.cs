using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public void SelectLevel(GameConfig config)
    {
        GameLauncher.instance.config = config;

    }
}
