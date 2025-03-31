using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float move = 15f;
    [SerializeField] float rotationalDamp = .5f;

    void Update()
    {
        Turn();
        Move();
    }

    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }

    void Move()
    {
        transform.position += transform.forward * Time.deltaTime;
    }
}