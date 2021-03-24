using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersonController : MonoBehaviour
{
    public GameObject[] targets;
    public bool useDynamicTargets = true;
    public Vector3 dynamicTargetsMinPos;
    public Vector3 dynamicTargetsMaxPos;
    public int dynamicTargetsCount = 10;
    public float targetPositionThreshold = 1.0f;
    public float speed = 1.0f;
    public float rotationSpeed = 1.0f;

    private int currentTargetIndex;
    private GameObject[] dynamicTargets;


    void Start()
    {
        currentTargetIndex = 0;

        if (useDynamicTargets) {
            dynamicTargets = new GameObject[dynamicTargetsCount];
            initializeDynamicTargets();
        }
    }


    void Update()
    {
        moveToTarget();
    }


    private void initializeDynamicTargets()
    {
        GameObject dynamicTarget;
        Vector3 dynamicTargetPosition;

        for (int targetIndex = 0; targetIndex < dynamicTargetsCount; targetIndex++) {
            dynamicTarget = new GameObject("Dynamic Movement Target");
            dynamicTargetPosition = new Vector3(
                Random.Range(dynamicTargetsMinPos.x, dynamicTargetsMaxPos.x),
                Random.Range(dynamicTargetsMinPos.y, dynamicTargetsMaxPos.y),
                Random.Range(dynamicTargetsMinPos.z, dynamicTargetsMaxPos.z));
            
            dynamicTarget.transform.position = dynamicTargetPosition;
            dynamicTargets[targetIndex] = dynamicTarget;
        }
    }


    private void moveToTarget()
    {
        GameObject currentTarget = useDynamicTargets ? dynamicTargets[currentTargetIndex] : targets[currentTargetIndex];

        if (Vector3.Distance(transform.position, currentTarget.transform.position) < targetPositionThreshold)
        {
            currentTargetIndex++;
            currentTargetIndex %= useDynamicTargets ? dynamicTargetsCount : targets.Length;
        }


        Vector3 targetPosition = new Vector3(currentTarget.transform.position.x, transform.position.y, currentTarget.transform.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
