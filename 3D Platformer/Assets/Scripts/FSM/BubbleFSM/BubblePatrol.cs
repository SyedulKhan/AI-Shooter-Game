using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BubblePatrol : BubbleFSM
{
    //Private variables for base behaviour
    Waypoint current;
    Waypoint previous;

    bool isTravelling;
    bool isWaiting;
    float timerToWait;
    int pointsVisited;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemyStateText.text = "PATROL";

        if (agent != null)
        {
            if (current == null)
            {
                GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                if (allWaypoints.Length > 0)
                {
                    while (current == null)
                    {
                        int random = Random.Range(0, allWaypoints.Length);
                        Waypoint startingWaypoint = allWaypoints[random].GetComponent<Waypoint>();

                        //found waypoint
                        if (startingWaypoint != null)
                        {
                            current = startingWaypoint;
                        }
                    }
                }
                else
                {
                    Debug.Log("Failed to find any waypoints for use in the scene");
                }
            }
            SetDestination();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Check if we are close to destination
        if (isTravelling && agent.remainingDistance <= 1.0f)
        {
            isTravelling = false;
            pointsVisited++;
            SetDestination();
        }
    }

    private void SetDestination()
    {
        if (pointsVisited > 0)
        {
            Waypoint nextWaypoint = current.NextWaypoint(previous);
            previous = current;
            current = nextWaypoint;
        }

        Vector3 targetVector = current.transform.position;
        agent.SetDestination(targetVector);
        isTravelling = true;
    }
}
