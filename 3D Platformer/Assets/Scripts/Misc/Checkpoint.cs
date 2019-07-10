using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public HealthManager healthManager;

    public Renderer rend;
    public Renderer rend1;

    public Material cpOff;
    public Material cpOn;

    
    // Use this for initialization
	void Start () {
        healthManager = FindObjectOfType<HealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckpointOn()
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint cp in checkpoints)
        {
            cp.CheckpointOff();
        }

        rend.material = cpOn;
        rend1.material = cpOn;
    }

    public void CheckpointOff()
    {
        rend.material = cpOff;
        rend1.material = cpOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            healthManager.SetSpawnPoint(transform.position);
            CheckpointOn();
        }
    }
}
