using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersonManager : MonoBehaviour
{
    public GameObject person;
    public Vector3 personMinPos;
    public Vector3 personMaxPos;
    public int personCount = 10;
    public float personSpeedVariation = 0.2f;


    void Start()
    {
        InitializePeople();
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


    private void InitializePeople()
    {
        GameObject personInstance;
        PersonController personController;
        Vector3 personPosition;
        float positionX, positionZ;

        for (int personIndex = 0; personIndex < personCount; personIndex++)
        {
            personInstance = GameObject.Instantiate(person);
            personController = personInstance.GetComponent<PersonController>();

            do {
                positionX = Random.Range(personMinPos.x, personMaxPos.x);
                positionZ = Random.Range(personMinPos.z, personMaxPos.z);
                personPosition = new Vector3(positionX, personInstance.transform.position.y, positionZ);
            }
            while (!IsPositionValid(personPosition, 2.0f));

            personController.SetTransformPosition(personPosition);
            personController.SetSpeed(Random.Range(personController.speed - personSpeedVariation, personController.speed + personSpeedVariation));
        }
    }


    private bool IsPositionValid(Vector3 position, float distanceThreshold)
    {
        GameObject finalGoal = GameObject.FindWithTag("Final Goal");
        GameObject player = GameObject.FindWithTag("Player");
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Static Obstacle");

        bool isValid =  GetFlatDistance(position, finalGoal.transform.position) > distanceThreshold;
        isValid = isValid && GetFlatDistance(position, player.transform.position) > distanceThreshold;

        foreach (GameObject obstacle in obstacles)
        {
            if (!isValid) break;
            isValid = isValid && GetFlatDistance(position, obstacle.transform.position) > distanceThreshold;
        }

        return isValid;
    }
}
