using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour {

    public float gap = 20;
    public float followers = 2;
    public GameObject prefab;

    public GameObject leaderBoid;
    List<GameObject> followersBoids = new List<GameObject>();

    void Awake () {
        // Instantiate Leader
        leaderBoid = Instantiate(prefab);
        leaderBoid.transform.parent = this.transform;
        leaderBoid.transform.position = this.transform.position;
        leaderBoid.transform.rotation = this.transform.rotation;

        // Instantiate followers
        for (int i = 0; i < followers; i++) {
            // Get local offset for the ship on the left
            Vector3 leftOffset = new Vector3(-gap * (i + 1), 0, -gap * (i + 1));
            leftOffset = Quaternion.Inverse(leaderBoid.transform.rotation) * leftOffset;

            // Get local offset for the ship on the right
            Vector3 rightOffset = new Vector3(gap * (i + 1), 0, -gap * (i + 1));
            rightOffset = Quaternion.Inverse(leaderBoid.transform.rotation) * rightOffset;


            Vector3 worldPosLeftFollower = leaderBoid.transform.TransformPoint(leftOffset);
            Vector3 worldPosRightFollower = leaderBoid.transform.TransformPoint(rightOffset);

            GameObject leftFollower = Instantiate(prefab);
            leftFollower.transform.parent = this.transform;
            leftFollower.transform.position = worldPosLeftFollower;
            //leftFollower.transform.rotation = leaderBoid.transform.rotation;
            leftFollower.transform.rotation = Quaternion.Inverse(leaderBoid.transform.rotation);
            followersBoids.Add(leftFollower);

            GameObject rightFollower = Instantiate(prefab);
            rightFollower.transform.parent = this.transform;
            rightFollower.transform.position = worldPosRightFollower;
            rightFollower.transform.rotation = leaderBoid.transform.rotation;
            followersBoids.Add(rightFollower);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
