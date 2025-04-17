using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] character;
    public int selectedCharacter = 0;

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

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        SceneManager.LoadScene("SinglePlay");
    }
}
