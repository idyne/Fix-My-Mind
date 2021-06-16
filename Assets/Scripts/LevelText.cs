using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    private Text levelText;

    private void Start()
    {
        levelText.text = "DAY " + Singleton.GM.level;
    }
}
