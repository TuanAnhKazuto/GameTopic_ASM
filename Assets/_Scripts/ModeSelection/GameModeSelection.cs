using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeSelection : MonoBehaviour
{
    public int gameMode;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("GameMode"))
        {
            PlayerPrefs.SetInt("GameMode", 0);
        }
        gameMode = PlayerPrefs.GetInt("GameMode");
    }

    public void SingleModeBnt()
    {
        PlayerPrefs.SetInt("GameMode", 1);
        gameMode = PlayerPrefs.GetInt("GameMode");
        SceneManager.LoadScene("Scene01");
    }

    public void MultiModeBnt()
    {
        PlayerPrefs.SetInt("GameMode", 2);
        gameMode = PlayerPrefs.GetInt("GameMode");
        SceneManager.LoadScene("Scene01");
    }
}
