using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    public GameObject[] obstacles;
    public Vector3 obstaclesMinPos;
    public Vector3 obstaclesMaxPos;
    public int numObstacles = 5;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 obstaclePosition;
        int randomIndex;
        for (int i = 0;i < numObstacles;i++){
            randomIndex = Random.Range(0, obstacles.Length);
            GameObject obstacle = GameObject.Instantiate(obstacles[randomIndex]);
            obstaclePosition = new Vector3(
                Random.Range(obstaclesMinPos.x, obstaclesMaxPos.x),
                obstacle.transform.position.y,
                Random.Range(obstaclesMinPos.z, obstaclesMaxPos.z));
            obstacle.transform.position = obstaclePosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
