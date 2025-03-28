using Fusion;
using UnityEngine;

public class PSDestroy : NetworkBehaviour
{

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }

}
