using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> ScriptsForPause;

    [SerializeField] private List<MonoBehaviour> ScriptsForRestart;

    [SerializeField] private PlayerMove PlayerMove;
    [SerializeField] private GameObject MenuCanvas;

    [SerializeField] private Button ContinueBtn;

    private void Start()
    {
        Pause();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ContinueBtn.interactable = true;

            Pause();
        }
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
        for(int i=0; i<ScriptsForPause.Count; i++)
            ScriptsForPause[i].enabled = true;

        MenuCanvas.SetActive(false);

        Time.timeScale = 1;
    }

    public void NewGame()
    {
        foreach (var item in ScriptsForRestart)
            item.Invoke("Restart", 0);

        ContinueGame();
    }
}
