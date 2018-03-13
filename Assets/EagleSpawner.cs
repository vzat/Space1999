using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour {

    public float gap = 20;
    public float followers = 2;
    public GameObject prefab;

    GameObject leaderObj;
    List<GameObject> followersObjs = new List<GameObject>();

    void Awake () {
        // Instantiate Leader
        leaderObj = Instantiate(prefab);
        leaderObj.transform.parent = this.transform;
        leaderObj.transform.position = this.transform.position;
        leaderObj.transform.rotation = this.transform.rotation;

        //Seek leaderSeek = leaderObj.GetComponent<Seek>();
        //Vector3 localTarget = new Vector3(0, 0, 1000);
        //leaderSeek.target = leaderObj.transform.TransformPoint(localTarget);

        Boid leaderBoid = leaderObj.GetComponent<Boid>();
        Vector3 localTarget = new Vector3(0, 0, 1000);
        leaderBoid.seek = true;
        leaderBoid.target = leaderObj.transform.TransformPoint(localTarget);

        // Instantiate followers
        for (int i = 0; i < followers; i++) {
            // Get local offset for the ship on the left
            Vector3 leftOffset = new Vector3(-gap * (i + 1), 0, -gap * (i + 1));
            //leftOffset = Quaternion.Inverse(leaderObj.transform.rotation) * leftOffset;

            // Get local offset for the ship on the right
            Vector3 rightOffset = new Vector3(gap * (i + 1), 0, -gap * (i + 1));
            //rightOffset = Quaternion.Inverse(leaderObj.transform.rotation) * rightOffset;

            // Convert offsets to world space
            Vector3 worldPosLeftFollower = leaderObj.transform.TransformPoint(leftOffset);
            Vector3 worldPosRightFollower = leaderObj.transform.TransformPoint(rightOffset);

            // Instantiate follower on the left of the leader
            GameObject leftFollower = Instantiate(prefab);
            
            // Set position and rotation
            leftFollower.transform.parent = this.transform;
            leftFollower.transform.position = worldPosLeftFollower;
            leftFollower.transform.rotation = leaderObj.transform.rotation;

            // Set Offset Pursue
            Boid leftFollowerBoid = leftFollower.GetComponent<Boid>();
            leftFollowerBoid.offsetPursue = true;
            leftFollowerBoid.offset = Quaternion.Inverse(leaderBoid.transform.rotation) * leftOffset;
            leftFollowerBoid.leader = leaderBoid;
            followersObjs.Add(leftFollower);


            // Instantiate follower on the right of the leader
            GameObject rightFollower = Instantiate(prefab);
            
            // Set Position and Rotation
            rightFollower.transform.parent = this.transform;
            rightFollower.transform.position = worldPosRightFollower;
            rightFollower.transform.rotation = leaderObj.transform.rotation;

            // Set Offset Pursue
            Boid rightFollowerBoid = rightFollower.GetComponent<Boid>();
            rightFollowerBoid.offsetPursue = true;
            rightFollowerBoid.offset = Quaternion.Inverse(leaderBoid.transform.rotation) * rightOffset;
            rightFollowerBoid.leader = leaderBoid;
            followersObjs.Add(rightFollower);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
