using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ControlType
{
    Keyboard,
    Mouse
}

public class Menu : MonoBehaviour
{
    public static ControlType CurrentControlType;

    [SerializeField] private List<MonoBehaviour> ScriptsForPause;

    [SerializeField] private List<MonoBehaviour> ScriptsForRestart;

    [SerializeField] private PlayerMove PlayerMove;
    [SerializeField] private GameObject MenuCanvas;

    [SerializeField] private Button ContinueBtn;
    [SerializeField] private Text ControlCaption;

    private void Start()
    {
        Pause();

        CurrentControlType = ControlType.Keyboard;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    private void Pause()
    {
        for (int i = 0; i < ScriptsForPause.Count; i++)
            ScriptsForPause[i].enabled = false;

        MenuCanvas.SetActive(true);

        Time.timeScale = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        for (int i = 0; i < ScriptsForPause.Count; i++)
            ScriptsForPause[i].enabled = true;

        MenuCanvas.SetActive(false);

        Time.timeScale = 1;
    }

    public void NewGame()
    {
        foreach (var item in ScriptsForRestart)
            item.Invoke("Restart", 0);

        ContinueBtn.interactable = true;

        GameOver.Hide();

        ContinueGame();
    }

    public void ChangeControl()
    {
        if (CurrentControlType == ControlType.Keyboard)
        {
            CurrentControlType = ControlType.Mouse;
            ControlCaption.text = "”правление: клавиатура + мышь";
        }
        else
        {
            CurrentControlType = ControlType.Keyboard;
            ControlCaption.text = "”правление: клавиатура";
        }
    }
}
