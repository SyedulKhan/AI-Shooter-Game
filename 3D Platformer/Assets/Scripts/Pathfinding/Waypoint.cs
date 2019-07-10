using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField]
    protected float radiusOfConnectivity = 30f;

    List<Waypoint> connections;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        connections = new List<Waypoint>();

        for (int i = 0; i < waypoints.Length; i++)
        {
            Waypoint nextWaypoint = waypoints[i].GetComponent<Waypoint>();

            if (nextWaypoint != null)
            {
                if (Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= radiusOfConnectivity &&
                    nextWaypoint != this)
                {
                    connections.Add(nextWaypoint);
                }
            }
        }
    }


    public Waypoint NextWaypoint(Waypoint previousWaypoint)
    {
        if (connections.Count == 0)
        {
            Debug.LogError("Insufficient waypoint count");
            return null;
        }
        else if (connections.Count == 1 && connections.Contains(previousWaypoint))
        {
            return previousWaypoint;
        }
        else
        {
            Waypoint nextWaypoint;
            int nextIndex = 0;

            do
            {
                nextIndex = UnityEngine.Random.Range(0, connections.Count);
                nextWaypoint = connections[nextIndex];
            } while (nextWaypoint == previousWaypoint);

            return nextWaypoint;
        }
    }
}
