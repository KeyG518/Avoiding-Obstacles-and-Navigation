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
        initializePeople();
    }


    void Update()
    {
    }


    private void initializePeople()
    {
        GameObject personInstance;
        PersonController personController;
        Vector3 personPosition;
        float positionX, positionZ;
        float safeRegion = 3;

        for (int personIndex = 0; personIndex < personCount; personIndex++)
        {
            personInstance = GameObject.Instantiate(person);
            personController = personInstance.GetComponent<PersonController>();
            do {
		positionX = Random.Range(personMinPos.x, personMaxPos.x);
		positionZ = Random.Range(personMinPos.z, personMaxPos.z);
            } while ((positionX < personMinPos.x + safeRegion && positionZ < personMinPos.z + safeRegion)
            	  || (positionX > personMaxPos.x - safeRegion && positionZ > personMaxPos.z - safeRegion));
            personPosition = new Vector3(positionX, personInstance.transform.position.y, positionZ);

            personController.setTransformPosition(personPosition);
            personController.setSpeed(Random.Range(personController.speed - personSpeedVariation, personController.speed + personSpeedVariation));
        }
    }
}