using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFinal : MonoBehaviour
{
    Text score;

    void Awake()
    {
        score = GetComponent<Text>();
        score.text = "You  scored  " + Score.scoreValue + "  points";
    }
}
