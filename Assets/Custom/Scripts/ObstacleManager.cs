using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstacles;
    public Vector3 obstacleMinPos;
    public Vector3 obstacleMaxPos;
    public int obstacleCount = 10;


    void Start()
    {
        InitializeObstacles();
    }


    void Update()
    {
    }


    private float GetFlatDistance(Vector3 vectorA, Vector3 vectorB)
    {
        vectorA.y = 0;
        vectorB.y = 0;

        return Vector3.Distance(vectorA, vectorB);
    }


    private void InitializeObstacles()
    {
        GameObject obstacleInstance;
        Vector3 obstaclePosition;
        int randomIndex;
        float positionX, positionZ;

        for (int obstacleIndex = 0; obstacleIndex < obstacleCount; obstacleIndex++)
        {
            randomIndex = Random.Range(0, obstacles.Length);
            obstacleInstance = GameObject.Instantiate(obstacles[randomIndex]);

            do {
                positionX = Random.Range(obstacleMinPos.x, obstacleMaxPos.x);
                positionZ = Random.Range(obstacleMinPos.z, obstacleMaxPos.z);
                obstaclePosition = new Vector3(positionX, obstacleInstance.transform.position.y, positionZ);
            }
            while (!IsPositionValid(obstaclePosition, 2.0f));

            obstacleInstance.transform.position = obstaclePosition;
        }
    }


    private bool IsPositionValid(Vector3 position, float distanceThreshold)
    {
        GameObject finalGoal = GameObject.FindWithTag("Final Goal");
        GameObject player = GameObject.FindWithTag("Player");

        bool isValid =  GetFlatDistance(position, finalGoal.transform.position) > distanceThreshold;
        isValid = isValid && GetFlatDistance(position, player.transform.position) > distanceThreshold;

        return isValid;
    }
}
