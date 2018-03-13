using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtLeader : MonoBehaviour {

    public GameObject leader;

    public Vector3 target;
    public Vector3 newTarget;

	// Use this for initialization
	void Start () {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Leader");
        if (objs.Length > 0) {
            leader = objs[0];
        }
        target = leader.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // Look at leader
        newTarget = leader.transform.position;
        target = Vector3.Lerp(target, newTarget, Time.deltaTime);
        transform.LookAt(target, Vector3.up);
	}
}
