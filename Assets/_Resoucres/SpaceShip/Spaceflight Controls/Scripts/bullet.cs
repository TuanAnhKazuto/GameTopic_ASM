using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject explo;


    void OnCollisionEnter(Collision col)
    {

        GameObject.Instantiate(explo, col.contacts[0].point, Quaternion.identity);

        Destroy(gameObject);
    }


}
