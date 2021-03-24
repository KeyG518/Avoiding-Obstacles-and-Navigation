using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersonManager : MonoBehaviour
{
    public GameObject person;
    public Vector3 personMinPos;
    public Vector3 personMaxPos;
    public int personCount = 10;


    void Start()
    {
        generatePeople();
    }


    void Update()
    {
    }


    private void generatePeople()
    {
        GameObject personInstance;
        PersonController personController;
        Vector3 personPosition;

        for (int i = 0; i < personCount; i++) {
            initializePerson(0.2f);
        }
    }


    private void initializePerson(float speedVariation) {
        GameObject personInstance = GameObject.Instantiate(person);

        Vector3 personPosition = new Vector3(
            Random.Range(personMinPos.x, personMaxPos.x),
            personInstance.transform.position.y,
            Random.Range(personMinPos.z, personMaxPos.z));
        personInstance.transform.position = personPosition;

        PersonController personController = personInstance.GetComponent(typeof(PersonController)) as PersonController;
        personController.speed = Random.Range(personController.speed - speedVariation, personController.speed + speedVariation);
    }
}
