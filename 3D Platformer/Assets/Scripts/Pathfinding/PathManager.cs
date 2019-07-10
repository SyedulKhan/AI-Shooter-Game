using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*  This script has been adapted from the reference below:
 
    Lague, S (2015). A* Pathfinding (E03: algorithm implementation). Youtube. Available at:
    https://www.youtube.com/watch?v=mZfyt03LDH4&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=3 [Accessed 16 March 2019]
*/

public class PathManager : MonoBehaviour
{
    Queue<PathRequest> queue = new Queue<PathRequest>();
    PathRequest pathRequest;
    Pathfinder pathfinder;
    static PathManager instance;
    bool isCreatingPath;

    private void Awake()
    {
        instance = this;
        pathfinder = GetComponent<Pathfinder>();
    }

    public static void RequestPath(Vector3 start, Vector3 destination, Action<Vector3[], bool> callback)
    {
        PathRequest request = new PathRequest(start, destination, callback);
        instance.queue.Enqueue(request);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isCreatingPath && queue.Count > 0)
        {
            pathRequest = queue.Dequeue();
            isCreatingPath = true;
            pathfinder.BeginFindingPath(pathRequest.start, pathRequest.destination);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        pathRequest.callback(path, success);
        isCreatingPath = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 start, destination;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 start, Vector3 destination, Action<Vector3[], bool> callback)
        {
            this.start = start;
            this.destination = destination;
            this.callback = callback;
        }
    }
}
