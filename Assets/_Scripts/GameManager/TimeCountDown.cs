using UnityEngine;
using UnityEngine.UI;

public class TimeCountDown : MonoBehaviour
{
    public float countDown = 20f;
    public Text timerText;

    private void Start()
    {
        timerText.text = countDown.ToString();
    }

    private void Update()
    {
        TimeDown();
    }

    private void TimeDown()
    {
        countDown -= Time.deltaTime;

        if (Mathf.Round(countDown) <= 9)
        {
            timerText.text = "0" + Mathf.Round(countDown).ToString();
        }
        else
        {
            timerText.text = Mathf.Round(countDown).ToString();
        }
    }
}
