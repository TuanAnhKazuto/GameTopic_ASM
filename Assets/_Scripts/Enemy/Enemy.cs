using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public float detectionRadius = 100f; // ban kin phat hien player
    public float roamingRadius = 100f; // ban kinh di chuyen cua enemy
    public float roamingDelay = 1.5f; // thoi gian cho cac lan chuyen dong
    private float roamingTimer;

    public GameObject[] targets;

    private void Update()
    {
        roamingTimer += Time.deltaTime;

        // tim kiem player co trong ban kinh 
        targets = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (var t in targets)
        {
            float distance = Vector3.Distance(t.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestTarget = t;
            }
        }

        // neu phat hien nguo cho enemy se duoi theo 
        if (closestTarget != null && minDistance <= detectionRadius)
        {
            agent.SetDestination(closestTarget.transform.position);
        }
        else
        {
           // neu khong phat hien enemy se di chuyen tu do
            if (roamingTimer >= roamingDelay)
            {
                MoveToRandomPosition();
                roamingTimer = 0f; // Reset timer
            }
        }
    }

    private void MoveToRandomPosition()
    {
        // tao 1 vi tri ngau nhien trong ban kinh
        Vector3 randomDirection = Random.insideUnitSphere * roamingRadius;
        randomDirection += transform.position;

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randomDirection, out navHit, roamingRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(navHit.position);
        }
    }
}
