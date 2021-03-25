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
        float positionX, positionZ;
        float safeRegion = 3;

        for (int obstacleIndex = 0; obstacleIndex < obstacleCount; obstacleIndex++)
        {
            randomIndex = Random.Range(0, obstacles.Length);
            obstacleInstance = GameObject.Instantiate(obstacles[randomIndex]);
            do {
		positionX = Random.Range(obstacleMinPos.x, obstacleMaxPos.x);
		positionZ = Random.Range(obstacleMinPos.z, obstacleMaxPos.z);
            } while ((positionX < obstacleMinPos.x + safeRegion && positionZ < obstacleMinPos.z + safeRegion)
            	  || (positionX > obstacleMaxPos.x - safeRegion && positionZ > obstacleMaxPos.z - safeRegion));
            obstaclePosition = new Vector3(positionX, obstacleInstance.transform.position.y, positionZ);
            obstacleInstance.transform.position = obstaclePosition;
        }
    }
}
