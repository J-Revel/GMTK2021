using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseElement : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
