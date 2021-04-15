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
    private ObstacleManager obstacleManager;
    private PersonManager personManager;
    private UnityInputTeleop unityRosInput;
    private Vector3 spawnPosition;


    void Start()
    {
        didHitBoundary = false;
        didHitObstacle = false;
        didHitPerson = false;
        didReachGoal = false;

        obstacleManager = obstacleManagerObject.GetComponent<ObstacleManager>();
        personManager = personManagerObject.GetComponent<PersonManager>();
        unityRosInput = rosInterface.GetComponent<UnityInputTeleop>();

        spawnPosition = this.transform.position;
    }


    void Update()
    {
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        GameObject[] obstacles = obstacleManager.GetObstacles();
        GameObject[] persons = personManager.GetPersons();

        foreach (GameObject obstacle in obstacles)
        {
            sensor.AddObservation(obstacle.transform.localPosition);
        }

        foreach (GameObject person in persons)
        {
            sensor.AddObservation(person.transform.localPosition);
        }

        sensor.AddObservation(finalGoal.transform.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(this.transform.localRotation.eulerAngles);
    }


    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions
        float angular = Mathf.Clamp(actionBuffers.ContinuousActions[0], -1.0f, 1.0f);
        float linear = Mathf.Clamp(actionBuffers.ContinuousActions[1], -1.0f, 1.0f);

        unityRosInput.MoveAngular(angular);
        unityRosInput.MoveLinear(linear);

        // Rewards
        if (didHitBoundary)
        {
            AddReward(-0.1f);
        }
        if (didHitObstacle)
        {
            AddReward(-0.2f);
        }
        if (didHitPerson)
        {
            AddReward(-1.0f);
            EndEpisode();
        }
        if (didReachGoal)
        {
            AddReward(1.0f);
            EndEpisode();
        }

        AddReward(-0.01f);
    }


    public override void OnEpisodeBegin()
    {
        didHitBoundary = false;
        didHitObstacle = false;
        didHitPerson = false;
        didReachGoal = false;

        obstacleManager.ResetObstacles();
        personManager.ResetPersons();

        unityRosInput.EnableUserControl(false);
        unityRosInput.MoveAngular(0.0f);
        unityRosInput.MoveLinear(0.0f);

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
