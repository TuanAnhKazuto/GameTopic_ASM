using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterSelection : MonoBehaviour
{
    public GameObject[] character;
    public int selectedCharacter = 0;

    public TimeCountDown timeCountDown;

    public void NextCharacter()
    {
        character[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % character.Length;
        character[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        character[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += character.Length;
        }
        character[selectedCharacter].SetActive(true);
    }

    private void Update()
    {
        if(timeCountDown.countDown <= 0)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        if (PlayerPrefs.GetInt("GameMode") == 1)
        {
            SceneManager.LoadScene("SinglePlay");
        }
        else if (PlayerPrefs.GetInt("GameMode") == 2)
        {
            SceneManager.LoadScene("MultiPlay");
        }
    }
}
