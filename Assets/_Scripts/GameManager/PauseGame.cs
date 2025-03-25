using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private void Update()
    {
         if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }
}
