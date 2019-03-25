using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_WaypointMovement : MonoBehaviour {

    public float speed = 10f;

    public Transform target;
    public int wavepointIndex = 0;
    public Waypoints waypoints;
    public bool stopMovment;

    NavMeshAgent _navAgent;

    void Start()
    {
        //waypoints = GetComponent<Waypoints>();
        target = waypoints.points[0];

        _navAgent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        if(!stopMovment)
        {
            //Vector3 dir = target.position - transform.position;
            //transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            _navAgent.SetDestination(target.transform.position);

            if (Vector3.Distance(transform.position, target.position) <= 1)
            {
                GetNextWaypoint();
            }

            if (wavepointIndex >= waypoints.points.Length - 1)
            {
                wavepointIndex = -1;
            }
        }
       
    }

    void GetNextWaypoint()
    {
        wavepointIndex++; //move in order
        //wavepointIndex = Random.Range(0, Waypoints.points.Length - 1);//random movement between waypoints

        target = waypoints.points[wavepointIndex];
    }
}
