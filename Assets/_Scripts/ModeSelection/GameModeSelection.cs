using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeSelection : MonoBehaviour
{
    public int gameMode;
    public GameObject startGamePanel;
    public TMP_InputField nameInput;
    public GameObject enterNameInfomation;

    private void Start()
    {
        startGamePanel.SetActive(false);
        enterNameInfomation.SetActive(false);


        if (!PlayerPrefs.HasKey("GameMode"))
        {
            PlayerPrefs.SetInt("GameMode", 0);
        }
        gameMode = PlayerPrefs.GetInt("GameMode");
    }

    public void SingleModeBtn()
    {
        ModeSelection(1);
    }
    public void PvPModeBtn()
    {
        ModeSelection(2);
    }

    void ModeSelection(int mode)
    {
        PlayerPrefs.SetInt("GameMode", mode);
        gameMode = mode;
        gameMode = PlayerPrefs.GetInt("GameMode");
        startGamePanel.SetActive(true);
    }

    public void StartBtn()
    {
        if(nameInput.text == "")
        {
            enterNameInfomation.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", nameInput.text);

            if (gameMode == 1)
            {
                SceneManager.LoadScene("SelectionShip");
            }
            else if (gameMode == 2)
            {
                SceneManager.LoadScene("MatchFound");
            }
        }
    }
    public void CloseStartGamePanelBtn()
    {
        startGamePanel.SetActive(false);
    }
}
