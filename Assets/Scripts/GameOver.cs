using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text ScoreText;
    [SerializeField] private GameObject GameOverScreen;

    private static Text StaticScoreText;
    private static GameObject StaticGameOverScreen;

    private void Start()
    {
        StaticScoreText = ScoreText;
        StaticGameOverScreen = GameOverScreen;
    }

    public static void Show()
    {
        StaticGameOverScreen.SetActive(true);
        StaticScoreText.text = Score.TotalScore.ToString();
    }

    public static void Hide()
    {
        StaticGameOverScreen.SetActive(false);
    }
}
