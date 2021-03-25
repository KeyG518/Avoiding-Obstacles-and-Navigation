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
        initializeObstacles();
    }


    void Update()
    {
    }


    private void initializeObstacles()
    {
        GameObject obstacleInstance;
        Vector3 obstaclePosition;
        int randomIndex;

        for (int obstacleIndex = 0; obstacleIndex < obstacleCount; obstacleIndex++)
        {
            randomIndex = Random.Range(0, obstacles.Length);
            obstacleInstance = GameObject.Instantiate(obstacles[randomIndex]);
            obstaclePosition = new Vector3(Random.Range(obstacleMinPos.x, obstacleMaxPos.x), obstacleInstance.transform.position.y, Random.Range(obstacleMinPos.z, obstacleMaxPos.z));
            obstacleInstance.transform.position = obstaclePosition;
        }
    }
}
