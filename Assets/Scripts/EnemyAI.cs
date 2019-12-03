using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{

    float shootTimer = 7f;
    NavMeshAgent agent;
    public Transform[] wayPoints;
    Vector3[] wayPointsPosition = new Vector3[3];
    int cWayPointIndex = 0;
    public Transform rayOrigin;
    Transform players;
    Animator fsm; 
    
    

    void Start()
    {
        fsm = GetComponent<Animator>();
        players = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPointsPosition[i] = wayPoints[i].position;
        }
        agent.SetDestination(wayPointsPosition[cWayPointIndex]);

        StartCoroutine("CheckPlayer");
    }


    IEnumerator CheckPlayer()
    {
        CheckVisibility();
        CheckDistance();
        CheckDistanceFromCurrentWaypoint();
        yield return new WaitForSeconds(0.1f);
        yield return CheckPlayer();
    }



    private void CheckDistance()
    {
        float dis = Vector3.Distance(players.position, rayOrigin.position);
        fsm.SetFloat("distance", dis);
    }
    private void CheckDistanceFromCurrentWaypoint()
    {
        float dis = Vector3.Distance(wayPointsPosition[cWayPointIndex], rayOrigin.position);
        fsm.SetFloat("distanceFromCurrentWaypoint", dis);
    }
    private void CheckVisibility()
    {
        float max_dis = 20;
        Vector3 dir = (players.position - rayOrigin.position).normalized;
        Debug.DrawRay(rayOrigin.position, dir * max_dis, Color.red);

        if (Physics.Raycast(rayOrigin.position, dir, out RaycastHit info_enemy, max_dis))
        {
            if (info_enemy.transform.tag == "Player")
                fsm.SetBool("isVisible", true);

            else
                fsm.SetBool("isVisible", false);
        }
        else
            fsm.SetBool("isVisible", false);
    }
    public void SetLoookRotation()
    {
        Vector3 direction = (players.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot,0.2f);
    }

    public void Shoot()
    {
        GetComponent<ShootBehavior>().Shoot(shootTimer);
    }

    public void Chase()
    {
        agent.SetDestination(players.position);
    }

    public void Patrol()
    {
    }


    public void SetNewWayPoint()
    {
        switch (cWayPointIndex)
        {
            case 0:
                cWayPointIndex = 1;
                break;
            case 1:
                cWayPointIndex = 2;
                break;
            case 2:
                cWayPointIndex = 0;
                break;
        }
        agent.SetDestination(wayPointsPosition[cWayPointIndex]);
    }
}
