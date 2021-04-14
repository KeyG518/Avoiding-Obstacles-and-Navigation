using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class TurtleBotAgent : Agent
{
    private bool didHitBoundary, didHitObstacle, didHitPerson, didReachGoal;
    private Vector3 spawnPosition;
    private UnityInputTeleop unityRosInput;


    void Start()
    {
        GameObject rosInterface = GameObject.Find("RosInterface");
        unityRosInput = rosInterface.GetComponent<UnityInputTeleop>();

        didHitBoundary = false;
        didHitObstacle = false;
        didHitPerson = false;
        didReachGoal = false;
        spawnPosition = this.transform.position;
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
        didHitBoundary = false;
        didHitObstacle = false;
        didHitPerson = false;
        didReachGoal = false;
        this.transform.position = spawnPosition;
    }


    private void OnTriggerEnter(Collider other)
    {
        didHitBoundary = other.gameObject.CompareTag("Static Boundary");
        didHitObstacle = other.gameObject.CompareTag("Static Obstacle");
        didHitPerson = other.gameObject.CompareTag("Person");
        didReachGoal = other.gameObject.CompareTag("Final Goal");
    }
}
