using Fusion;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    public static RunnerManager Instance { get; private set; }
    public NetworkRunner Runner;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
