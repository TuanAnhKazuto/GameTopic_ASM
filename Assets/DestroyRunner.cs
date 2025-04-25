using Fusion;
using UnityEngine;

public class DestroyRunner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var runner = FindAnyObjectByType<NetworkRunner>();

        Destroy(runner.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
