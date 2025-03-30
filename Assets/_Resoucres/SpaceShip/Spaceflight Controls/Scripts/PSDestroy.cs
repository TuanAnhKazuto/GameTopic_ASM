using Fusion;
using System.Collections;
using UnityEngine;

public class PSDestroy : NetworkBehaviour
{
    public NetworkRunner runner;
    // Use this for initialization
    void Start()
    {
        //Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
        StartCoroutine(Destroy(Object, GetComponent<ParticleSystem>().main.duration));
    }

    IEnumerator Destroy(NetworkObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null && obj.IsValid)
        {
            Runner.Despawn(obj);
        }
    }

}
