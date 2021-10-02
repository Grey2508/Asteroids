using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private int ScoreForLife = 1000;

    static int TotalScore;
    static Text ScoreText;

    void Start()
    {
        ScoreText = GetComponent<Text>();
    }

    public static void AddScore(int value)
    {
        TotalScore += value;

        ScoreText.text = TotalScore.ToString();
        //if(TotalScore%score)
    }
}
