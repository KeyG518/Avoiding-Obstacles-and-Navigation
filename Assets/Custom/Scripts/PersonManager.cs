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

        for (int personIndex = 0; personIndex < personCount; personIndex++)
        {
            personInstance = GameObject.Instantiate(person);
            personController = personInstance.GetComponent<PersonController>();
            personPosition = new Vector3(Random.Range(personMinPos.x, personMaxPos.x), personInstance.transform.position.y, Random.Range(personMinPos.z, personMaxPos.z));

            personController.setTransformPosition(personPosition);
            personController.setSpeed(Random.Range(personController.speed - personSpeedVariation, personController.speed + personSpeedVariation));
        }
    }
}
