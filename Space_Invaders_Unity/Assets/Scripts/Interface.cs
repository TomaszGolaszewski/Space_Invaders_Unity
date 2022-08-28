using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Interface : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public static int currentScore = 0; // { get; private set; } = 0;

    // Update is called once per frame
    void Update()
    {
        // Print current score
        Score.text = currentScore.ToString();
    }
}

