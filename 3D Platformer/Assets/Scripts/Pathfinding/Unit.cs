using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  This script has been adapted from the reference below:
 
    Lague, S (2015). A* Pathfinding (E05: units). Youtube. Available at:
    https://www.youtube.com/watch?v=dn1XRIaROM4&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=5 [Accessed 20 March 2019]
*/


public class Unit : MonoBehaviour
{
    public Transform target;
    Vector3[] path;
    float moveSpeed = 20;
    int index;

    private void Start()
    {
        PathManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool foundPath)
    {
        if (foundPath)
        {
            path = newPath;
            StopCoroutine("TraversePath");
            StartCoroutine("TraversePath");
        }
    }

    IEnumerator TraversePath()
    {
        Vector3 currentPos = path[0];

        while (true)
        {
            if (transform.position == currentPos)
            {
                index++;
                if (index >= path.Length)
                {
                    yield break;
                }
                currentPos = path[index];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = index; i < path.Length; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == index)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
