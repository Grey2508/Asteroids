using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private int ScoreForLife = 1000;
    [SerializeField] private AudioSource ExtraLifeSound;

    public static int TotalScore
    {
        get;
        private set;
    }

    static Text ScoreText;
    static int StaticScoreForLife;
    static int PointsToLife;
    static AudioSource StaticExtraLifeSound;

    void Start()
    {
        ScoreText = GetComponent<Text>();
        StaticScoreForLife = ScoreForLife;
        StaticExtraLifeSound = ExtraLifeSound;
    }

    public static void AddScore(int value)
    {
        TotalScore += value;
        PointsToLife += value;

        ScoreText.text = TotalScore.ToString();

        if (PointsToLife >= StaticScoreForLife)
        {
            StaticExtraLifeSound.Play();
            FindObjectOfType<PlayerHealth>().AddHealth(1);
            PointsToLife -= StaticScoreForLife;
        }
    }

    public void Restart()
    {
        TotalScore = 0;

        ScoreText.text = TotalScore.ToString();
    }
}
