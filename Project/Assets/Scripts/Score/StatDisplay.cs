using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDisplay : MonoBehaviour
{
    public Stat stat;
    public int multiplier;
    public string prefix;
    private TMPro.TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = prefix + (ScoreService.instance.stats[(int)stat] * multiplier);
    }
}
