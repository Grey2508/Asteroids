using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> ScriptsForOff;
    [SerializeField] private PlayerMove PlayerMove;
    [SerializeField] private GameObject MenuCanvas;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            for(int i=0; i<ScriptsForOff.Count; i++)
                ScriptsForOff[i].enabled = false;

            MenuCanvas.SetActive(true);
            
            Time.timeScale = 0;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        for(int i=0; i<ScriptsForOff.Count; i++)
            ScriptsForOff[i].enabled = true;

        MenuCanvas.SetActive(false);

        Time.timeScale = 1;
    }
}
