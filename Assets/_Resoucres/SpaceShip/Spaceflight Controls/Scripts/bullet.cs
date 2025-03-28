using UnityEngine;
using Fusion;
using static Unity.Collections.Unicode;
using System.Collections;
using Unity.VisualScripting;

public class Bullet : NetworkBehaviour
{

    public GameObject explo;

    void OnCollisionEnter(Collision col)
    {

        //GameObject.Instantiate(explo, col.contacts[0].point, Quaternion.identity);
        Runner.Spawn(explo, col.contacts[0].point, Quaternion.identity);

        //Destroy(gameObject);
        Runner.Despawn(Object);
    }

    public override void FixedUpdateNetwork()
    {
        StartCoroutine(AutoDespawn(Object, 3f));
    }

    IEnumerator AutoDespawn(NetworkObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null && obj.IsValid)
        {
            Runner.Despawn(obj);
        }
    }

}
