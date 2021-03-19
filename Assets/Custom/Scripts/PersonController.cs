using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    public GameObject[] targets;
    public bool useDynamicTargets = true;
    public Vector3 dynamicTargetsMinPos;
    public Vector3 dynamicTargetsMaxPos;
    public float targetPositionThreshold = 1.0f;
    public float speed = 1.0f;
    public float rotationSpeed = 1.0f;

    private int currentTargetIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentTargetIndex = 0;

        if (useDynamicTargets) {
            Vector3 newTargetPosition;

            foreach (GameObject target in targets) {
                newTargetPosition = new Vector3(
                    Random.Range(dynamicTargetsMinPos.x, dynamicTargetsMaxPos.x),
                    Random.Range(dynamicTargetsMinPos.y, dynamicTargetsMaxPos.y),
                    Random.Range(dynamicTargetsMinPos.z, dynamicTargetsMaxPos.z));
                
                target.transform.position = newTargetPosition;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveToTarget();
    }


    private void moveToTarget()
    {
        if (Vector3.Distance(transform.position, targets[currentTargetIndex].transform.position) < targetPositionThreshold)
        {
            currentTargetIndex++;
            currentTargetIndex %= targets.Length;
        }

        GameObject currentTarget = targets[currentTargetIndex];

        Vector3 targetPosition = new Vector3(currentTarget.transform.position.x, transform.position.y, currentTarget.transform.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
