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
        // GameObject[] obstacles = obstacleManager.GetObstacles();
        // GameObject[] persons = personManager.GetPersons();

        // for (int obstacleIndex = 0; obstacleIndex < obstacles.Length; obstacleIndex++)
        // {
        //     sensor.AddObservation(obstacles[obstacleIndex].transform.localPosition);
        // }

        // for (int personIndex = 0; personIndex < persons.Length; personIndex++)
        // {
        //     sensor.AddObservation(persons[personIndex].transform.localPosition);
        // }

        // sensor.AddObservation(finalGoal.transform.localPosition);
        // sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(Vector3.Distance(this.transform.position, finalGoal.transform.position) / maxGoalDistance);
        sensor.AddObservation(unityRosInput.GetAngularVelocity());
        // sensor.AddObservation(unityRosInput.GetLinearVelocity());
    }


    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions
        float angularVelocity = Mathf.Clamp(actionBuffers.ContinuousActions[0], -1.0f, 1.0f);
        float linearVelocity = 1.0f;

        unityRosInput.MoveAngular(angularVelocity);
        unityRosInput.MoveLinear(linearVelocity);

        // Rewards
        if (didHitBoundary)
        {
            AddReward(-1.0f);
            EndEpisode();
        }
        if (didHitObstacle)
        {
            AddReward(-1.0f);
            EndEpisode();
        }
        if (didHitPerson)
        {
            AddReward(-1.0f);
            EndEpisode();
        }
        if (didReachGoal)
        {
            Debug.Log("Reached Goal!");
            AddReward(1.0f);
            EndEpisode();
        }

        float currentDistance = Vector3.Distance(this.transform.position, finalGoal.transform.position);
        float distanceReward = 0.01f * (maxGoalDistance - currentDistance) / maxGoalDistance;
        AddReward(distanceReward);
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
        this.transform.rotation = Quaternion.Euler(spawnRotation);
    }


    private void OnCollisionEnter(Collision other)
    {
        didHitBoundary = other.collider.gameObject.CompareTag("Static Boundary");
        didHitObstacle = other.collider.gameObject.CompareTag("Static Obstacle");
        didHitPerson = other.collider.gameObject.CompareTag("Person");
        didReachGoal = other.collider.gameObject.CompareTag("Final Goal");
    }
}
