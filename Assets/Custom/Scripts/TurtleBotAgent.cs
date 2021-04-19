using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class TurtleBotAgent : Agent
{
    public GameObject finalGoal, obstacleManagerObject, personManagerObject, rosInterface;

    private bool didHitBoundary, didHitObstacle, didHitPerson, didReachGoal;
    private float maxGoalDistance;
    private ObstacleManager obstacleManager;
    private PersonManager personManager;
    private UnityInputTeleop unityRosInput;
    private Vector3 spawnPosition, spawnRotation;


    void Start()
    {
        didHitBoundary = false;
        didHitObstacle = false;
        didHitPerson = false;
        didReachGoal = false;
        maxGoalDistance = Vector3.Distance(this.transform.position, finalGoal.transform.position);

        obstacleManager = obstacleManagerObject.GetComponent<ObstacleManager>();
        personManager = personManagerObject.GetComponent<PersonManager>();
        unityRosInput = rosInterface.GetComponent<UnityInputTeleop>();

        spawnPosition = this.transform.position;
        spawnRotation = this.transform.rotation.eulerAngles;
    }


    void Update()
    {
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(finalGoal.transform.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(this.transform.InverseTransformPoint(finalGoal.transform.position));
        sensor.AddObservation(Vector3.Distance(this.transform.position, finalGoal.transform.position));
        sensor.AddObservation(unityRosInput.GetAngularVelocity());
        sensor.AddObservation(unityRosInput.GetLinearVelocity());
    }


    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions
        float angularVelocity = Mathf.Clamp(actionBuffers.ContinuousActions[0], -1.0f, 1.0f);
        float linearVelocity = Mathf.Clamp(actionBuffers.ContinuousActions[1], 0.5f, 1.0f);

        unityRosInput.MoveAngular(angularVelocity);
        unityRosInput.MoveLinear(linearVelocity);

        // Rewards
        if (didHitBoundary)
        {
            // Debug.Log("Hit Boundary!");
            AddReward(-1.0f);
            EndEpisode();
        }
        else if (didHitObstacle)
        {
            // Debug.Log("Hit Obstacle!");
            AddReward(-1.0f);
            EndEpisode();
        }
        else if (didHitPerson)
        {
            // Debug.Log("Hit Person!");
            AddReward(-1.0f);
            EndEpisode();
        }
        else if (didReachGoal)
        {
            // Debug.Log("Reached Goal!");
            AddReward(1.0f);
            ResetFinalGoal();
        }
        else
        {
            float currentDistance = Vector3.Distance(this.transform.position, finalGoal.transform.position);
            float distanceReward = 0.01f * (maxGoalDistance - currentDistance) / maxGoalDistance;
            AddReward(distanceReward);
        }
    }


    public override void OnEpisodeBegin()
    {
        didHitBoundary = false;
        didHitObstacle = false;
        didHitPerson = false;

        unityRosInput.EnableUserControl(false);
        unityRosInput.MoveAngular(0.0f);
        unityRosInput.MoveLinear(0.0f);

        this.transform.position = spawnPosition;
        this.transform.rotation = Quaternion.Euler(spawnRotation);
        
        ResetFinalGoal();
        obstacleManager.ResetObstacles();
        personManager.ResetPersons();
    }


    private void OnTriggerEnter(Collider other)
    {
        didHitBoundary = other.gameObject.CompareTag("Static Boundary");
        didHitObstacle = other.gameObject.CompareTag("Static Obstacle");
        didHitPerson = other.gameObject.CompareTag("Person");
        didReachGoal = other.gameObject.CompareTag("Final Goal");
    }


    private void ResetFinalGoal()
    {
        finalGoal.GetComponent<FinalGoalManager>().InitializeFinalGoal();
        maxGoalDistance = Vector3.Distance(this.transform.position, finalGoal.transform.position);
        didReachGoal = false;
    }
}
