using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class TurtleBotAgent : Agent
{
    private Vector3 spawnPosition;
    private bool didHitBoundary, didHitObstacle, didHitPerson, didReachGoal;


    void Start()
    {
        spawnPosition = this.transform.position;
        didHitBoundary = false;
        didHitObstacle = false;
        didHitPerson = false;
        didReachGoal = false;
    }


    void Update()
    {
    }


    public override void CollectObservations(VectorSensor sensor)
    {
    }


    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
    }


    public override void OnEpisodeBegin()
    {
        this.transform.position = spawnPosition;
        didHitBoundary = false;
        didHitObstacle = false;
        didHitPerson = false;
        didReachGoal = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        didHitBoundary = other.gameObject.CompareTag("Static Boundary");
        didHitObstacle = other.gameObject.CompareTag("Static Obstacle");
        didHitPerson = other.gameObject.CompareTag("Person");
        didReachGoal = other.gameObject.CompareTag("Final Goal");
    }
}
