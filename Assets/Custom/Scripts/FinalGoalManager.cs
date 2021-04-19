using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGoalManager : MonoBehaviour
{
    public Vector3 finalGoalMinPos, finalGoalMaxPos;


    void Start()
    {
        InitializeFinalGoal();
    }


    void Update()
    {
    }


    public void InitializeFinalGoal()
    {
        float positionX, positionZ;
        Vector3 finalGoalPosition;

        do {
            positionX = Random.Range(finalGoalMinPos.x, finalGoalMaxPos.x);
            positionZ = Random.Range(finalGoalMinPos.z, finalGoalMaxPos.z);
            finalGoalPosition = new Vector3(positionX, this.transform.position.y, positionZ);
        }
        while (!IsPositionValid(finalGoalPosition, 2.0f));

        this.transform.position = finalGoalPosition;
    }


    private float GetFlatDistance(Vector3 vectorA, Vector3 vectorB)
    {
        vectorA.y = 0;
        vectorB.y = 0;

        return Vector3.Distance(vectorA, vectorB);
    }


    private bool IsPositionValid(Vector3 position, float distanceThreshold)
    {
        GameObject player = GameObject.FindWithTag("Player");
        bool isValid =  GetFlatDistance(position, player.transform.position) > distanceThreshold;

        return isValid;
    }
}
