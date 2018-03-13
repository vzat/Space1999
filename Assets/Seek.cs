using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour {

    public Vector3 target;

    public override Vector3 Calculate() {
        return boid.SeekForce(target);
    }

    public void Update() {

    }
}
