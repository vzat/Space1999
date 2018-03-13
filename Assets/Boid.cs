using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    public float mass = 1;
    public float maximumSpeed = 10.0f;

    public Vector3 force;
    public Vector3 acceleration;
    public Vector3 velocity;
    
    List<SteeringBehaviour> behaviours = new List<SteeringBehaviour>();

    public Vector3 target;

    public Boid leader;
    public Vector3 offset;

    public bool seek = false;
    public bool offsetPursue = false;

    public Vector3 SeekForce(Vector3 target) {
        Vector3 toTarget = target - this.transform.position;
        toTarget.Normalize();

        Vector3 desired = toTarget * maximumSpeed;

        return desired - velocity;
    }

    public Vector3 Arrive(Vector3 target, float slowingDistance = 10.0f) {
        Vector3 toTarget = target - this.transform.position;

        float dist = Vector3.Distance(target, this.transform.position);
        float ramped = maximumSpeed * (dist / slowingDistance);
        float clamped = Mathf.Min(maximumSpeed, ramped);

        Vector3 desired = maximumSpeed * (toTarget / dist);

        return desired - velocity;
    }

    public Vector3 OffsetPursue(Boid target) {
        Vector3 offsetTarget = target.transform.TransformPoint(offset);
        Vector3 toTarget = offsetTarget - this.transform.position;

        // Time to reach target
        float time = toTarget.magnitude / maximumSpeed;

        Vector3 futureTargetPos = offsetTarget + target.velocity * time;

        return Arrive(futureTargetPos);
    }

    Vector3 Calculate() {
        //force = Vector3.zero;

        //foreach (SteeringBehaviour behaviour in behaviours) {
        //    if (behaviour.isActiveAndEnabled) {
        //        force += behaviour.Calculate() * behaviour.weight;
        //    }
        //}

        //return force;

        force = Vector3.zero;

        if (seek) {
            force = SeekForce(target);
        }
        if (offsetPursue) {
            force = OffsetPursue(leader);
        }

        return force;
    }

    // Use this for initialization
    void Start () {
        SteeringBehaviour[] behaviours = GetComponents<SteeringBehaviour>();

        foreach (SteeringBehaviour behaviour in behaviours) {
            this.behaviours.Add(behaviour);
        }
	}
	
	// Update is called once per frame
	void Update () {
        force = Calculate();
        force = Vector3.ClampMagnitude(force, maximumSpeed);

        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;

        // Look in the direction of the velocity if object has a velocity
        if (velocity.magnitude > float.Epsilon) {
            transform.LookAt(transform.position + velocity, Vector3.up);
            velocity *= 0.99f;
        }

        transform.position += velocity * Time.deltaTime;
	}
}
