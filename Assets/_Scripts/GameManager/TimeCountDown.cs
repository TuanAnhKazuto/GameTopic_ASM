using UnityEngine;
using UnityEngine.UI;

public class TimeCountDown : MonoBehaviour
{
    public float timeCountDown = 20f;
    public Text timerText;

    private void Start()
    {
        timerText.text = timeCountDown.ToString();
    }

    private void Update()
    {
        TimeDown();
    }

    private void TimeDown()
    {
        timeCountDown -= Time.deltaTime;
        Debug.Log("Time: " + timeCountDown);

        if (Mathf.Round(timeCountDown) <= 9)
        {
            timerText.text = "0" + Mathf.Round(timeCountDown).ToString();
        }
        else
        {
            timerText.text = Mathf.Round(timeCountDown).ToString();
        }
    }
}
