using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour {

    public float minDistance;
    public float maxDistance;
    public float smooth;
    Vector3 dollyDir;
    public Vector3 dollyDirAdj;
    public float dist;


	// Use this for initialization
	void Start () {
        dollyDir = transform.localPosition.normalized;
        dist = transform.localPosition.magnitude;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 desiredCameraPosition = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredCameraPosition, out hit))
        {
            dist = Mathf.Clamp((hit.distance * 0.9f), minDistance, maxDistance);
        }
        else
        {
            dist = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * dist, Time.deltaTime * smooth);
	}
}
