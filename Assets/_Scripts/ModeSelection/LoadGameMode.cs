using Fusion;
using UnityEngine;

public class LoadGameMode : MonoBehaviour
{
    int gameMode;
    public FusionBootstrap fusionBootstrap;

    private void Start()
    {
        Time.timeScale = 1;
        gameMode = PlayerPrefs.GetInt("GameMode");
        if (gameMode == 1)
        {
            // Load Single Player Mode
            Debug.Log("Single Player Mode");
            SingleMode();
        }
        else if (gameMode == 2)
        {
            // Load Multiplayer Mode
            Debug.Log("Multiplayer Mode");
            MultiMode();
        }
    }

    public void SingleMode()
    {
        fusionBootstrap.StartSinglePlayer();
    }

    public void MultiMode()
    {
        fusionBootstrap.StartSharedClient();
    }
}
