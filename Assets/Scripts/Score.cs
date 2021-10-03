using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private int ScoreForLife = 1000;
    [SerializeField] private PlayerHealth PlayerHealth;

    static int TotalScore;
    static Text ScoreText;
    static int StaticScoreForLife;
    static PlayerHealth StaticPlayerHealth;

    void Start()
    {
        ScoreText = GetComponent<Text>();
        StaticScoreForLife = ScoreForLife;
        StaticPlayerHealth = PlayerHealth;
    }

    public static void AddScore(int value)
    {
        TotalScore += value;

        ScoreText.text = TotalScore.ToString();

        if (TotalScore % StaticScoreForLife == 0)
            StaticPlayerHealth.AddHealth(1);
    }

    public static void Reset()
    {
        TotalScore = 0;

        ScoreText.text = TotalScore.ToString();
    }
}
