using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtLeader : MonoBehaviour {

    public GameObject leader;

	// Use this for initialization
	void Start () {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Leader");
        if (objs.Length > 0) {
            leader = objs[0];
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(leader.transform.position, Vector3.up);
	}
}
