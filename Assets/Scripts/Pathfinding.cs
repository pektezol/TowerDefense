using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Transform[] waypoints;

    public Vector3 GetWaypointPosition(int waypointIndex)
    {
        return waypoints[waypointIndex].position;
    }

    public int GetNextWaypointIndex(int currentWaypoint)
    {
        int nextWaypoint = currentWaypoint + 1;
        if (nextWaypoint >= waypoints.Length)
        {
            return -1;
        }
        return nextWaypoint;
    }
}