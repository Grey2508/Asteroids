using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private int ScoreForLife = 1000;

    public static int TotalScore
    {
        get;
        private set;
    }

    static Text ScoreText;
    static int StaticScoreForLife;

    void Start()
    {
        ScoreText = GetComponent<Text>();
        StaticScoreForLife = ScoreForLife;
    }

    public static void AddScore(int value)
    {
        TotalScore += value;

        ScoreText.text = TotalScore.ToString();

        if (TotalScore % StaticScoreForLife == 0)
            FindObjectOfType<PlayerHealth>().AddHealth(1);
    }

    public void Restart()
    {
        TotalScore = 0;

        ScoreText.text = TotalScore.ToString();
    }
}
